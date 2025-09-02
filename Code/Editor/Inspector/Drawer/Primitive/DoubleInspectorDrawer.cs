using UnityEditor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class DoubleInspectorDrawer : AValueInspectorDrawer<double>
	{
		protected override void Draw(object parent, AValueSource source, double current)
		{
			double newValue = EditorGUILayout.DoubleField(source.Name, current);
			if(current != newValue)
			{
				source.SetValue(parent, newValue);
			}
		}
	}
}
