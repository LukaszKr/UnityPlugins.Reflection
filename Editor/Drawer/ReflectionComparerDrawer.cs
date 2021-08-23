using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Unity;
using ProceduralLevel.UnityPlugins.Comparer.Unity;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Comparer.Editor
{
	public class ReflectionComparerDrawer
	{
		private float m_LineHeight;
		private float m_Width;
		private float m_HeightOffset = 0;
		private int m_Depth;

		public void Draw(Rect rect, ObjectIssue diff)
		{
			m_LineHeight = EditorGUIUtility.singleLineHeight;
			m_HeightOffset = 0;
			m_Width = rect.width;

			GUI.BeginGroup(rect);
			if(diff != null)
			{
				Draw(diff);
			}
			GUI.EndGroup();
		}

		private void Draw(ObjectIssue diff)
		{
			EditorGUI.LabelField(GetNextRect(), diff.GetPath());
			++m_Depth;

			Draw(diff.Issues);
			Draw(diff.Nodes);

			--m_Depth;
		}

		private void Draw(List<ADetectedIssue> issues)
		{
			int issueCount = issues.Count;
			for(int x = 0; x < issueCount; ++x)
			{
				ADetectedIssue issue = issues[x];
				if(issue is IDebugPairIssue pair)
				{
					DrawIssue(issue, pair);
				}
				else if(issue is IDebugValueIssue value)
				{
					DrawIssue(issue, value);
				}
				else
				{
					DrawIssue(issue);
				}
			}
		}

		private Rect DrawKey(Rect rect, string key)
		{
			RectPair pair = rect.CutLeft(150);
			EditorGUI.LabelField(pair.A, key);
			return pair.B;
		}

		private Rect DrawName(Rect rect, string name)
		{
			RectPair pair = rect.CutLeft(100);
			EditorGUI.LabelField(pair.A, name);
			return pair.B;
		}

		private void DrawIssue(ADetectedIssue issue, IDebugPairIssue pair)
		{
			Rect rect = GetNextRect();
			rect = DrawKey(rect, issue.Key);
			rect = DrawName(rect, issue.Name);
			EditorGUI.LabelField(rect, $"{pair.DebugLeft} =/= {pair.DebugRight}");
		}

		private void DrawIssue(ADetectedIssue issue, IDebugValueIssue value)
		{
			Rect rect = GetNextRect();
			rect = DrawKey(rect, issue.Key);
			rect = DrawName(rect, issue.Name);
			EditorGUI.LabelField(rect, $"{value.DebugValue}");
		}

		private void DrawIssue(ADetectedIssue issue)
		{
			Rect rect = GetNextRect();
			rect = DrawKey(rect, issue.Key);
			rect = DrawName(rect, issue.Name);
			EditorGUI.LabelField(rect, $"{issue}");
		}

		private void Draw(List<ObjectIssue> nodes)
		{
			int nodeCount = nodes.Count;
			for(int x = 0; x < nodeCount; ++x)
			{
				Draw(nodes[x]);
			}
		}

		private Rect GetNextRect()
		{
			return GetNextRect(m_LineHeight);
		}

		private Rect GetNextRect(float height)
		{
			const int TAB_SIZE = 20;
			int tabSize = m_Depth*TAB_SIZE;
			Rect rect = new Rect(tabSize, m_HeightOffset, m_Width-tabSize, height);
			m_HeightOffset += height;
			return rect;
		}
	}
}
