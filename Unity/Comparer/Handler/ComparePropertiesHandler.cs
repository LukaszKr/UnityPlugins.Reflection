using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ComparePropertiesHandler : ACompareHandler<object>
	{
		public BindingFlags Bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		public readonly List<IPropertyFilter> Filters = new List<IPropertyFilter>();

		public override bool Exclusive => false;

		protected override bool CompareImpl(ReflectionComparer comparer, ObjectIssue parent, string path, object left, object right)
		{
			bool foundDiff = false;
			Type type = left.GetType();

			PropertyInfo[] properties = type.GetProperties(Bindings);
			int length = properties.Length;
			for(int x = 0; x < length; ++x)
			{
				PropertyInfo property = properties[x];
				if(ShouldIgnore(left, property) || ShouldIgnore(right, property))
				{
					continue;
				}

				try
				{
					object leftValue = property.GetValue(left);
					object rightValue = property.GetValue(right);
					if(comparer.Compare(parent, property.Name, leftValue, rightValue))
					{
						foundDiff = true;
					}
				}
				catch(Exception exception)
				{
					parent.Issues.Add(new ExceptionIssue(parent, property.Name, exception));
					foundDiff = true;
				}
			}
			return foundDiff;
		}

		private bool ShouldIgnore(object parent, PropertyInfo property)
		{
			int count = Filters.Count;
			for(int x = 0; x < count; ++x)
			{
				IPropertyFilter filter = Filters[x];
				if(filter.ShouldIgnore(parent, property))
				{
					return true;
				}
			}
			return false;
		}

	}
}
