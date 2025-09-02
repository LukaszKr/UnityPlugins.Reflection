using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityPlugins.Reflection.Logic
{
	public static class TypeUtility
	{
		private static readonly object[] m_ArrayArgs = new object[] { 1 };

		public static object GetDefaultValue(this Type type)
		{
			if(type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		public static object CreateInstance(this Type type)
		{
			if(type.IsArray)
			{
				return Activator.CreateInstance(type, m_ArrayArgs);
			}
			return Activator.CreateInstance(type);
		}

		public static List<Type> GetConstructableTypes(Type type)
		{
			List<Type> validTypes = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach(Assembly assembly in assemblies)
			{
				Type[] typesToCheck = assembly.GetTypes();
				foreach(Type typeToCheck in typesToCheck)
				{
					if(IsValidType(type, typeToCheck))
					{
						validTypes.Add(typeToCheck);
					}
				}
			}

			if(validTypes.Count == 0)
			{
				//temp catch-all for generics
				if(IsValidType(type, type))
				{
					validTypes.Add(type);
				}
			}

			return validTypes;
		}

		private static bool IsValidType(Type type, Type typeToCheck)
		{
			if(!typeToCheck.IsClass)
			{
				return false;
			}
			if(typeToCheck.IsAbstract)
			{
				return false;
			}
			if(!type.IsAssignableFrom(typeToCheck))
			{
				return false;
			}

			if(typeToCheck.IsArray)
			{
				return true;
			}

			ConstructorInfo constructor = typeToCheck.GetConstructor(Type.EmptyTypes);
			if(constructor == null)
			{
				if(typeToCheck.GetConstructors().Length != 0)
				{
					return false;
				}
			}

			return true;
		}
	}
}
