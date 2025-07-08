using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class ShortInspectorDrawer : AValueInspectorDrawer<short>
	{
		protected override short OnDraw(object parent, Type type, string label, short value)
		{
			int newValue = EditorGUILayout.IntField(label, value);
			return (short)Math.Clamp(newValue, short.MinValue, short.MaxValue);
		}
	}
}
