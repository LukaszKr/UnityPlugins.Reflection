using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public static class AssemblyHelper
	{
		public static List<Type> GetAllAssignableTo<TBaseClass>()
		{
			return GetAllAssignableTo(typeof(TBaseClass));
		}

		public static List<Type> GetAllAssignableTo(Type baseType)
		{

			List<Type> validTypes = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for(int x = 0; x < assemblies.Length; ++x)
			{
				Assembly assembly = assemblies[x];
				Type[] types = assembly.GetTypes();
				for(int y = 0; y < types.Length; ++y)
				{
					Type type = types[y];
					if(baseType.IsAssignableFrom(type))
					{
						validTypes.Add(type);
					}
				}
			}
			return validTypes;
		}

		public static List<Type> GetAllWithAttribute<TAttribute>()
			where TAttribute : Attribute
		{
			List<Type> validTypes = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for(int x = 0; x < assemblies.Length; ++x)
			{
				Assembly assembly = assemblies[x];
				Type[] types = assembly.GetTypes();
				for(int y = 0; y < types.Length; ++y)
				{
					Type type = types[y];
					if(type.GetCustomAttribute<TAttribute>() != null)
					{
						validTypes.Add(type);
					}
				}
			}
			return validTypes;
		}

		public static Type FindFirstMatching(string typeName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for(int x = 0; x < assemblies.Length; ++x)
			{
				Assembly assembly = assemblies[x];
				Type[] types = assembly.GetTypes();
				for(int y = 0; y < types.Length; ++y)
				{
					Type type = types[y];
					if(type.Name == typeName)
					{
						return type;
					}
				}
			}

			return null;
		}
	}
}
