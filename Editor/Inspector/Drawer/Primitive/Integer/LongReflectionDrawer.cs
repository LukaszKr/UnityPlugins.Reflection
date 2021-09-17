using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class LongReflectionDrawer : AKeyValueReflectionDrawer<long>
	{
		protected override long OnDraw(Rect rect, string label, long current)
		{
			return EditorGUI.LongField(rect, label, current);
		}
	}
}
