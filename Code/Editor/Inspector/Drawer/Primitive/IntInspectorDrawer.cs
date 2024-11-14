using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class IntInspectorDrawer : AValueInspectorDrawer<int>
	{
		protected override int OnDraw(object parent, string label, int value)
		{
			return EditorGUILayout.IntField(label, value);
		}
	}
}
