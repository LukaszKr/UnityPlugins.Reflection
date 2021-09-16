using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class DoubleReflectionDrawer : AKeyValueReflectionDrawer<double>
	{
		protected override double OnDraw(Rect rect, string label, double current)
		{
			return EditorGUI.DoubleField(rect, label, current);
		}
	}
}
