using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class ULongReflectionDrawer : AKeyValueReflectionDrawer<ulong>
	{
		protected override ulong OnDraw(Rect rect, string label, ulong current)
		{
			//TODO: this doesn't cover full ulong range atm
			return (ulong)EditorGUI.LongField(rect, label, (long)current);
		}
	}
}
