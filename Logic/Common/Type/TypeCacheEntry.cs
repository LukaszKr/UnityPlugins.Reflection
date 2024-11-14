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
				ProcessFields(fields, type.GetFields(BindingFlags.Instance | BindingFlags.Public));
				ProcessFields(fields, type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
				ProcessProperties(properties, type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
				ProcessProperties(properties, type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic));

				Fields = fields.ToArray();
				Properties = properties.ToArray();
			}
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
