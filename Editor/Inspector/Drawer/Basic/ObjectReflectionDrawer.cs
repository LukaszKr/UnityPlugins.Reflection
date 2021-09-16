using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ObjectReflectionDrawer : AReflectionDrawer
	{
		public override bool CanDraw(Type type)
		{
			return !type.IsPrimitive && !type.IsValueType && type != typeof(string);
		}

		protected override void OnDraw(object obj, FieldInfo field)
		{
			Rect line = Layout.GetLine();

			object inspected = field.GetValue(obj);
			Type type = inspected.GetType();
			EditorGUI.LabelField(line, $"{type.Name}");


			FieldInfo[] fields = Inspector.GetFields(type);
			for(int x = 0; x < fields.Length; ++x)
			{
				FieldInfo inspectedField = fields[x];
				Type inspectedFieldType = inspectedField.FieldType;
				AReflectionDrawer drawer = Inspector.GetDrawer(inspectedFieldType);
				drawer.Draw(Inspector, inspected, inspectedField);
			}
		}
	}
}
