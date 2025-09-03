using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ULongInspectorDrawer : AValueInspectorDrawer<ulong>
	{
		protected override void Draw(object parent, AValueSource source, ulong current)
		{
			EditorGUILayout.LabelField($"{source.Name}(ULong) = {current}");
		}
	}
}
