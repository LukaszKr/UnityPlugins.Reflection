using System;
using System.Collections;

namespace UnityPlugins.Reflection.Logic
{
	public class ListValueSource : AListValueSource
	{
		public readonly IList List;
		public readonly Type ElementType;

		public override Type Type => ElementType;

		public ListValueSource(IList list)
		{
			List = list;
			Type listType = list.GetType();
			ElementType = listType.GetGenericArguments()[0];
		}

		protected override object OnGetValue(object parent)
		{
			return List[Index];
		}

		protected override void OnSetValue(object parent, object value)
		{
			List[Index] = value;
		}

		public override void AddElement()
		{
			List.Add(ElementType.GetDefaultValue());
		}

		public override void RemoveAt(int index)
		{
			List.RemoveAt(index);
		}
	}
}
