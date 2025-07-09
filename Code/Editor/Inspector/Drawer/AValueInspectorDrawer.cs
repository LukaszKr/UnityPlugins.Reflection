using System;
using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public abstract class AValueInspectorDrawer<TValue> : AInspectorDrawer
	{
		public override bool CanDraw(Type type)
		{
			return typeof(TValue).IsAssignableFrom(type);
		}

		protected override void Draw(object parent, AValueSource source)
		{
			TValue current = source.GetValue<TValue>(parent);
			Draw(parent, source, current);
		}

		protected abstract void Draw(object parent, AValueSource source, TValue current);
	}
}
