using NUnit.Framework;

namespace AssetRipper.VersionUtilities.Tests
{
	public class ComparisonTests
	{
		private static readonly UnityVersion lowValue = new UnityVersion(2017, 3, 0, UnityVersionType.Patch, 1);
		private static readonly UnityVersion mediumValue = new UnityVersion(2019, 4, 3, UnityVersionType.Final, 1);
		private static readonly UnityVersion highValue = new UnityVersion(2020, 2, 0, UnityVersionType.Final, 1);

		[Test]
		public void LessThanIsTransitive()
		{
			Assert.IsTrue(lowValue < mediumValue);
			Assert.IsTrue(mediumValue < highValue);
			Assert.IsTrue(lowValue < highValue);
		}

		[Test]
		public void GreaterThanIsTransitive()
		{
			Assert.IsTrue(mediumValue > lowValue);
			Assert.IsTrue(highValue > mediumValue);
			Assert.IsTrue(highValue > lowValue);
		}

		[Test]
		public void WeakInequalityHolds()
		{
			UnityVersion low = lowValue;
			Assert.IsTrue(lowValue <= low);
		}
	}
}
