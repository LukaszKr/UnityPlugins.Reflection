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

		public override object GetValue(object parent)
		{
			return Property.GetValue(parent);
		}

		public override void SetValue(object parent, object value)
		{
			Property.SetValue(parent, value);
		}
	}
}
