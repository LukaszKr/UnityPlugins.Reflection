using System;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ArrayInspectorDrawer : AListInspectorDrawer<Array>
	{
		protected override AListValueSource CreateSource(object parent, AValueSource source, Array current)
		{
			return new ArrayValueSource(parent, source, current);
		}
	}
}
