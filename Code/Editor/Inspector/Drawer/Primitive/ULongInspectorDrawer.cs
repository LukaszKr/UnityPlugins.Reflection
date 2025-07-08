using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class ULongInspectorDrawer : AValueInspectorDrawer<ulong>
	{
		protected override ulong OnDraw(object parent, Type type, string label, ulong value)
		{
			EditorGUILayout.LabelField($"{label}(ULong) = {value}");
			return value;
		}
	}
}
