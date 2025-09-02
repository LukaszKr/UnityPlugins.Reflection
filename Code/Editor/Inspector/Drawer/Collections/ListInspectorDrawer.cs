using System.Collections;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ListInspectorDrawer : AListInspectorDrawer<IList>
	{
		protected override AListValueSource CreateSource(object parent, AValueSource source, IList current)
		{
			return new ListValueSource(current);
		}
	}
}
