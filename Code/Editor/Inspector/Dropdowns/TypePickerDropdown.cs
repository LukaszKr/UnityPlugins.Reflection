using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.IMGUI.Controls;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class TypePickerDropdown : AdvancedDropdown
	{
		public readonly string Label;
		public readonly object Parent;
		public readonly AValueSource Source;

		private readonly Dictionary<int, Type> m_Map = new Dictionary<int, Type>();

		public TypePickerDropdown(string label, object parent, AValueSource source)
			: base(new AdvancedDropdownState())
		{
			Label = label;
			Parent = parent;
			Source = source;
		}

		protected override AdvancedDropdownItem BuildRoot()
		{
			AdvancedDropdownItem root = new AdvancedDropdownItem(Label);
			int id = 0;
			foreach(Type type in GetValidTypes(Source.Type))
			{
				AdvancedDropdownItem option = new AdvancedDropdownItem(type.Name);
				option.id = id;
				id++;
				m_Map.Add(option.id, type);
				root.AddChild(option);
			}
			return root;
		}

		protected override void ItemSelected(AdvancedDropdownItem item)
		{
			Type type = m_Map[item.id];
			object instance = Activator.CreateInstance(type);
			Source.SetValue(Parent, instance);
		}

		protected static IEnumerable<Type> GetValidTypes(Type type)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach(Assembly assembly in assemblies)
			{
				Type[] typesToCheck = assembly.GetTypes();
				foreach(Type typeToCheck in typesToCheck)
				{
					if(!typeToCheck.IsClass)
					{
						continue;
					}
					if(typeToCheck.IsAbstract)
					{
						continue;
					}
					if(!type.IsAssignableFrom(typeToCheck))
					{
						continue;
					}
					ConstructorInfo constructor = typeToCheck.GetConstructor(Type.EmptyTypes);
					if(constructor == null)
					{
						if(typeToCheck.GetConstructors().Length != 0)
						{
							continue;
						}
					}
					yield return typeToCheck;
				}
			}
		}
	}
}
