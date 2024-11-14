using UnityEditor;

namespace UnityPlugins.Reflection.Editor
{
	public class ULongInspectorDrawer : AValueInspectorDrawer<ulong>
	{
		protected override ulong OnDraw(object parent, string label, ulong value)
		{
			EditorGUILayout.LabelField($"{label}(ULong) = {value}");
			return value;
		}
	}
}
