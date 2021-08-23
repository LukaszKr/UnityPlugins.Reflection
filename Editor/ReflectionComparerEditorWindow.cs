using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Comparer.Unity;
using ProceduralLevel.UnityPlugins.ExtendedEditor.Editor;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Comparer.Editor
{
	public class ReflectionComparerEditorWindow : AExtendedEditorWindow
	{
		public const string TITLE = "Reflection Comparer";

		private ReflectionComparer m_UnityComparer = new ReflectionComparer();
		private ReflectionComparerDrawer m_Drawer = new ReflectionComparerDrawer();
		private ObjectIssue m_Diff;

		private Object m_LeftObject;
		private Object m_RightObject;

		public override string Title => TITLE;

		[MenuItem("Procedural Level/Comparer/Reflection Comparer")]
		public static void OpenEditorWindow()
		{
			GetWindow<ReflectionComparerEditorWindow>();
		}

		public static void Show(ObjectIssue diff)
		{
			ReflectionComparerEditorWindow window = GetWindow<ReflectionComparerEditorWindow>();
			window.m_Diff = diff;
		}

		protected override void Initialize()
		{
			ReflectionComparer comparer = new ReflectionComparer();
			List<int> listA = new List<int>() { 1, 2 };
			List<int> listB = new List<int>() { 2, 1, 3 };
			m_Diff = comparer.Compare(listA, listB);
		}

		protected override void Draw()
		{
			DrawUnityObjectSelector();
			m_Drawer.Draw(GUILayoutUtility.GetRect(Width, Height), m_Diff);
		}

		private void DrawUnityObjectSelector()
		{
			EditorGUILayout.BeginHorizontal("box");
			EditorGUI.BeginChangeCheck();
			m_LeftObject = EditorGUILayout.ObjectField(m_LeftObject, typeof(Object), true);
			m_RightObject = EditorGUILayout.ObjectField(m_RightObject, typeof(Object), true);
			if(EditorGUI.EndChangeCheck())
			{
				m_Diff = m_UnityComparer.Compare(m_LeftObject, m_RightObject);
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}
