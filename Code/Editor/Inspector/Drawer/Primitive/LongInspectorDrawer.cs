using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class LongInspectorDrawer : AValueInspectorDrawer<long>
	{
		protected override void Draw(object parent, AValueSource source, long current)
		{
			long newValue = EditorGUILayout.LongField(source.Name, current);
			source.SetValue(parent, newValue);
		}
	}
}
