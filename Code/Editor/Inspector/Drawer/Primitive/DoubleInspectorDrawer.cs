using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class DoubleInspectorDrawer : AValueInspectorDrawer<double>
	{
		protected override double OnDraw(object parent, string label, double value)
		{
			return EditorGUILayout.DoubleField(label, value);
		}
	}
}
