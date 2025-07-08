using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class StringInspectorDrawer : AValueInspectorDrawer<string>
	{
		protected override string OnDraw(object parent, Type type, string label, string value)
		{
			return EditorGUILayout.TextField(label, value);
		}
	}
}
