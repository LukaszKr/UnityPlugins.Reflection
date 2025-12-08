using System;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public static class AssemblyUtility
	{
		public static Assembly FindAssembly(string name)
		{
			foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if(assembly.GetName().Name == name)
				{
					return assembly;
				}
			}

			return null;
		}
	}
}
