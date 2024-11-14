using System;
using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class UIntInspectorDrawer : AValueInspectorDrawer<uint>
	{
		protected override uint OnDraw(object parent, string label, uint value)
		{
			long newValue = EditorGUILayout.LongField(label, value);
			return (uint)Math.Clamp(newValue, uint.MinValue, uint.MaxValue);
		}
	}
}
