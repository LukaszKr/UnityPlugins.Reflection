using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class TypeAnalyzer
	{
		public readonly IgnoreTypeFilter IgnoreType = new IgnoreTypeFilter();
		public readonly List<ITypeFilter> Filters = new List<ITypeFilter>();

		public bool IncludeInstance = true;
		public bool IncludeStatic = false;

		private Dictionary<Type, TypeCacheEntry> m_Entries = new Dictionary<Type, TypeCacheEntry>();

		public TypeAnalyzer()
		{
			IgnoreType.Ignore.Add(typeof(Type));

			Filters.Add(IgnoreType);
			Filters.Add(new IgnoreCompilerGeneratedTypeFilter());
		}

		public TypeCacheEntry GetEntry(Type type)
		{
			TypeCacheEntry entry;
			if(!m_Entries.TryGetValue(type, out entry))
			{
				entry = new TypeCacheEntry(this, type);
				m_Entries.Add(type, entry);
			}
			return entry;
		}

		public bool IsValid(Type type)
		{
			int count = Filters.Count;
			for(int x = 0; x < count; ++x)
			{
				ITypeFilter filter = Filters[x];
				if(!filter.IsValid(type))
				{
					return false;
				}
			}

			return true;
		}

		public bool IsValid(MemberInfo member, Type type)
		{
			int count = Filters.Count;
			for(int x = 0; x < count; ++x)
			{
				ITypeFilter filter = Filters[x];
				if(!filter.IsValid(member, type))
				{
					return false;
				}
			}

			return true;
		}

		public bool IsValid(FieldInfo field)
		{
			return IsValid(field, field.FieldType);
		}

		public bool IsValid(PropertyInfo property)
		{
			if(!property.CanRead || !property.CanWrite)
			{
				return false;
			}

			//no support for indexed elements at this time
			if(property.GetIndexParameters().Length > 0)
			{
				return false;
			}

			return IsValid(property, property.PropertyType);
		}
	}
}
