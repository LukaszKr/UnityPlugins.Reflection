using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class ObjectComparerHandler : AComparerHandler
	{
		public ObjectComparerHandler(ReflectionComparer comparer)
			: base(comparer)
		{
		}

		public override bool CanHandle(Type type)
		{
			return true;
		}

		public override void Compare(ComparisionGroup group, Type type)
		{
			TypeCacheEntry entry = m_Comparer.Analyzer.GetEntry(type);

			if(group.Left == null)
			{
				if(group.Right != null)
				{
					group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
					return;
				}
				return; //both null
			}
			else if(group.Right == null)
			{
				group.Entries.Add(new DifferentValueResultEntry(group.Left, group.Right));
				return;
			}

			foreach(FieldInfo field in entry.Fields)
			{
				object subLeft = field.GetValue(group.Left);
				object subRight = field.GetValue(group.Right);
				m_Comparer.CompareInternal(group, field.Name, subLeft, subRight);
			}

			foreach(PropertyInfo property in entry.Properties)
			{
				object subLeft = property.GetValue(group.Left);
				object subRight = property.GetValue(group.Right);
				m_Comparer.CompareInternal(group, property.Name, subLeft, subRight);
			}
		}
	}
}
