using System;
using System.Collections;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Reflection.Logic
{
	public class ArrayValueSource : AListValueSource
	{
		private readonly object m_Parent;
		private readonly AValueSource m_Source;

		public readonly Array Array;
		public readonly Type ElementType;

		public override Type Type => ElementType;

		public ArrayValueSource(object parent, AValueSource source, Array array)
		{
			m_Parent = parent;
			m_Source = source;

			Array = array;
			Type listType = array.GetType();
			ElementType = listType.GetElementType();
		}

		protected override object OnGetValue(object parent)
		{
			return Array.GetValue(Index);
		}

		protected override void OnSetValue(object parent, object value)
		{
			Array.SetValue(value, Index);
		}

		public override void AddElement()
		{
			Array newArray = Array.Resize(Array.Length+1);
			m_Source.SetValue(m_Parent, newArray);
		}

		public override void RemoveAt(int index)
		{
			for(int x = index; x < Array.Length-1; ++x)
			{
				object nextValue = Array.GetValue(x+1);
				Array.SetValue(nextValue, x);
			}
			Array newArray = Array.Resize(Array.Length-1);
			m_Source.SetValue(m_Parent, newArray);
		}
	}
}
