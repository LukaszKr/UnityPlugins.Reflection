using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class SByteInspectorDrawer : AValueInspectorDrawer<sbyte>
	{
		protected override void Draw(object parent, AValueSource source, sbyte current)
		{
			long newValue = EditorGUILayout.IntField(source.Name, current);
			long clamped = (sbyte)Math.Clamp(newValue, sbyte.MinValue, sbyte.MaxValue);
			if(current != clamped)
			{
				source.SetValue(parent, clamped);
			}
		}
	}
}
