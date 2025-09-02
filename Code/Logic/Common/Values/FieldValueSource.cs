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

		protected override object OnGetValue(object parent)
		{
			return Field.GetValue(parent);
		}

		protected override void OnSetValue(object parent, object value)
		{
			Field.SetValue(parent, value);
		}
	}
}
