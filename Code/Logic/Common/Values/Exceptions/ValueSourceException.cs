using System;

namespace UnityPlugins.Reflection.Logic
{
	public class ValueSourceException : Exception
	{
		public readonly AValueSource ValueSource;
		public readonly Exception Exception;

		public ValueSourceException(AValueSource valueSource, Exception exception)
			: base($"{valueSource.Name} | {exception}")
		{
			ValueSource = valueSource;
			Exception = exception;
		}
	}
}
