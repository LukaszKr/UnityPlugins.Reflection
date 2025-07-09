using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public class FieldValueSource : AValueSource
	{
		public readonly FieldInfo Field;

		public override string Name => Field.Name;
		public override Type Type => Field.FieldType;

		public FieldValueSource(FieldInfo field)
		{
			Field = field;
		}

		public override object GetValue(object parent)
		{
			return Field.GetValue(parent);
		}

		public override void SetValue(object parent, object value)
		{
			Field.SetValue(parent, value);
		}
	}
}
