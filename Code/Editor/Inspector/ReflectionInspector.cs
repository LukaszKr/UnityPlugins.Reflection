using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ReflectionInspector
	{
		public readonly static ReflectionInspector Instance = new ReflectionInspector();

		public readonly TypeAnalyzer Analyzer = new TypeAnalyzer();
		public readonly ReflectionDrawerManager Drawers = new ReflectionDrawerManager();

		public ReflectionInspector()
		{
			Analyzer.Filters.Add(new ReflectionIgnoreTypeFilter());

			Drawers.AddSpecificDrawer(new StringInspectorDrawer());
			Drawers.AddSpecificDrawer(new BoolInspectorDrawer());
			Drawers.AddSpecificDrawer(new SByteInspectorDrawer());
			Drawers.AddSpecificDrawer(new ByteInspectorDrawer());
			Drawers.AddSpecificDrawer(new UShortInspectorDrawer());
			Drawers.AddSpecificDrawer(new ShortInspectorDrawer());
			Drawers.AddSpecificDrawer(new UIntInspectorDrawer());
			Drawers.AddSpecificDrawer(new IntInspectorDrawer());
			Drawers.AddSpecificDrawer(new LongInspectorDrawer());
			Drawers.AddSpecificDrawer(new ULongInspectorDrawer());
			Drawers.AddSpecificDrawer(new FloatInspectorDrawer());
			Drawers.AddSpecificDrawer(new DoubleInspectorDrawer());

			Drawers.AddSpecificDrawer(new GuidInspectorDrawer());

			Drawers.AddGenericDrawer(new ObjectInspectorDrawer<object>());
			Drawers.AddGenericDrawer(new UnityObjectInspectorDrawer());
			Drawers.AddGenericDrawer(new ListInspectorDrawer());
			Drawers.AddGenericDrawer(new ArrayInspectorDrawer());
		}

		public void Draw(object value, string name)
		{
			AInspectorDrawer drawer = Drawers.GetDrawer(value.GetType());
			drawer.Draw(this, null, new StaticValueSource(value, name));
		}

		public void Draw(AValueSource source)
		{
			AInspectorDrawer drawer = Drawers.GetDrawer(source.Type);
			drawer.Draw(this, null, source);
		}
	}
}
