using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityPlugins.Common.Editor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ObjectInspectorDrawer<TValue> : AValueInspectorDrawer<TValue>
	{
		protected override void Draw(object parent, AValueSource source, TValue current)
		{
			EditorGUILayout.BeginVertical("box");
			{
				DrawHeader(parent, source, current);
				DrawValue(parent, source, current);
				//Structs - otherwise any changes to them in sub-drawers would get discarded
				if(source.Type.IsValueType)
				{
					source.SetValue(parent, current);
				}
			}
			EditorGUILayout.EndVertical();
		}

		protected virtual void DrawHeader(object parent, AValueSource source, TValue current)
		{
			EditorGUILayout.BeginHorizontal("helpbox");
			{
				string label = source.Name;
				if(current != null)
				{
					label = $"{label}({current.GetType().Name})";
				}

				EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
				if(current == null)
				{
					if(GUILayout.Button($"N/A"))
					{
						List<Type> types = TypeUtility.GetConstructableTypes(source.Type);
						if(types.Count == 1)
						{
							object instance = types[0].CreateInstance();
							source.SetValue(parent, instance);
						}
						else if(types.Count > 1)
						{
							TypePickerDropdown dropdown = new TypePickerDropdown(source.Name, parent, source, types);
							dropdown.ShowAtCurrentMousePosition();
						}
					}
				}
				else if(!source.Type.IsValueType)
				{
				}
			}
			EditorGUILayout.EndHorizontal();
		}

		protected virtual void DrawValue(object parent, AValueSource source, TValue current)
		{
			if(current != null)
			{
				EditorGUILayout.BeginVertical();
				{
					TypeCacheEntry entry = m_Inspector.Analyzer.GetEntry(current.GetType());

					foreach(FieldValueSource field in entry.Fields)
					{
						AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(field.GetValueType(current));
						drawer.Draw(m_Inspector, current, field);
					}

					foreach(PropertyValueSource property in entry.Properties)
					{
						AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(property.GetValueType(current));
						drawer.Draw(m_Inspector, current, property);
					}
				}
				EditorGUILayout.EndVertical();
			}
		}

		protected override void PopulateGenericMenu(GenericMenu menu, object parent, AValueSource source, TValue current)
		{
			if(!source.Type.IsValueType)
			{
				menu.AddItem(new GUIContent("Delete"), source.Type.IsValueType, () =>
				{
					source.SetValue(parent, source.Type.GetDefaultValue());
				});
			}
		}
	}
}
