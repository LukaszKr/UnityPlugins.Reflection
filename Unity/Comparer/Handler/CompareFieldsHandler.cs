using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public class CompareFieldsHandler : ACompareHandler<object>
	{
		public BindingFlags Bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		public readonly List<IFieldFilter> Filters = new List<IFieldFilter>();

		public override bool Exclusive => false;

		protected override bool CompareImpl(ReflectionComparer comparer, ObjectIssue parent, string path, object left, object right)
		{
			bool foundDiff = false;
			Type type = left.GetType();

			FieldInfo[] fields = type.GetFields(Bindings);
			int length = fields.Length;
			for(int x = 0; x < length; ++x)
			{
				FieldInfo field = fields[x];
				if(Filters.ShouldIgnore(left, field) || Filters.ShouldIgnore(right, field))
				{
					continue;
				}

				try
				{
					object leftValue = field.GetValue(left);
					object rightValue = field.GetValue(right);
					if(comparer.Compare(parent, field.Name, leftValue, rightValue))
					{
						foundDiff = true;
					}
				}
				catch(Exception exception)
				{
					parent.Issues.Add(new ExceptionIssue(parent, field.Name, exception));
					foundDiff = true;
				}
			}
			return foundDiff;
		}
	}
}
