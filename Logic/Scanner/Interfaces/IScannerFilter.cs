using System;

namespace UnityPlugins.Reflection.Logic
{
	public interface IScannerFilter
	{
		bool IsValid(Type type);
	}
}
