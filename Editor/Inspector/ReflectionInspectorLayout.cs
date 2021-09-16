using ProceduralLevel.UnityPlugins.Common.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionInspectorLayout
	{
		public static float LineHeight = 18f;
		public static float LineMargin = 3f;
		public static float IndentSize = 20f;

		private Rect _current;

		public Rect Current => _current;

		public void Start(float width)
		{
			_current = new Rect(0f, 0f, width, 0f);
		}

		public void StartIndent()
		{
			_current = _current.CutLeft(IndentSize).B;
		}

		public void EndIndent()
		{
			_current = _current.AddLeft(IndentSize);
		}

		public Rect GetLine()
		{
			return GetLine(LineHeight);
		}

		public Rect GetLine(float height)
		{
			Rect rect = new Rect(_current.x, _current.yMax, _current.width, height);
			_current = new Rect(_current.x, _current.y, _current.width, _current.height+height+LineMargin);
			return rect;
		}
	}
}
