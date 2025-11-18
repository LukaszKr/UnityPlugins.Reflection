using System;
using UnityEditor;
using UnityEngine;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class GuidInspectorDrawer : AValueInspectorDrawer<Guid>
	{
		protected override void Draw(object parent, AValueSource source, Guid current)
		{
			EditorGUILayout.TextField(source.Name, current.ToString());
		}

		protected override void PopulateGenericMenu(GenericMenu menu, object parent, AValueSource source, Guid current)
		{
			menu.AddItem(new GUIContent("Generate New"), false, () =>
			{
				source.SetValue(parent, Guid.NewGuid());
			});
			base.PopulateGenericMenu(menu, parent, source, current);
		}
	}
}
