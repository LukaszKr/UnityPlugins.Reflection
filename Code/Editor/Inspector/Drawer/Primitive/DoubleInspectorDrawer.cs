using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class DoubleInspectorDrawer : AValueInspectorDrawer<double>
	{
		protected override double OnDraw(object parent, Type type, string label, double value)
		{
			return EditorGUILayout.DoubleField(label, value);
		}
	}
}
