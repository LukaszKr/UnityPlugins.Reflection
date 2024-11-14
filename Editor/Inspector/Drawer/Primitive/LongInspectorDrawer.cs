using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class LongInspectorDrawer : AValueInspectorDrawer<long>
	{
		protected override long OnDraw(object parent, string label, long value)
		{
			return EditorGUILayout.LongField(label, value);
		}
	}
}
