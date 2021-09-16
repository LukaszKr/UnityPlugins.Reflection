using ProceduralLevel.UnityPlugins.Common.Editor;
using ProceduralLevel.UnityPlugins.Reflection.Editor;
using UnityEditor;

namespace ProceduralLevel.UnityPlugins.Reflection.Samples.Editor
{
	public class ReflectionInspectorSampleEditorWindow : AExtendedEditorWindow
	{
		public const string TITLE = "Reflection Inspector";

		private ReflectionTestClass m_TestClass;

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
			m_TestClass = ReflectionInspector.Instance.DrawLayout("Test Field", m_TestClass, Width);
			EditorGUILayout.LabelField("Label After Inspector");
		}
	}
}
