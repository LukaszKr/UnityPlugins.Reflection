using System;

namespace ProceduralLevel.Reflection.Logic
{
	public static class ComparerUtility
	{
		public static bool IsPrimitive(Type type)
		{
			return (type.IsPrimitive || typeof(string).IsAssignableFrom(type));
		}

		public static bool IsPrimitive(object value)
		{
			if(value == null)
			{
				return false;
			}

			Type type = value.GetType();
			return IsPrimitive(type);
		}
	}
}
