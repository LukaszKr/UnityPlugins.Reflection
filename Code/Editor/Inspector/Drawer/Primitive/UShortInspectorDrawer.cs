using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class UShortInspectorDrawer : AValueInspectorDrawer<ushort>
	{
		protected override ushort OnDraw(object parent, Type type, string label, ushort value)
		{
			int newValue = EditorGUILayout.IntField(label, value);
			return (ushort)Math.Clamp(newValue, ushort.MinValue, ushort.MaxValue);
		}
	}
}
