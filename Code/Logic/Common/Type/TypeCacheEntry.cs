using System;
using System.Collections.Generic;
using System.Reflection;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Reflection.Logic
{
	public class TypeCacheEntry
	{
		public readonly TypeAnalyzer Analyzer;
		public readonly Type Type;

		public readonly FieldInfo[] Fields;
		public readonly PropertyInfo[] Properties;

		public TypeCacheEntry(TypeAnalyzer analyzer, Type type)
		{
			Analyzer = analyzer;
			Type = type;

			if(Type.IsPrimitive || Type.IsString() || !Analyzer.IsValid(type))
			{
				Fields = Array.Empty<FieldInfo>();
				Properties = Array.Empty<PropertyInfo>();
			}
			else
			{
				List<FieldInfo> fields = new List<FieldInfo>();
				List<PropertyInfo> properties = new List<PropertyInfo>();
				if(analyzer.IncludeInstance)
				{
					ProcessFields(type, fields, BindingFlags.Instance);
					ProcessProperties(type, properties, BindingFlags.Instance);
				}
				if(analyzer.IncludeStatic)
				{
					ProcessFields(type, fields, BindingFlags.Static);
					ProcessProperties(type, properties, BindingFlags.Static);
				}

				Fields = fields.ToArray();
				Properties = properties.ToArray();
			}
		}

		private void ProcessFields(Type type, List<FieldInfo> fields, BindingFlags flags)
		{
			ProcessFields(fields, type.GetFields(flags | BindingFlags.Public));
			ProcessFields(fields, type.GetFields(flags | BindingFlags.NonPublic));
		}

		private void ProcessProperties(Type type, List<PropertyInfo> properties, BindingFlags flags)
		{
			ProcessProperties(properties, type.GetProperties(flags | BindingFlags.Public));
			ProcessProperties(properties, type.GetProperties(flags | BindingFlags.NonPublic));
		}

		private void ProcessFields(List<FieldInfo> buffer, FieldInfo[] fields)
		{
			foreach(FieldInfo field in fields)
			{
				if(!Analyzer.IsValid(field))
				{
					continue;
				}
				buffer.Add(field);
			}
		}

		private void ProcessProperties(List<PropertyInfo> buffer, PropertyInfo[] properties)
		{
			foreach(PropertyInfo property in properties)
			{
				if(!Analyzer.IsValid(property))
				{
					continue;
				}
				buffer.Add(property);
			}
		}

		public override string ToString()
		{
			return $"[{Type.Name}, {nameof(Fields)}: {Fields.Length}, {nameof(Properties)}: {Properties.Length}]";
		}
	}
}
