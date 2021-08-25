using System;
using System.Diagnostics;
using System.Reflection;

namespace ProceduralLevel.UnityPlugins.Reflection.Unity
{
	public static class UnityDebugExt
	{
		private static readonly Type m_LogEntries;
		private static readonly MethodInfo m_OpenFileOnSpecificLineAndColumn;

		static UnityDebugExt()
		{
			//AssemblyHelper.FindFirstMatching("LogEntries");
			m_LogEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.CoreModule");
			m_OpenFileOnSpecificLineAndColumn = m_LogEntries.GetMethod("OpenFileOnSpecificLineAndColumn", BindingFlags.Static | BindingFlags.Public);
		}

		public static void OpenFileOnSpecificLineAndColumn(StackFrame frame)
		{
			OpenFileOnSpecificLineAndColumn(frame.GetFileName(), frame.GetFileLineNumber(), frame.GetFileColumnNumber());
		}

		public static void OpenFileOnSpecificLineAndColumn(string filePath, int line, int column)
		{
			m_OpenFileOnSpecificLineAndColumn.Invoke(null, new object[] { filePath, line, column });
		}
	}
}
