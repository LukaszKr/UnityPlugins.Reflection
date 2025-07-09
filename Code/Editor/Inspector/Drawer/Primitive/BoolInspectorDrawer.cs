using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class BoolInspectorDrawer : AValueInspectorDrawer<bool>
	{
		protected override void Draw(object parent, AValueSource source, bool current)
		{
			bool newValue = EditorGUILayout.ToggleLeft(source.Name, current);
			source.SetValue(parent, newValue);
		}
	}
}
