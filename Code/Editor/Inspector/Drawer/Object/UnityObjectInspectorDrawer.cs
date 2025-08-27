using UnityEditor;
using UnityPlugins.Reflection.Logic;
using Object = UnityEngine.Object;

namespace UnityPlugins.Reflection.Editor
{
	public class UnityObjectInspectorDrawer : AValueInspectorDrawer<Object>
	{
		protected override void Draw(object parent, AValueSource source, Object current)
		{
			Object newValue = EditorGUILayout.ObjectField(current, source.Type, false);
			source.SetValue(parent, newValue);
		}
	}
}
