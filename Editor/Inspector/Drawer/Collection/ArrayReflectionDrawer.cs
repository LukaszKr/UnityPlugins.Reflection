using System;

namespace ProceduralLevel.UnityPlugins.Reflection.Editor
{
	[CustomReflectionDrawer]
	public class ArrayReflectionDrawer : AListReflectionDrawer<Array>
	{
		protected override Type GetElementType(Type listType)
		{
			return listType.GetElementType();
		}

		public override bool CanDraw(Type type)
		{
			return type.IsArray;
		}

		protected override Array SetSize(Type elementType, Array current, int newSize)
		{
			Array newArray = Array.CreateInstance(elementType, newSize);
			int currentSize = current.Length;
			int copySize = Math.Min(currentSize, newSize);
			for(int x = 0; x < copySize; ++x)
			{
				newArray.SetValue(current.GetValue(x), x);
			}
			return newArray;
		}
	}
}
