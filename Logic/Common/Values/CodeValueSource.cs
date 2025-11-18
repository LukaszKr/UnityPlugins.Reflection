using System;

namespace UnityPlugins.Reflection.Logic
{
	public class CodeValueSource : AValueSource
	{
		private readonly Type m_Type;
		private readonly string m_Name;
		private readonly GetDelegate m_Get;
		private readonly SetDelegate m_Set;

		public override Type Type => m_Type;
		public override string Name => m_Name;

		public delegate object GetDelegate();
		public delegate void SetDelegate(object value);

		public CodeValueSource(Type type, string name, GetDelegate get, SetDelegate set)
		{
			m_Type = type;
			m_Name = name;
			m_Get = get;
			m_Set = set;
		}

		protected override object OnGetValue(object parent)
		{
			return m_Get();
		}

		protected override void OnSetValue(object parent, object value)
		{
			m_Set(value);
		}
	}
}
