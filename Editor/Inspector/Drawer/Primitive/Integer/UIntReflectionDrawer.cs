using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class UIntReflectionDrawer : AKeyValueReflectionDrawer<uint>
	{
		protected override uint OnDraw(Rect rect, string label, uint current)
		{
			return (uint)EditorGUI.LongField(rect, label, current);
		}
	}
}
