using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.Reflection.Logic
{
	public static class FilterExt
	{
		public static bool ShouldIgnore(this List<IValueFilter> filters, object value)
		{
			int count = filters.Count;
			for(int x = 0; x < count; ++x)
			{
				IValueFilter filter = filters[x];
				if(filter.ShouldIgnore(value))
				{
					return true;
				}
			}
			return false;
		}

		public static bool ShouldIgnore(this List<IFieldFilter> filters, object parent, FieldInfo field)
		{
			int count = filters.Count;
			for(int x = 0; x < count; ++x)
			{
				IFieldFilter filter = filters[x];
				if(filter.ShouldIgnore(parent, field))
				{
					return true;
				}
			}
			return false;
		}

		public static bool ShouldIgnore(this List<IPropertyFilter> filters, object parent, PropertyInfo property)
		{
			int count = filters.Count;
			for(int x = 0; x < count; ++x)
			{
				IPropertyFilter filter = filters[x];
				if(filter.ShouldIgnore(parent, property))
				{
					return true;
				}
			}
			return false;
		}
	}
}
