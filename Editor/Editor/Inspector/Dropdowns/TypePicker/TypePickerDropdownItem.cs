using System;
using UnityEditor.IMGUI.Controls;

namespace UnityPlugins.Reflection.Editor
{
	public class TypePickerDropdownItem : AdvancedDropdownItem
	{
		public readonly Type Type;

		public TypePickerDropdownItem(Type type)
			: base(type.Name)
		{
			Type = type;
		}
	}
}
