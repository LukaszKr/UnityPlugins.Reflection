using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class ByteInspectorDrawer : AValueInspectorDrawer<byte>
	{
		protected override byte OnDraw(object parent, string label, byte value)
		{
			int newValue = EditorGUILayout.IntField(label, value);
			return (byte)Math.Clamp(newValue, byte.MinValue, byte.MaxValue);
		}
	}
}
