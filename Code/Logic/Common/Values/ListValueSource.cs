using System;
using System.Collections;

namespace UnityPlugins.Reflection.Logic
{
	public class ListValueSource : AValueSource
	{
		public readonly IList List;
		public readonly Type ElementType;
		public int Index;

		public override string Name => $"[{Index}]";
		public override Type Type => ElementType;

		public ListValueSource(IList list)
		{
			List = list;
			Type listType = list.GetType();
			if(listType.HasElementType)
			{
				ElementType = listType.GetElementType();
			}
			else if(listType.IsGenericType)
			{
				ElementType = listType.GetGenericArguments()[0];
			}
		}

		protected override object OnGetValue(object parent)
		{
			return List[Index];
		}

		protected override void OnSetValue(object parent, object value)
		{
			List[Index] = value;
		}
	}
}
