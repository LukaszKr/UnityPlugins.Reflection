using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityPlugins.Common.Editor;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class TypePickerDropdown : ExtendedAdvancedDropdown
	{
		public readonly string Label;
		public readonly object Parent;
		public readonly AValueSource Source;
		public readonly IEnumerable<Type> Types;

		public TypePickerDropdown(string label, object parent, AValueSource source, IEnumerable<Type> types)
			: base(new AdvancedDropdownState())
		{
			Label = label;
			Parent = parent;
			Source = source;
			Types = types;
		}

		protected override AdvancedDropdownItem BuildRoot()
		{
			AdvancedDropdownItem root = new AdvancedDropdownItem(Label);

			foreach(Type type in Types)
			{
				TypePickerDropdownItem option = new TypePickerDropdownItem(type);
				root.AddChild(option);
			}
			return root;
		}

		protected override void ItemSelected(AdvancedDropdownItem item)
		{
			TypePickerDropdownItem typeItem = (TypePickerDropdownItem)item;
			object instance = TypeUtility.CreateInstance(typeItem.Type);
			Source.SetValue(Parent, instance);
		}
	}
}
