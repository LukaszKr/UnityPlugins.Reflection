using UnityEditor;
using UnityEngine;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ObjectInspectorDrawer : AValueInspectorDrawer<object>
	{
		protected override void Draw(object parent, AValueSource source, object current)
		{
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.LabelField(source.Name, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.labelWidth));
				if(current == null)
				{
					if(GUILayout.Button($"N/A"))
					{
						TypePickerDropdown dropdown = new TypePickerDropdown(source.Name, parent, source);
						Rect rect = GUILayoutUtility.GetLastRect();
						rect.width = Screen.width;
						rect.position = Event.current.mousePosition;
						dropdown.Show(rect);
					}
				}
				else
				{
					EditorGUILayout.LabelField(current.GetType().Name);
					if(GUILayout.Button("X", GUILayout.Width(24)))
					{
						source.SetValue(parent, null);
					}
				}
			}
			EditorGUILayout.EndHorizontal();

			if(current != null)
			{
				EditorGUILayout.BeginVertical("helpbox");
				{
					TypeCacheEntry entry = m_Inspector.Analyzer.GetEntry(source.Type);

					foreach(FieldValueSource field in entry.Fields)
					{
						AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(field.Type);
						drawer.Draw(m_Inspector, current, field);
					}

					foreach(PropertyValueSource property in entry.Properties)
					{
						AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(property.Type);
						drawer.Draw(m_Inspector, current, property);
					}
				}
				EditorGUILayout.EndVertical();
			}
		}
	}
}
