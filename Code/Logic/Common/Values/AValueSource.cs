using System;

namespace UnityPlugins.Reflection.Logic
{
	public abstract class AValueSource
	{
		public abstract string Name { get; }
		public abstract Type Type { get; }

		public Type GetValueType(object parent)
		{
			object value = GetValue(parent);
			if(value != null)
			{
				return value.GetType();
			}
			return Type;
		}

		public abstract object GetValue(object parent);
		public abstract void SetValue(object parent, object value);

		public TValue GetValue<TValue>(object parent)
		{
			return (TValue)GetValue(parent);
		}
	}
}
