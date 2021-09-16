using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class FloatReflectionDrawer : AKeyValueReflectionDrawer<float>
	{
		protected override float OnDraw(Rect rect, string label, float current)
		{
			return EditorGUI.FloatField(rect, label, current);
		}
	}
}
