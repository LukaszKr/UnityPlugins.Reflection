using System;
using System.Collections.Generic;
using System.Reflection;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Reflection.Logic
{
	public class TypeCacheEntry
	{
		private readonly static TypeDepthBasedComparer m_Comparer = new TypeDepthBasedComparer();

		public readonly TypeAnalyzer Analyzer;
		public readonly Type Type;

		public readonly FieldValueSource[] Fields;
		public readonly PropertyValueSource[] Properties;

		public TypeCacheEntry(TypeAnalyzer analyzer, Type type)
		{
			Analyzer = analyzer;
			Type = type;

			if(Type.IsPrimitive || Type.IsString() || !Analyzer.IsValid(type))
			{
				Fields = Array.Empty<FieldValueSource>();
				Properties = Array.Empty<PropertyValueSource>();
			}
			else
			{
				List<FieldValueSource> fields = new List<FieldValueSource>();
				List<PropertyValueSource> properties = new List<PropertyValueSource>();
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

		private void ProcessFields(Type type, List<FieldValueSource> fields, BindingFlags flags)
		{
			ProcessFields(fields, type.GetFields(flags | BindingFlags.Public));
			ProcessFields(fields, type.GetFields(flags | BindingFlags.NonPublic));
		}

		private void ProcessProperties(Type type, List<PropertyValueSource> properties, BindingFlags flags)
		{
			ProcessProperties(properties, type.GetProperties(flags | BindingFlags.Public));
			ProcessProperties(properties, type.GetProperties(flags | BindingFlags.NonPublic));
		}

		private void ProcessFields(List<FieldValueSource> buffer, FieldInfo[] fields)
		{
			Array.Sort(fields, m_Comparer);
			foreach(FieldInfo field in fields)
			{
				if(!Analyzer.IsValid(field))
				{
					continue;
				}
				buffer.Add(new FieldValueSource(field));
			}
		}

		private void ProcessProperties(List<PropertyValueSource> buffer, PropertyInfo[] properties)
		{
			Array.Sort(properties, m_Comparer);
			foreach(PropertyInfo property in properties)
			{
				if(!Analyzer.IsValid(property))
				{
					continue;
				}
				buffer.Add(new PropertyValueSource(property));
			}
		}

		public override string ToString()
		{
			return $"[{Type.Name}, {nameof(Fields)}: {Fields.Length}, {nameof(Properties)}: {Properties.Length}]";
		}
	}
}
