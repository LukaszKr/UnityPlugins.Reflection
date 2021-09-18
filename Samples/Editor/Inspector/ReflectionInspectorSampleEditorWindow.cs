using ProceduralLevel.UnityPlugins.Common.Editor;
using ProceduralLevel.UnityPlugins.Reflection.Editor;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Samples.Editor
{
	public class ReflectionInspectorSampleEditorWindow : AExtendedEditorWindow
	{
		public const string TITLE = "Reflection Inspector";

		private ReflectionTestClass m_TestClass;
		private Vector2 m_Scroll;

		public override string Title => TITLE;

		[MenuItem("Procedural Level/Reflection/"+TITLE)]
		public static void OpenEditorWindow()
		{
			GetWindow<ReflectionInspectorSampleEditorWindow>();
		}

		protected override void Initialize()
		{
			m_TestClass = new ReflectionTestClass();
		}

		protected override void Draw()
		{
			m_Scroll = EditorGUILayout.BeginScrollView(m_Scroll);
			m_TestClass = ReflectionInspector.Instance.DrawLayout("Test Field", m_TestClass, Width-20f);
			EditorGUILayout.EndScrollView();
		}
	}
}
