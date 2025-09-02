using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public abstract class AListInspectorDrawer<TValue> : ObjectInspectorDrawer<TValue>
		where TValue : IList
	{
		protected override void DrawValue(object parent, AValueSource source, TValue current)
		{
			if(current == null)
			{
				return;
			}

			int count = current.Count;
			AListValueSource elementSource = CreateSource(parent, source, current);

			for(int index = 0; index < count; ++index)
			{
				object element = current[index];
				AInspectorDrawer drawer = m_Inspector.Drawers.GetDrawer(elementSource.Type);
				drawer.Draw(m_Inspector, current, elementSource);
				elementSource.Index++;
			}

			if(GUILayout.Button("+"))
			{
				elementSource.AddElement();
			}
		}

		protected abstract AListValueSource CreateSource(object parent, AValueSource source, TValue current);

		protected override void PopulateGenericMenu(GenericMenu menu, object parent, AValueSource source, TValue current)
		{
			if(!source.Type.IsValueType)
			{
				menu.AddItem(new GUIContent("Clear"), false, () =>
				{
					source.SetValue(parent, source.Type.CreateInstance());
				});
			}

			base.PopulateGenericMenu(menu, parent, source, current);
		}
	}
}
