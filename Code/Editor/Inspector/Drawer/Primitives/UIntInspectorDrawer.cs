using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class UIntInspectorDrawer : AValueInspectorDrawer<uint>
	{
		protected override void Draw(object parent, AValueSource source, uint current)
		{
			long newValue = EditorGUILayout.LongField(source.Name, current);
			long clamped = (uint)Math.Clamp(newValue, uint.MinValue, uint.MaxValue);
			if(current != clamped)
			{
				source.SetValue(parent, clamped);
			}
		}
	}
}
