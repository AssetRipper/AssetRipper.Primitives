using NUnit.Framework;

namespace AssetRipper.VersionUtilities.Tests
{
	public class RangeTests
	{
		private static readonly UnityVersion lowerBound = new UnityVersion(2017, 3, 0, UnityVersionType.Patch, 1);
		private static readonly UnityVersion intermediateValue = new UnityVersion(2019, 4, 3, UnityVersionType.Final, 1);
		private static readonly UnityVersion upperBound = new UnityVersion(2020, 2, 0, UnityVersionType.Final, 1);

		private static readonly UnityVersionRange lowerInclusiveUpperInclusiveRange = new UnityVersionRange(lowerBound, upperBound, true, true);
		private static readonly UnityVersionRange lowerInclusiveUpperExclusiveRange = new UnityVersionRange(lowerBound, upperBound, true, false);
		private static readonly UnityVersionRange lowerExclusiveUpperInclusiveRange = new UnityVersionRange(lowerBound, upperBound, false, true);
		private static readonly UnityVersionRange lowerExclusiveUpperExclusiveRange = new UnityVersionRange(lowerBound, upperBound, false, false);
		private static readonly UnityVersionRange reversedRange = new UnityVersionRange(upperBound, lowerBound, true, true);

		[Test]
		public void IntermediateValueIsContained()
		{
			Assert.IsTrue(lowerInclusiveUpperExclusiveRange.Contains(intermediateValue));
			Assert.IsTrue(lowerExclusiveUpperInclusiveRange.Contains(intermediateValue));
		}

		[Test]
		public void ExclusiveRangeDoesntContainBoundaries()
		{
			Assert.IsFalse(lowerExclusiveUpperExclusiveRange.Contains(lowerBound));
			Assert.IsFalse(lowerExclusiveUpperExclusiveRange.Contains(upperBound));
		}

		[Test]
		public void InclusiveRangeDoesContainBoundaries()
		{
			Assert.IsTrue(lowerInclusiveUpperInclusiveRange.Contains(lowerBound));
			Assert.IsTrue(lowerInclusiveUpperInclusiveRange.Contains(upperBound));
		}

		[Test]
		public void ReversedRangeContainsNothing()
		{
			Assert.IsFalse(reversedRange.Contains(UnityVersion.MinVersion));
			Assert.IsFalse(reversedRange.Contains(lowerBound));
			Assert.IsFalse(reversedRange.Contains(intermediateValue));
			Assert.IsFalse(reversedRange.Contains(upperBound));
			Assert.IsFalse(reversedRange.Contains(UnityVersion.MaxVersion));
		}
	}
}
