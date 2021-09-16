using System;
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

		protected override object OnDraw(string label, object value, Type type)
		{
			DrawLabel(value?.GetType());
			return value;
		}

		private void DrawLabel(Type type)
		{
			Rect line = Layout.GetLine();
			EditorGUI.LabelField(line, $"{type?.Name} currently has no registered drawer.");
		}
	}
}
