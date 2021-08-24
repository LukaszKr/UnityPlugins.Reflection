using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Unity;
using ProceduralLevel.UnityPlugins.Reflection.Unity;
using ProceduralLevel.UnityPlugins.ExtendedEditor.Editor;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionComparerDrawer
	{
		private const float MARGIN = 1f;
		private const float FULL_COLOR = 2f;
		private const float PART_COLOR = 1f;
		private const float COLOR_OFFSET = -0.5f;

		private float m_LineHeight;
		private float m_Width;
		private float m_HeightOffset = 0;
		private int m_Depth;
		private Vector2 m_Scroll;

		private ExtendedGUIStyle m_LabelStyle = new ExtendedGUIStyle("box", (s) =>
		{
			s.padding = new RectOffset();
			s.alignment = TextAnchor.UpperLeft;
			s.wordWrap = true;
		});

		private Color m_IssueColor = new Color(PART_COLOR, PART_COLOR, FULL_COLOR, 1f);
		private Color m_ObjectColor = new Color(FULL_COLOR, FULL_COLOR, PART_COLOR, 1f);
		private Color m_ValueColor = new Color(FULL_COLOR, PART_COLOR, PART_COLOR, 1f);

		public void Draw(Rect rect, ObjectIssue diff)
		{
			if(Event.current.type == EventType.Layout)
			{
				return;
			}

			m_LineHeight = EditorGUIUtility.singleLineHeight;
			float height = m_HeightOffset;
			m_HeightOffset = 0;
			m_Width = rect.width;
			m_Scroll = GUI.BeginScrollView(rect, m_Scroll, new Rect(0, 0, m_Width, height).CutRight(20).A);
			if(diff != null)
			{
				Draw(diff);
			}
			GUI.EndScrollView();
		}

		private void Draw(ObjectIssue diff)
		{
			DrawLabel(GetNextRect(), diff.Key, m_ObjectColor, false);
			++m_Depth;

			Draw(diff.Issues);
			DrawIssues(diff.Nodes);

			--m_Depth;
		}

		private void Draw(List<ADetectedIssue> issues)
		{
			int issueCount = issues.Count;
			for(int x = 0; x < issueCount; ++x)
			{
				bool oddRow = (x & 1 ) == 1;
				ADetectedIssue issue = issues[x];
				if(issue is IDebugPairIssue pair)
				{
					DrawIssue(issue, pair, oddRow);
				}
				else if(issue is IDebugValueIssue value)
				{
					DrawIssue(issue, value, oddRow);
				}
				else
				{
					DrawIssue(issue, issue.ToString(), oddRow);
				}
			}
		}

		private void DrawIssues(List<ObjectIssue> nodes)
		{
			int nodeCount = nodes.Count;
			for(int x = 0; x < nodeCount; ++x)
			{
				Draw(nodes[x]);
			}
		}

		private void DrawIssue(ADetectedIssue issue, IDebugPairIssue pair, bool oddRow)
		{
			Rect rect = GetNextRect();
			rect = DrawKey(rect, issue.Key, oddRow);
			rect = DrawType(rect, issue.Type, oddRow);

			RectPair rectPair = rect.SplitHorizontal(0.5f);
			DrawLabel(rectPair.A, pair.DebugLeft, m_ValueColor, oddRow);
			DrawLabel(rectPair.B, pair.DebugRight, m_ValueColor, oddRow);
		}

		private void DrawIssue(ADetectedIssue issue, IDebugValueIssue value, bool oddRow)
		{
			DrawIssue(issue, value.DebugValue, oddRow);
		}

		private void DrawIssue(ADetectedIssue issue, string value, bool oddRow)
		{
			GUIContent content = new GUIContent(value);
			float height = m_LabelStyle.Style.CalcHeight(content, m_Width);
			Rect rect = GetNextRect(height);
			rect = DrawKey(rect, issue.Key, oddRow);
			rect = DrawType(rect, issue.Type, oddRow);

			DrawLabel(rect, value, m_ValueColor, oddRow);
		}

		private Rect DrawKey(Rect rect, string key, bool oddRow)
		{
			RectPair pair = rect.CutLeft(150);
			DrawLabel(pair.A, key, m_IssueColor, oddRow);
			return pair.B;
		}

		private Rect DrawType(Rect rect, string name, bool oddRow)
		{
			RectPair pair = rect.CutRight(100);
			DrawLabel(pair.B, name, m_IssueColor, oddRow);
			return pair.A;
		}

		private void DrawLabel(Rect rect, string text, Color color, bool oddRow)
		{
			GUIExt.PushBackgroundColor(GetColor(color, oddRow));
			EditorGUI.LabelField(rect.AddMargin(MARGIN), text, m_LabelStyle);
			GUIExt.PopBackgroundColor();
		}

		private Color GetColor(Color baseColor, bool oddRow)
		{
			if(oddRow)
			{
				return baseColor.Offset(COLOR_OFFSET, COLOR_OFFSET, COLOR_OFFSET);
			}
			return baseColor;
		}

		private Rect GetNextRect()
		{
			return GetNextRect(m_LineHeight+MARGIN*2f);
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
