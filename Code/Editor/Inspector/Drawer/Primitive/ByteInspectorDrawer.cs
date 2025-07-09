using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ByteInspectorDrawer : AValueInspectorDrawer<byte>
	{
		protected override void Draw(object parent, AValueSource source, byte current)
		{
			int newValue = EditorGUILayout.IntField(source.Name, current);
			byte clampedValue = (byte)Math.Clamp(newValue, byte.MinValue, byte.MaxValue);
			source.SetValue(parent, clampedValue);
		}
	}
}
