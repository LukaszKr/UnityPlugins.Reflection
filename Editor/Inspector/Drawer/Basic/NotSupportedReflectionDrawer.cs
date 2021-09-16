using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class NotSupportedReflectionDrawer : AReflectionDrawer
	{
		public override bool CanDraw(Type type)
		{
			return true;
		}

		protected override void OnDraw(object obj, FieldInfo field)
		{
			Rect line = Layout.GetLine();
			EditorGUI.LabelField(line, $"{field.FieldType} currently has no registered drawer.");
		}
	}
}
