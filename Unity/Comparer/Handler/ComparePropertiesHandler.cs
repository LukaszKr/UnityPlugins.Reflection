using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class ComparePropertiesHandler : ACompareHandler<object>
	{
		public bool IgnoreObsolete = true;
		public bool IgnoreBrackets = true;
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
				if(IgnoreObsolete && property.GetCustomAttribute<ObsoleteAttribute>() != null)
				{
					continue;
				}
				if(IgnoreBrackets)
				{
					MethodInfo info = property.GetGetMethod();
					if(info != null && info.GetParameters().Length > 0)
					{
						continue;
					}
				}
				if(Filters.ShouldIgnore(left, property) || Filters.ShouldIgnore(right, property))
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
	}
}
