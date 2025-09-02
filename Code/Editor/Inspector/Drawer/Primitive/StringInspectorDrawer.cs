using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class StringInspectorDrawer : AValueInspectorDrawer<string>
	{
		protected override void Draw(object parent, AValueSource source, string current)
		{
			string newValue = EditorGUILayout.TextField(source.Name, current);
			if(current != newValue)
			{
				source.SetValue(parent, newValue);
			}
		}
	}
}
