﻿using System;
using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class FloatInspectorDrawer : AValueInspectorDrawer<float>
	{
		protected override void Draw(object parent, AValueSource source, float current)
		{
			float newValue = EditorGUILayout.FloatField(source.Name, current);
			source.SetValue(parent, newValue);
		}
	}
}
