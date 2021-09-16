using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class ByteReflectionDrawer : AKeyValueReflectionDrawer<byte>
	{
		protected override byte OnDraw(Rect rect, string label, byte current)
		{
			return (byte)EditorGUI.IntField(rect, label, current);
		}
	}
}
