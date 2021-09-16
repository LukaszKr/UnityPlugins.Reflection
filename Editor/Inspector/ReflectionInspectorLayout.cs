using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionInspectorLayout
	{
		public static float LineHeight = 20f;
		public static float LineMargin = 4f;

		private Rect _current;

		public Rect Current => _current;

		public void Start(float width)
		{
			_current = new Rect(0f, 0f, width, 0f);
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
