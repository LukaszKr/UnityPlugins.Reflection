using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Logic
{
	public class ReflectionComparer
	{
		private readonly TypeDifferenceDetector m_TypeDifference = new TypeDifferenceDetector();

		private readonly List<IComparerHandler> m_Handlers = new List<IComparerHandler>();
		private readonly List<IComparerHandler> m_ExclusiveHandlers = new List<IComparerHandler>();
		private readonly List<AIssueDetector> m_Detectors = new List<AIssueDetector>();
		private readonly List<IValueFilter> m_ValueFilters = new List<IValueFilter>();

		private readonly HashSet<int> m_VisitedObjects = new HashSet<int>();
		private readonly HashSet<Type> m_ScannedTypes = new HashSet<Type>();

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

				AddDetector(new ValueDifferenceDetector());

				AddFilter(new IgnoreAutomaticBackingFieldFilter());
			}

			AddHandler(Fields);
			AddHandler(Properties);
		}

		#region Compare
		public ObjectIssue Compare(object left, object right)
		{
			m_VisitedObjects.Clear();
			ObjectIssue root = null;
			if(IsPrimitive(left) || IsPrimitive(right))
			{
				root = new ObjectIssue(null, ".");
			}
			ObjectIssue diff = CompareObjects(root, ".", left, right);
			m_VisitedObjects.Clear();
			return diff;
		}

		internal bool Compare(ObjectIssue parent, string path, object left, object right)
		{
			ObjectIssue diff = CompareObjects(parent, path, left, right);
			return diff != null;
		}

		private ObjectIssue CompareObjects(ObjectIssue parent, string path, object left, object right)
		{
			TryScanObject(left);
			TryScanObject(right);

			if(m_ValueFilters.ShouldIgnore(left) || m_ValueFilters.ShouldIgnore(right))
			{
				return null;
			}

			if(CompareValues(parent, path, left, right))
			{
				return parent;
			}

			if(!TryStartCompareObjects(left) || !TryStartCompareObjects(right))
			{
				return null;
			}

			ObjectIssue current = new ObjectIssue(parent, path);

			if(ProcessHandlers(current, path, left, right))
			{
				parent?.Nodes.Add(current);
				return current;
			}

			return null;
		}

		private bool ProcessHandlers(ObjectIssue current, string path, object left, object right)
		{
			bool foundDiff = false;
			bool processed;

			int count = m_ExclusiveHandlers.Count;
			for(int x = 0; x < count; ++x)
			{
				IComparerHandler handler = m_ExclusiveHandlers[x];
				foundDiff |= handler.Compare(this, current, path, left, right, out processed);
				if(processed)
				{
					return foundDiff;
				}
			}

			count = m_Handlers.Count;
			for(int x = 0; x < count; ++x)
			{
				IComparerHandler handler = m_Handlers[x];
				foundDiff |= handler.Compare(this, current, path, left, right, out _);
			}
			return foundDiff;
		}

		private bool CompareValues(ObjectIssue parent, string path, object left, object right)
		{
			bool foundIssue = false;
			if(m_TypeDifference.Compare(parent, path, left, right))
			{
				foundIssue = true;
			}

			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				AIssueDetector detector = m_Detectors[x];
				foundIssue |= detector.Compare(parent, path, left, right);
			}
			return foundIssue;
		}
		#endregion

		#region Handler
		public ReflectionComparer AddHandler(IComparerHandler handler)
		{
			if(handler.Exclusive)
			{
				m_ExclusiveHandlers.Add(handler);
			}
			else
			{
				m_Handlers.Add(handler);
			}
			return this;
		}
		#endregion

		#region Filters
		public ReflectionComparer AddFilter(IFilter filter)
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
		private bool IsPrimitive(object value)
		{
			if(value == null)
			{
				return false;
			}

			Type type = value.GetType();
			return (type.IsPrimitive || typeof(string).IsAssignableFrom(type));
		}

		private bool TryStartCompareObjects(object value)
		{
			if(value == null)
			{
				return false;
			}
			if(IsPrimitive(value))
			{
				return false;
			}
			int hash = value.GetHashCode();
			return m_VisitedObjects.Add(hash);
		}

		private void TryScanObject(object value)
		{
			if(value == null)
			{
				return;
			}

			Type type = value.GetType();

			if(m_ScannedTypes.Contains(type))
			{
				return;
			}
			m_ScannedTypes.Add(type);

			bool ignoreType = type.IsDefined(typeof(ReflectionIgnoreAttribute));
			if(ignoreType)
			{
				IgnoreType(type);
				return;
			}

			MemberInfo[] members = type.GetMembers();
			foreach(MemberInfo member in members)
			{
				if(member.IsDefined(typeof(ReflectionIgnoreAttribute)))
				{
					AddFilter(new IgnoreMemberFilter(type, member));
				}
			}
		}
		#endregion
	}
}
