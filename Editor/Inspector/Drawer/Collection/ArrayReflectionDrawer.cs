using System;
using System.Reflection;
using UnityEditor;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class ArrayReflectionDrawer : AReflectionDrawer
	{
		public override bool CanDraw(Type type)
		{
			return type.IsArray;
		}

		protected override void OnDraw(object obj, FieldInfo field)
		{
			Array array = (Array)field.GetValue(obj);

			EditorGUI.LabelField(Layout.GetLine(), $"Length: {array.Length}");
		}
	}
}
