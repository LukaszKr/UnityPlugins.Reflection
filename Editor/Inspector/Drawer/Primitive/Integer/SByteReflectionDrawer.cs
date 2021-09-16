using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class SByteReflectionDrawer : AKeyValueReflectionDrawer<sbyte>
	{
		protected override sbyte OnDraw(Rect rect, string label, sbyte current)
		{
			return (sbyte)EditorGUI.IntField(rect, label, current);
		}
	}
}
