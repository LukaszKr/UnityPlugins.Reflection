using UnityPlugins.Reflection.Logic;

namespace UnityPlugins.Reflection.Editor
{
	public class ReflectionInspector
	{
		public readonly TypeAnalyzer Analyzer = new TypeAnalyzer();
		public readonly ReflectionDrawerManager Drawers = new ReflectionDrawerManager();

		public ReflectionInspector()
		{
			Analyzer.Filter.Add(new ReflectionIgnoreTypeFilter());

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

			Drawers.AddGenericDrawer(new ObjectInspectorDrawer());
			Drawers.AddGenericDrawer(new UnityObjectInspectorDrawer());
		}

		public void Draw(CodeValueSource source)
		{
			AInspectorDrawer drawer = Drawers.GetDrawer(source.Type);
			drawer.Draw(this, null, source);
		}
	}
}
