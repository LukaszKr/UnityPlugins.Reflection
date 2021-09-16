using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ShortReflectionDrawer : AKeyValueReflectionDrawer<short>
	{
		protected override short OnDraw(Rect rect, string label, short current)
		{
			return (short)EditorGUI.IntField(rect, label, current);
		}
	}
}
