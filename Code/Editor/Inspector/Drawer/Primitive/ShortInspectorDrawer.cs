using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ShortInspectorDrawer : AValueInspectorDrawer<short>
	{
		protected override void Draw(object parent, AValueSource source, short current)
		{
			long newValue = EditorGUILayout.IntField(source.Name, current);
			long clamped = (short)Math.Clamp(newValue, short.MinValue, short.MaxValue);
			source.SetValue(parent, clamped);
		}
	}
}
