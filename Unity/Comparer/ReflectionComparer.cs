using System;
using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ReflectionComparer
	{
		private readonly TypeDifferenceDetector m_TypeDifference = new TypeDifferenceDetector();

		private readonly List<IComparerHandler> m_Handlers = new List<IComparerHandler>();
		private readonly List<AIssueDetector> m_Detectors = new List<AIssueDetector>();
		private readonly List<IValueFilter> m_ValueFilters = new List<IValueFilter>();

		private readonly HashSet<int> m_VisitedObjects = new HashSet<int>();

		public readonly CompareFieldsHandler Fields;
		public readonly ComparePropertiesHandler Properties;

		public ReflectionComparer(bool setupDefaults = true)
		{
			Fields = new CompareFieldsHandler();
			Properties = new ComparePropertiesHandler();

			if(setupDefaults)
			{
				AddHandler(new CompareDictionaryHandler());
				AddHandler(new CompareEnumerableHandler());

				AddHandler(Fields);
				AddHandler(Properties);

				AddDetector(new ValueDifferenceDetector());

				AddFilter(new IgnoreAutomaticBackingFieldFilter());
			}
		}

		#region Compare
		public ObjectIssue Compare(object left, object right)
		{
			m_VisitedObjects.Clear();
			ObjectIssue diff = CompareObjects(null, "", left, right);
			m_VisitedObjects.Clear();
			return diff;
		}

		public bool Compare(ObjectIssue parent, string path, object left, object right)
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

			if(IsPrimitive(left.GetType()))
			{
				return null;
			}

			int count = m_Handlers.Count;
			bool foundDiff = false;

			for(int x = 0; x < count; ++x)
			{
				IComparerHandler handler = m_Handlers[x];
				foundDiff |= handler.Compare(this, current, path, left, right);
				if(foundDiff && handler.Exclusive)
				{
					break;
				}
			}

			if(foundDiff)
			{
				parent?.Nodes.Add(current);
				return current;
			}
			return null;
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
		#endregion

		#region Handler
		public ReflectionComparer AddHandler(IComparerHandler handler)
		{
			m_Handlers.Add(handler);
			return this;
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
			Fields.Filters.Add(fieldFilter);
			return this;
		}

		public ReflectionComparer AddPropertyFilter(IPropertyFilter propertyFilter)
		{
			Properties.Filters.Add(propertyFilter);
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

		private bool IsPrimitive(Type type)
		{
			return (type.IsPrimitive || typeof(string).IsAssignableFrom(type));
		}

		private bool CanCompareObjects(object value)
		{
			if(value == null)
			{
				return true;
			}
			Type type = value.GetType();
			if(IsPrimitive(type))
			{
				return true;
			}
			int hash = value.GetHashCode();
			return m_VisitedObjects.Add(hash);
		}
		#endregion
	}
}
