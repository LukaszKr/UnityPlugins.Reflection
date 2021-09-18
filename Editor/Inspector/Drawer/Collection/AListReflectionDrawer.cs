using System;
using System.Collections;
using ProceduralLevel.UnityPlugins.Common.Unity;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public abstract class AListReflectionDrawer<TList> : AReflectionDrawer
		where TList : IList
	{
		private const float OPTIONS_SIZE = 24f;

		protected override object OnDraw(string label, object value, Type type)
		{
			TList list = (TList)value;
			Type elementType = GetElementType(type);

			int length = list.Count;
			Rect headerLine = Layout.GetLine();
			RectPair headerPair = headerLine.SplitHorizontal(0.7f);
			EditorGUI.LabelField(headerPair.A, $"{label} | {elementType.Name}");
			int desiredLength = EditorGUI.DelayedIntField(headerPair.B, length);
			if(length != desiredLength)
			{
				desiredLength = Math.Max(0, desiredLength);
				list = SetSize(elementType, list, desiredLength);
				length = desiredLength;
			}

			Layout.StartIndent();
			for(int x = 0; x < length; ++x)
			{
				object element = list[x];
				AReflectionDrawer drawer = Inspector.GetDrawer(elementType);
				element = drawer.Draw(Inspector, $"[{x}]", element, elementType);
				list[x] = element;
			}
			Layout.EndIndent();

			Rect optionsRect = Layout.GetLine(OPTIONS_SIZE);
			RectPair pair = optionsRect.CutRight(OPTIONS_SIZE);
			if(GUI.Button(pair.B, "+"))
			{
				list = SetSize(elementType, list, length+1);
			}

			return list;
		}

		protected abstract Type GetElementType(Type listType);
		protected abstract TList SetSize(Type elementType, TList current, int newSize);
	}
}
