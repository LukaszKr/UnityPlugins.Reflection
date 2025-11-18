using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class PropertyValueSource : AValueSource
	{
		public readonly PropertyInfo Property;

		public override string Name => Property.Name;
		public override Type Type => Property.PropertyType;

		public PropertyValueSource(PropertyInfo property)
		{
			Property = property;
		}

		protected override object OnGetValue(object parent)
		{
			return Property.GetValue(parent);
		}

		protected override void OnSetValue(object parent, object value)
		{
			Property.SetValue(parent, value);
		}
	}
}
