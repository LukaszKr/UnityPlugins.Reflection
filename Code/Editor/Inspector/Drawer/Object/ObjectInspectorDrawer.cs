using System.Reflection;
using UnityPlugins.Reflection.Logic;
using UnityEditor;
using System;

namespace UnityPlugins.Reflection.Editor
{
	public class ObjectInspectorDrawer : AValueInspectorDrawer<object>
	{
		protected override object OnDraw(object parent, Type type, string label, object value)
		{
			EditorGUILayout.BeginVertical("box");
			{
				EditorGUILayout.LabelField(label);
				if(value == null)
				{
					EditorGUILayout.LabelField($"{label}: NULL");
					EditorGUILayout.EndVertical();
					return value;
				}
				TypeCacheEntry entry = m_Inspector.Analyzer.GetEntry(value.GetType());

				foreach(FieldInfo field in entry.Fields)
				{
					object fieldValue = field.GetValue(value);
					AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(field.FieldType);
					fieldValue = drawer.Draw(m_Inspector, value, field.FieldType, field.Name, fieldValue);
					field.SetValue(value, fieldValue);
				}

				foreach(PropertyInfo property in entry.Properties)
				{
					object propertyValue = property.GetValue(value);
					AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(property.PropertyType);
					propertyValue = drawer.Draw(m_Inspector, value, property.PropertyType, property.Name, propertyValue);
					property.SetValue(value, propertyValue);
				}
			}
			EditorGUILayout.EndVertical();

			return value;
		}
	}
}
