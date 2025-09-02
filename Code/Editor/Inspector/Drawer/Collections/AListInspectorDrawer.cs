using System.Collections;
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
			ListValueSource elementSource = new ListValueSource(current);

			for(int index = 0; index < count; ++index)
			{
				object element = current[index];
				m_Inspector.Draw(elementSource);
				elementSource.Index++;
			}

			if(GUILayout.Button("+"))
			{
				object defaultValue = TypeUtility.GetDefaultValue(elementSource.ElementType);
				current = AddElement(current, defaultValue);
				source.SetValue(parent, current);
			}
		}

		protected abstract TValue AddElement(TValue list, object defaultValue);
	}
}
