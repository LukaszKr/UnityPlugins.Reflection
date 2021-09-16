using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class StringReflectionDrawer : AKeyValueReflectionDrawer<string>
	{
		protected override string OnDraw(Rect rect, string label, string current)
		{
			return EditorGUI.TextField(rect, label, current);
		}
	}
}
