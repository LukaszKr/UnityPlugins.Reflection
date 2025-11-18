using System;
using UnityEditor;
using UnityEngine;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public abstract class AValueInspectorDrawer<TValue> : AInspectorDrawer
	{
		public override bool CanDraw(Type type)
		{
			return typeof(TValue).IsAssignableFrom(type);
		}

		protected override void Draw(object parent, AValueSource source)
		{
			TValue current = source.GetValue<TValue>(parent);
			if(source is AListValueSource listSource)
			{
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUILayout.IntField(listSource.Index, GUILayout.Width(40));
					EditorGUILayout.LabelField(":", GUILayout.Width(10));
					Draw(parent, source, current);
				}
				EditorGUILayout.EndHorizontal();
			}
			else
			{
				Draw(parent, source, current);
			}

			if(Event.current.type == EventType.MouseUp && Event.current.button == 1)
			{
				Rect lastRect = GUILayoutUtility.GetLastRect();
				if(lastRect.Contains(Event.current.mousePosition))
				{
					if(OnContextMenu(parent, source, current))
					{
						Event.current.Use();
					}
				}
			}
		}

		protected abstract void Draw(object parent, AValueSource source, TValue current);

		protected bool OnContextMenu(object parent, AValueSource source, TValue current)
		{
			GenericMenu menu = new GenericMenu();
			PopulateGenericMenu(menu, parent, source, current);
			if(menu.GetItemCount() > 0)
			{
				menu.ShowAsContext();
				return true;
			}
			return false;
		}

		protected virtual void PopulateGenericMenu(GenericMenu menu, object parent, AValueSource source, TValue current)
		{
			if(source is AListValueSource listSource)
			{
				int index = listSource.Index;
				GUIContent label = new GUIContent($"Remove Element at ({listSource.Index})");
				menu.AddItem(label, false, () =>
				{
					listSource.RemoveAt(index);
				});
			}
		}
	}
}
