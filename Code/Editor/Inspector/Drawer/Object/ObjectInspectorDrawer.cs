using UnityEditor;
using UnityEngine;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ObjectInspectorDrawer : AValueInspectorDrawer<object>
	{
		protected override void Draw(object parent, AValueSource source, object current)
		{
			EditorGUILayout.BeginVertical("helpbox");
			{
				DrawHeader(parent, source, current);
				DrawValue(current);
			}
			EditorGUILayout.EndVertical();
		}

		protected virtual void DrawHeader(object parent, AValueSource source, object current)
		{
			EditorGUILayout.BeginHorizontal("box");
			{
				string label = source.Name;
				if(current != null)
				{
					label = $"{label}({current.GetType().Name})";
				}

				EditorGUILayout.LabelField(label);
				if(current == null)
				{
					if(GUILayout.Button($"N/A"))
					{
						TypePickerDropdown dropdown = new TypePickerDropdown(source.Name, parent, source);
						dropdown.ShowAtCurrentMousePosition();
					}
				}
				else
				{
					if(GUILayout.Button("X", GUILayout.Width(24)))
					{
						source.SetValue(parent, null);
					}
				}
			}
			EditorGUILayout.EndHorizontal();
		}

		protected virtual void DrawValue(object current)
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
	}
}
