using System;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public abstract class AKeyValueReflectionDrawer<TValue> : AReflectionDrawer
	{
		public override bool CanDraw(Type type)
		{
			return type == typeof(TValue);
		}

		protected override object OnDraw(string label, object value, Type type)
		{
			Rect rect = Layout.GetLine();
			return OnDraw(rect, label, (TValue)value);
		}

		protected abstract TValue OnDraw(Rect rect, string label, TValue current);
	}
}
