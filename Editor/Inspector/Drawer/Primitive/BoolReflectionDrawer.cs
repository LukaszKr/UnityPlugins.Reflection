using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class BoolReflectionDrawer : AKeyValueReflectionDrawer<bool>
	{
		protected override bool OnDraw(Rect rect, string label, bool current)
		{
			return EditorGUI.Toggle(rect, label, current);
		}
	}
}
