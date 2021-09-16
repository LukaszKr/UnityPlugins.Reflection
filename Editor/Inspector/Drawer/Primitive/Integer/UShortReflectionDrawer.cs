using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class UShortReflectionDrawer : AKeyValueReflectionDrawer<ushort>
	{
		protected override ushort OnDraw(Rect rect, string label, ushort current)
		{
			return (ushort)EditorGUI.IntField(rect, label, current);
		}
	}
}
