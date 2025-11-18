using System;
using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Comparer.Handlers
{
	internal abstract class AComparerHandlerTests<THandler>
		where THandler : AComparerHandler
	{
		public class CanHandleTest
		{
			public readonly Type Type;
			public readonly bool CanHandle;

			public CanHandleTest(Type type, bool canHandle)
			{
				Type = type;
				CanHandle = canHandle;
			}

			public void Run(THandler handler)
			{
				Assert.AreEqual(CanHandle, handler.CanHandle(Type));
			}

			public override string ToString()
			{
				if(CanHandle)
				{
					return $"Can handle {Type.Name}";
				}
				return $"Can't handle {Type.Name}";
			}
		}

		public abstract void CanHandle(CanHandleTest test);

		public abstract void Same();

		public abstract void Null_Both();
		public abstract void Null_Left();
		public abstract void Null_Right();

		protected ComparisionGroup CompareValues<T>(AComparerHandler handler, object left, object right, bool isDifferent)
		{
			ComparisionGroup group = new ComparisionGroup(null, "", left, right);
			handler.Compare(group, typeof(T));
			Assert.AreEqual(isDifferent, group.Entries.Count != 0 || group.SubResults.Count != 0);
			return group;
		}
	}
}
