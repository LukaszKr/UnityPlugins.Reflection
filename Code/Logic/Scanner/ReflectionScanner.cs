using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Reflection.Logic
{
	public class ReflectionScanner
	{
		public TypeAnalyzer Analyzer = new TypeAnalyzer();
		public readonly List<IScannerFilter> Filters = new List<IScannerFilter>();
		public readonly List<IScannerVisitor> Visitors = new List<IScannerVisitor>();

		private readonly HashSet<object> m_Visited = new HashSet<object>();

		public ReflectionScanner()
		{
		}

		public void Scan(Type type)
		{
			if(!IsValid(type))
			{
				return;
			}
			ScanType(type, null, null);
		}

		public void Scan(object value)
		{
			Scan(null, value);
			m_Visited.Clear();
		}

		private void Scan(object parent, object value)
		{
			if(value == null)
			{
				return;
			}
			Type type = value.GetType();
			if(!IsValid(type))
			{
				return;
			}

			if(type.IsPrimitive || type.IsString())
			{
				Visit(new ScannerVisitData(parent, value));
				return;
			}

			if(!m_Visited.Add(value))
			{
				return;
			}

			Visit(new ScannerVisitData(parent, value));

			if(value is IDictionary dictionary)
			{
				foreach(object dictionaryValue in dictionary.Values)
				{
					Scan(parent, dictionaryValue);
				}
				return;
			}

			if(value is IEnumerable enumerable)
			{
				foreach(object enumerableValue in enumerable)
				{
					Scan(parent, enumerableValue);
				}
				return;
			}

			ScanType(type, parent, value);
		}

		private void ScanType(Type type, object parent, object value)
		{
			TypeCacheEntry entry = Analyzer.GetEntry(type);
			foreach(FieldInfo field in entry.Fields)
			{
				object fieldValue = field.GetValue(value);
				Scan(parent, fieldValue);
			}

			foreach(PropertyInfo property in entry.Properties)
			{
				object propertyValue = property.GetValue(value);
				Scan(parent, propertyValue);
			}
		}

		private bool IsValid(Type type)
		{
			int count = Filters.Count;
			for(int x = 0; x < count; ++x)
			{
				IScannerFilter filter = Filters[x];
				if(!filter.IsValid(type))
				{
					return false;
				}
			}
			return true;
		}

		private void Visit(ScannerVisitData data)
		{
			int count = Visitors.Count;
			for(int x = 0; x < count; ++x)
			{
				IScannerVisitor user = Visitors[x];
				user.Visit(data);
			}
		}
	}
}
