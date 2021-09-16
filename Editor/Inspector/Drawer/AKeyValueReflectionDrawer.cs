using System;
using System.Reflection;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public abstract class AKeyValueReflectionDrawer<TValue> : AReflectionDrawer
	{
		public override bool CanDraw(Type type)
		{
			return type == typeof(TValue);
		}

		protected override void OnDraw(object obj, FieldInfo field)
		{
			Rect rect = Layout.GetLine();
			TValue current = (TValue)field.GetValue(obj);
			current = OnDraw(rect, field.Name, current);
			field.SetValue(obj, current);
		}

		protected abstract TValue OnDraw(Rect rect, string label, TValue current);
	}
}
