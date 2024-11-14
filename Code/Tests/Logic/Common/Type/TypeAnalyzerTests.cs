using NUnit.Framework;

namespace UnityPlugins.Reflection.Logic.Common.Types
{
	[Category(ReflectionTestsConsts.CATEGORY_ASSEMBLY)]
	internal class TypeAnalyzerTests
	{
		private TypeAnalyzer m_Analyzer;

		[ReflectionIgnore]
		private class IgnoredType
		{
			public int Field = default;
			public int Property { get; set; }
		}

		private class InheritsIgnoredType : IgnoredType
		{
			public bool Flag = default;
		}

		private class IgnoredMembersType
		{
			public int Field = default;
			public int Property { get; set; }
			public NestedType Nested = default;

			public IgnoredType IgnoredType = default;
			public InheritsIgnoredType InheritedIgnoredType = default;

			[ReflectionIgnore]
			public int IgnoredField = default;
			[ReflectionIgnore]
			public bool IgnoredProperty { get; set; }
		}

		private class NestedType
		{
			public bool Flag = true;
		}

		[SetUp]
		public void SetUp()
		{
			m_Analyzer = new TypeAnalyzer();

			m_Analyzer.Filter.Add(new ReflectionIgnoreTypeFilter());
		}

		[Test]
		public void GetEntry_Primitive()
		{
			TypeCacheEntry entry = m_Analyzer.GetEntry(typeof(int));
			AssertEntry(entry, 0, 0);
		}

		[Test]
		public void GetEntry_String()
		{
			TypeCacheEntry entry = m_Analyzer.GetEntry(typeof(string));
			AssertEntry(entry, 0, 0);
		}

		[Test]
		public void GetEntry_IgnoreAttribute_OnType()
		{
			TypeCacheEntry entry = m_Analyzer.GetEntry(typeof(IgnoredType));
			AssertEntry(entry, 0, 0);
		}

		[Test]
		public void GetEntry_IgnoreAttribute_OnInheritedType()
		{
			TypeCacheEntry entry = m_Analyzer.GetEntry(typeof(InheritsIgnoredType));
			AssertEntry(entry, 0, 0);
		}

		[Test]
		public void GetEntry_IgnoredMembersType()
		{
			TypeCacheEntry entry = m_Analyzer.GetEntry(typeof(IgnoredMembersType));
			AssertEntry(entry, 2, 1);

			Assert.AreEqual(nameof(IgnoredMembersType.Field), entry.Fields[0].Name);
			Assert.AreEqual(nameof(IgnoredMembersType.Nested), entry.Fields[1].Name);
			Assert.AreEqual(nameof(IgnoredMembersType.Property), entry.Properties[0].Name);
		}

		private void AssertEntry(TypeCacheEntry entry, int fieldCount, int propertyCount)
		{
			Assert.AreEqual(fieldCount, entry.Fields.Length);
			Assert.AreEqual(propertyCount, entry.Properties.Length);
		}

		[Test]
		public void GetEntry_IgnoreType()
		{
			m_Analyzer.IgnoreType.Ignore.Add(typeof(bool));

			TypeCacheEntry entry = m_Analyzer.GetEntry(typeof(NestedType));
			AssertEntry(entry, 0, 0);
		}
	}
}
