using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class BoolInspectorDrawer : AValueInspectorDrawer<bool>
	{
		protected override bool OnDraw(object parent, string label, bool value)
		{
			return EditorGUILayout.ToggleLeft(label, value);
		}
	}
}
