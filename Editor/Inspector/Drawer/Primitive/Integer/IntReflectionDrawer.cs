using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class IntReflectionDrawer : AKeyValueReflectionDrawer<int>
	{
		protected override int OnDraw(Rect rect, string label, int current)
		{
			return EditorGUI.IntField(rect, label, current);
		}
	}
}
