using System;

namespace ProceduralLevel.UnityPlugins.Comparer.Unity
{
	public class ExceptionIssue : ADetectedIssue, IDebugValueIssue
	{
		public readonly Exception Exception;

		public override string Name => "Exception";
		public string DebugValue => Exception.ToString();

		public ExceptionIssue(ObjectIssue parent, string key, Exception exception)
			: base(parent, key)
		{
			Exception = exception;
		}

		protected override string ToStringImpl()
		{
			return Exception.ToString();
		}
	}
}
