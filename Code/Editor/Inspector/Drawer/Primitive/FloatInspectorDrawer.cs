using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class FloatInspectorDrawer : AValueInspectorDrawer<float>
	{
		protected override float OnDraw(object parent, Type type, string label, float value)
		{
			return EditorGUILayout.FloatField(label, value);
		}
	}
}
