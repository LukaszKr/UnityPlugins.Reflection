using System;
using System.Collections;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class ListReflectionDrawer : AListReflectionDrawer<IList>
	{
		protected override Type GetElementType(Type listType)
		{
			return listType.GenericTypeArguments[0];
		}

		public override bool CanDraw(Type type)
		{
			return typeof(IList).IsAssignableFrom(type);
		}

		protected override IList SetSize(Type elementType, IList current, int newSize)
		{
			int currentSize = current.Count;
			if(currentSize > newSize)
			{
				for(int x = currentSize-1; x >= newSize; --x)
				{
					current.RemoveAt(x);
				}
			}
			else
			{
				for(int x = currentSize; x < newSize; ++x)
				{
					current.Add(default);
				}
			}
			return current;
		}
	}
}
