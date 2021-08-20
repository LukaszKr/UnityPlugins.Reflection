using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ReflectionComparer
	{
		private readonly TypeDifferenceDetector m_TypeDifference = new TypeDifferenceDetector();

		private readonly List<AIssueDetector> m_Detectors = new List<AIssueDetector>();
		private readonly List<IValueFilter> m_ValueFilters = new List<IValueFilter>();
		private readonly List<IFieldFilter> m_FieldFilters = new List<IFieldFilter>();
		private readonly List<IPropertyFilter> m_PropertyFilters = new List<IPropertyFilter>();

		private readonly HashSet<int> m_VisitedObjects = new HashSet<int>();

		public ReflectionComparer()
		{
			AddDetector(new ValueDifferenceDetector());

			AddFilter(new IgnoreAutomaticBackingFieldFilter());
		}

		#region Compare
		public ObjectIssue Compare(object left, object right)
		{
			try
			{
				return CompareObjects(null, "", left, right);
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				m_VisitedObjects.Clear();
			}
		}

		private ObjectIssue CompareObjects(ObjectIssue parent, string path, object left, object right)
		{
			ObjectIssue current = new ObjectIssue(parent, path);

			if(CompareValues(current, path, left, right))
			{
				parent?.Nodes.Add(current);
				return current;
			}

			if(left == null || right == null)
			{
				return null;
			}

			if(CompareDictonary(current, path, left as IDictionary, right as IDictionary))
			{
				parent?.Nodes.Add(current);
				return current;
			}
			if(CompareEnumerable(current, path, left as IEnumerable, right as IEnumerable))
			{
				parent?.Nodes.Add(current);
				return current;
			}

			bool foundDiff = false;
			Type type = left.GetType();

			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			int length = fields.Length;
			for(int x = 0; x < length; ++x)
			{
				FieldInfo field = fields[x];
				if(ShouldIgnore(left, field) || ShouldIgnore(right, field))
				{
					continue;
				}

				object leftValue = field.GetValue(left);
				object rightValue = field.GetValue(right);
				if(Compare(current, field.Name, leftValue, rightValue))
				{
					foundDiff = true;
					continue;
				}
			}

			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			length = properties.Length;
			for(int x = 0; x < length; ++x)
			{
				PropertyInfo property = properties[x];
				if(ShouldIgnore(left, property) || ShouldIgnore(right, property))
				{
					continue;
				}

				object leftValue = property.GetValue(left);
				object rightValue = property.GetValue(right);
				if(Compare(current, property.Name, leftValue, rightValue))
				{
					foundDiff = true;
					continue;
				}
			}

			if(foundDiff)
			{
				parent?.Nodes.Add(current);
				return current;
			}
			return null;
		}

		private bool CompareDictonary(ObjectIssue parent, string path, IDictionary left, IDictionary right)
		{
			if(left == null || right == null)
			{
				return false;
			}

			bool foundIssue = false;

			int leftCount = left.Count;
			int rightCount = right.Count;
			if(leftCount != rightCount)
			{
				parent.Issues.Add(new DifferentLengthIssue(parent, path, leftCount, rightCount));
				foundIssue = true;
			}

			foreach(object key in left.Keys)
			{
				object leftValue = left[key];
				string keyPath = $"{path}[{key}]";
				if(right.Contains(key))
				{
					object rightValue = right[key];
					foundIssue |= Compare(parent, keyPath, leftValue, rightValue);
				}
				else
				{
					parent.Issues.Add(new DifferentValueIssue(parent, keyPath, leftValue, null));
					foundIssue = true;
				}
			}

			foreach(object key in right.Keys)
			{
				object rightValue = right[key];
				if(!left.Contains(key))
				{
					string keyPath = $"{path}[{key}]";
					parent.Issues.Add(new DifferentValueIssue(parent, keyPath, null, rightValue));
					foundIssue = true;
				}
			}

			return foundIssue;
		}

		private bool CompareEnumerable(ObjectIssue parent, string path, IEnumerable enumerableA, IEnumerable enumerableB)
		{
			if(enumerableA == null || enumerableB == null)
			{
				return false;
			}

			bool foundIssue = false;

			IEnumerator leftEnumerator = enumerableA.GetEnumerator();
			IEnumerator rightEnumerator = enumerableB.GetEnumerator();

			int oldIssueCount = parent.Issues.Count;
			int leftCount = 0;
			int rightCount = 0;
			do
			{
				bool leftProgressed = leftEnumerator.MoveNext();
				if(leftProgressed)
				{
					++leftCount;
				}
				bool rightProgressed = rightEnumerator.MoveNext();
				if(rightProgressed)
				{
					++rightCount;
				}

				if(leftCount != rightCount)
				{
					parent.Issues.Insert(oldIssueCount, new DifferentLengthIssue(parent, path, leftCount, rightCount));
					return true;
				}

				if(!leftProgressed || !rightProgressed)
				{
					return foundIssue;
				}

				object leftValue = leftEnumerator.Current;
				object rightValue = rightEnumerator.Current;
				string elementPath = $"{path}[{leftCount-1}]";
				foundIssue |= Compare(parent, elementPath, leftValue, rightValue);

			}
			while(true);
		}

		private bool CompareValues(ObjectIssue parent, string path, object left, object right)
		{
			bool foundIssue = false;
			if(parent.TryAddIssue(m_TypeDifference.Detect(parent, path, left, right)))
			{
				foundIssue = true;
			}

			if(ShouldIgnore(left) || ShouldIgnore(right))
			{
				return foundIssue;
			}
			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				AIssueDetector detector = m_Detectors[x];
				ADetectedIssue issue = detector.Detect(parent, path, left, right);
				foundIssue |= parent.TryAddIssue(issue);
			}
			return foundIssue;
		}

		private bool Compare(ObjectIssue parent, string path, object left, object right)
		{
			if(CompareValues(parent, path, left, right))
			{
				return true;
			}
			else if(CanCompareObjects(left) && CanCompareObjects(right))
			{
				ObjectIssue issue = CompareObjects(parent, path, left, right);
				return issue != null;
			}
			return false;
		}
		#endregion

		#region Filters
		public ReflectionComparer AddFilter(AFilter filter)
		{
			if(filter is IValueFilter value)
			{
				AddValueFilter(value);
			}
			if(filter is IFieldFilter field)
			{
				AddFieldFilter(field);
			}
			if(filter is IPropertyFilter property)
			{
				AddPropertyFilter(property);
			}
			return this;
		}

		public ReflectionComparer AddValueFilter(IValueFilter valueFilter)
		{
			m_ValueFilters.Add(valueFilter);
			return this;
		}

		public ReflectionComparer AddFieldFilter(IFieldFilter fieldFilter)
		{
			m_FieldFilters.Add(fieldFilter);
			return this;
		}

		public ReflectionComparer AddPropertyFilter(IPropertyFilter propertyFilter)
		{
			m_PropertyFilters.Add(propertyFilter);
			return this;
		}

		public ReflectionComparer IgnoreMember<TParent>(string memberName)
		{
			return AddFilter(new IgnoreMemberByNameFilter(typeof(TParent), memberName));
		}

		public ReflectionComparer IgnoreMember(Type parentType, string memberName)
		{
			return AddFilter(new IgnoreMemberByNameFilter(parentType, memberName));
		}

		public ReflectionComparer IgnoreMember(Type parentType, Type memberType)
		{
			return AddFilter(new IgnoreMemberByTypeFilter(parentType, memberType));
		}

		public ReflectionComparer IgnoreType<TType>()
		{
			return AddFilter(new IgnoreByTypeFilter(typeof(TType)));
		}

		public ReflectionComparer IgnoreType(Type ignoredType)
		{
			return AddFilter(new IgnoreByTypeFilter(ignoredType));
		}
		#endregion

		#region Detectors
		public ReflectionComparer AddDetector(AIssueDetector detector)
		{
			m_Detectors.Add(detector);
			return this;
		}

		public ReflectionComparer DetectSharedObject(Type type)
		{
			return AddDetector(new SharedObjectDetector(type));
		}

		public ReflectionComparer DetectSharedObject<TShared>(TShared shared)
		{
			return DetectSharedObject(typeof(TShared));
		}

		public ReflectionComparer DetectSingleton(Type type)
		{
			return AddDetector(new DuplicatedSingletonDetector(type));
		}

		public ReflectionComparer DetectSingleton<TSingleton>(TSingleton singleton)
		{
			return DetectSingleton(typeof(TSingleton));
		}
		#endregion

		#region Helpers
		private bool ShouldIgnore(object parent, FieldInfo field)
		{
			int count = m_FieldFilters.Count;
			for(int x = 0; x < count; ++x)
			{
				IFieldFilter filter = m_FieldFilters[x];
				if(filter.ShouldIgnore(parent, field))
				{
					return true;
				}
			}
			return false;
		}

		private bool ShouldIgnore(object parent, PropertyInfo property)
		{
			int count = m_PropertyFilters.Count;
			for(int x = 0; x < count; ++x)
			{
				IPropertyFilter filter = m_PropertyFilters[x];
				if(filter.ShouldIgnore(parent, property))
				{
					return true;
				}
			}
			return false;
		}

		private bool ShouldIgnore(object value)
		{
			int count = m_ValueFilters.Count;
			for(int x = 0; x < count; ++x)
			{
				IValueFilter filter = m_ValueFilters[x];
				if(filter.ShouldIgnore(value))
				{
					return true;
				}
			}
			return false;
		}

		private bool CanCompareObjects(object value)
		{
			if(value == null)
			{
				return true;
			}
			int hash = value.GetHashCode();
			return m_VisitedObjects.Add(hash);
		}
		#endregion
	}
}
