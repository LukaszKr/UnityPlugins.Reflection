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

		public object GetValue(object parent)
		{
			try
			{
				return OnGetValue(parent);
			}
			catch(Exception e)
			{
				throw new ValueSourceException(this, e);
			}
		}

		public void SetValue(object parent, object value)
		{
			try
			{
				OnSetValue(parent, value);
			}
			catch(Exception e)
			{
				throw new ValueSourceException(this, e);
			}
		}


		protected abstract object OnGetValue(object parent);
		protected abstract void OnSetValue(object parent, object value);

		public TValue GetValue<TValue>(object parent)
		{
			return (TValue)GetValue(parent);
		}
	}
}
