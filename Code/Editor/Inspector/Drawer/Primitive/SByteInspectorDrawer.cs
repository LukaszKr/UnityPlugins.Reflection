using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class SByteInspectorDrawer : AValueInspectorDrawer<sbyte>
	{
		protected override sbyte OnDraw(object parent, Type type, string label, sbyte value)
		{
			int newValue = EditorGUILayout.IntField(label, value);
			return (sbyte)Math.Clamp(newValue, sbyte.MinValue, sbyte.MaxValue);
		}
	}
}
