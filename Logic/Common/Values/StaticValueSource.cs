using System;

namespace UnityPlugins.Reflection.Logic
{
	public class StaticValueSource : AValueSource
	{
		private readonly object m_Value;
		private readonly string m_Name;

		public override string Name => m_Name;
		public override Type Type => m_Value.GetType();

		public StaticValueSource(object value, string name)
		{
			m_Value = value;
			m_Name = name;
		}

		protected override object OnGetValue(object parent)
		{
			return m_Value;
		}

		protected override void OnSetValue(object parent, object value)
		{
		}
	}
}
