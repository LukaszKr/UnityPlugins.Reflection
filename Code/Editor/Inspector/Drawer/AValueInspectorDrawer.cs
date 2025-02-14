﻿using System;

namespace UnityPlugins.Reflection.Editor
{
	public abstract class AValueInspectorDrawer<TValue> : AInspectorDrawer
	{
		public override bool CanDraw(Type type)
		{
			return typeof(TValue).IsAssignableFrom(type);
		}

		protected override object Draw(object parent, Type type, string label, object value)
		{
			return OnDraw(parent, label, (TValue)value);
		}

		protected abstract TValue OnDraw(object parent, string label, TValue value);
	}
}
