using System;
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

		protected override object OnDraw(string label, object value, Type type)
		{
			Array array = (Array)value;
			Type elementType = type.GetElementType();

			int length = array.Length;
			EditorGUI.LabelField(Layout.GetLine(), $"{label}({length}) | {elementType.Name}");

			Layout.StartIndent();
			for(int x = 0; x < length; ++x)
			{
				object element = array.GetValue(x);
				AReflectionDrawer drawer = Inspector.GetDrawer(elementType);
				element = drawer.Draw(Inspector, $"[{x}]", element, elementType);
				array.SetValue(element, x);
			}
			Layout.EndIndent();

			return value;
		}
	}
}
