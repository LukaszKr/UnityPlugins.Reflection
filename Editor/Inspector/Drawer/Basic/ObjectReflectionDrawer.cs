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

		protected override object OnDraw(string label, object value, Type type)
		{
			if(value == null)
			{
				EditorGUI.LabelField(Layout.GetLine(), $"{type.Name}");
				if(GUI.Button(Layout.GetLine(), "Create"))
				{
					value = Activator.CreateInstance(type);
				}
			}
			else
			{
				Type inspectedType = value.GetType();
				EditorGUI.LabelField(Layout.GetLine(), $"{inspectedType.Name}");


				Layout.StartIndent();
				FieldInfo[] fields = Inspector.GetFields(inspectedType);
				for(int x = 0; x < fields.Length; ++x)
				{
					FieldInfo inspectedField = fields[x];
					Type inspectedFieldType = inspectedField.FieldType;
					AReflectionDrawer drawer = Inspector.GetDrawer(inspectedFieldType);
					drawer.Draw(Inspector, value, inspectedField);
				}
				Layout.EndIndent();
			}

			return value;
		}
	}
}
