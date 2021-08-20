using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ReflectionComparer
	{
		private readonly List<ADifferenceDetector> m_ValueDetectors = new List<ADifferenceDetector>();

		private readonly HashSet<int> m_VisitedObjects = new HashSet<int>();

		public ReflectionComparer()
		{
			AddDetector(new TypeDifferenceDetector());
			AddDetector(new ValueDifferenceDetector());
		}

		public void AddDetector(ADifferenceDetector detector)
		{
			m_ValueDetectors.Add(detector);
		}

		public ObjectDifference Compare(object a, object b)
		{
			try
			{
				return CompareObjects(null, "", a, b);
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				m_VisitedObjects.Clear();
			}
		}

		private ObjectDifference CompareObjects(ObjectDifference parent, string path, object objectA, object objectB)
		{
			ObjectDifference current = new ObjectDifference(parent, path);

			if(CompareValues(current, path, objectA, objectB))
			{
				return current;
			}

			if(objectA == null || objectB == null)
			{
				return current;
			}

			bool foundDiff = false;
			Type type = objectA.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			int length = fields.Length;
			for(int x = 0; x < length; ++x)
			{
				FieldInfo field = fields[x];
				object valueA = field.GetValue(objectA);
				object valueB = field.GetValue(objectB);
				if(CompareValues(current, field.Name, valueA, valueB))
				{
					foundDiff = true;
					continue;
				}

				if(!field.FieldType.IsPrimitive && !typeof(string).IsAssignableFrom(field.FieldType))
				{
					if(CanCompareObjects(valueA) && CanCompareObjects(valueB))
					{
						CompareObjects(current, field.Name, valueA, valueB);
					}
				}
			}

			if(foundDiff && current.Parent != null)
			{
				current.Parent.Nodes.Add(current);
			}
			return current;
		}

		private bool CompareValues(ObjectDifference parent, string path, object valueA, object valueB)
		{
			int count = m_ValueDetectors.Count;
			bool isDifferent = false;
			for(int x = 0; x < count; ++x)
			{
				ADifferenceDetector detector = m_ValueDetectors[x];
				ADifference difference = detector.Detect(parent, path, valueA, valueB);
				if(difference != null)
				{
					isDifferent = true;
					parent.Differences.Add(difference);
				}
			}
			return isDifferent;
		}
		private bool CanCompareObjects(object value)
		{
			if(value == null)
			{
				return true;
			}
			int hash = value.GetHashCode();
			return m_VisitedObjects.Add(hash);
		}
	}
}
