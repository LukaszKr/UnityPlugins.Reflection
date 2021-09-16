using System;
using System.Collections.Generic;
using System.Reflection;
using ProceduralLevel.UnityPlugins.Common.Logic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	public class ReflectionInspector
	{
		public static readonly ReflectionInspector Instance = new ReflectionInspector();

		public readonly ReflectionInspectorLayout Layout = new ReflectionInspectorLayout();

		private readonly List<AReflectionDrawer> _builtInDrawers = new List<AReflectionDrawer>();
		private readonly NotSupportedReflectionDrawer _notSupportedDrawer = new NotSupportedReflectionDrawer();

		public ReflectionInspector()
		{
			Add(new ByteReflectionDrawer());
			Add(new SByteReflectionDrawer());
			Add(new UShortReflectionDrawer());
			Add(new ShortReflectionDrawer());
			Add(new UIntReflectionDrawer());
			Add(new IntReflectionDrawer());
			Add(new ULongReflectionDrawer());
			Add(new LongReflectionDrawer());

			Add(new FloatReflectionDrawer());
			Add(new DoubleReflectionDrawer());

			Add(new BoolReflectionDrawer());
			Add(new StringReflectionDrawer());

			Add(new ObjectReflectionDrawer());
		}

		private TReflectionDrawer Add<TReflectionDrawer>(TReflectionDrawer drawer)
			where TReflectionDrawer : AReflectionDrawer
		{
			_builtInDrawers.Add(drawer);
			return drawer;
		}

		public void DrawLayout(object inspectedObject, FieldInfo field, float width)
		{
			Rect rect = Draw(inspectedObject, field, width);
			GUILayoutUtility.GetRect(rect.width, rect.height);
		}

		public Rect Draw(object inspectedObject, FieldInfo field, float width)
		{
			Assert.IsNotNull(inspectedObject);
			Layout.Start(width);

			Type type = inspectedObject.GetType();
			Assert.IsTrue(!type.IsPrimitive);
			Assert.IsTrue(!type.IsValueType);

			AReflectionDrawer drawer = GetDrawer(type);
			drawer.Draw(this, inspectedObject, field);

			return Layout.Current;
		}

		public AReflectionDrawer GetDrawer(Type type)
		{
			int count = _builtInDrawers.Count;
			for(int x = 0; x < count; ++x)
			{
				AReflectionDrawer drawer = _builtInDrawers[x];
				if(drawer.CanDraw(type))
				{
					return drawer;
				}
			}
			return _notSupportedDrawer;
		}

		public FieldInfo[] GetFields(Type type)
		{
			return type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		}
	}
}
