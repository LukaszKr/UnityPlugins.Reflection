using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class IntInspectorDrawer : AValueInspectorDrawer<int>
	{
		protected override void Draw(object parent, AValueSource source, int current)
		{
			int newValue = EditorGUILayout.IntField(source.Name, current);
			if(current != newValue)
			{
				source.SetValue(parent, newValue);
			}
		}
	}
}
