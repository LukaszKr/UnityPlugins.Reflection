using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Unity;
using ProceduralLevel.UnityPlugins.Reflection.Unity;
using ProceduralLevel.UnityPlugins.Common.Editor;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
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

		[MenuItem("Procedural Level/Reflection/"+TITLE)]
		public static void OpenEditorWindow()
		{
			GetWindow<ReflectionComparerEditorWindow>();
		}

		public static void Show(ReflectionComparer comparer)
		{
			Show(null, comparer);
		}

		public static void Show(ObjectIssue diff, ReflectionComparer comparer = null)
		{
			ReflectionComparerEditorWindow window = GetWindow<ReflectionComparerEditorWindow>();
			window.m_Diff = diff;
			if(comparer != null)
			{
				window.m_UnityComparer = comparer;
			}
		}

		protected override void Initialize()
		{
			ReflectionComparer comparer = new ReflectionComparer();
			List<object> listA = new List<object>() { 1, 2, 3, 2, 5, 2, 2, 3, 4, 6, 8, 9, new List<object>() { 1, 2 } };
			List<object> listB = new List<object>() { 2, 1, 3, 4, 1, 4, 4, 2, 5, 4, 2, 2, new List<object>() { 2, 3 }, 2 };
			m_Diff = comparer.Compare(listA, listB);
		}

		protected override void Draw()
		{
			DrawUnityObjectSelector();
			Rect rect = GUILayoutUtility.GetRect(Width, Height);
			rect = rect.CutBottom(rect.y).A;
			m_Drawer.Draw(rect, m_Diff);
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
