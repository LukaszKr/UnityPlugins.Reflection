using System.Collections;

namespace UnityPlugins.Reflection.Editor
{
	public class ListInspectorDrawer : AListInspectorDrawer<IList>
	{
		protected override IList AddElement(IList list)
		{
			list.Add(default);
			return list;
		}
	}
}
