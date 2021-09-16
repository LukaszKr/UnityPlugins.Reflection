using System;
using System.Reflection;
using ProceduralLevel.UnityPlugins.Common.Unity;
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

			object inspected = field.GetValue(obj);
			Type inspectedType;

			if(inspected == null)
			{
				inspectedType = field.FieldType;
				EditorGUI.LabelField(Layout.GetLine(), $"{inspectedType.Name}");
				if(GUI.Button(Layout.GetLine(), "Create"))
				{
					inspected = Activator.CreateInstance(field.FieldType);
					field.SetValue(obj, inspected);
				}
				return;
			}
			
			inspectedType = inspected.GetType();
			EditorGUI.LabelField(Layout.GetLine(), $"{inspectedType.Name}");


			FieldInfo[] fields = Inspector.GetFields(inspectedType);
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
