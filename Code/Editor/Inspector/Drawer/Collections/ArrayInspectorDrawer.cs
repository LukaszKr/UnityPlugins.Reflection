using System;

namespace UnityPlugins.Reflection.Editor
{
	public class ArrayInspectorDrawer : AListInspectorDrawer<Array>
	{
		protected override Array AddElement(Array array)
		{
			Type elementType = array.GetType().GetElementType();
			Array newArray = Array.CreateInstance(elementType, array.Length+1);
			Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
			return newArray;
		}
	}
}
