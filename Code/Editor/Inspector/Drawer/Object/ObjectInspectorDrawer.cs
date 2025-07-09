using UnityEditor;
using UnityEngine;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ObjectInspectorDrawer : AValueInspectorDrawer<object>
	{
		protected override void Draw(object parent, AValueSource source, object current)
		{
			EditorGUILayout.BeginVertical("box");
			{
				EditorGUILayout.LabelField(source.Name);
				if(current == null)
				{

					if(GUILayout.Button($"{source.Name}: NULL"))
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
			}
			EditorGUILayout.EndVertical();
		}
	}
}
