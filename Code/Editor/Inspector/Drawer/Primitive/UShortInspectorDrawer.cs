using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class UShortInspectorDrawer : AValueInspectorDrawer<ushort>
	{
		protected override void Draw(object parent, AValueSource source, ushort current)
		{
			long newValue = EditorGUILayout.IntField(source.Name, current);
			long clamped = (ushort)Math.Clamp(newValue, ushort.MinValue, ushort.MaxValue);
			source.SetValue(parent, clamped);
		}
	}
}
