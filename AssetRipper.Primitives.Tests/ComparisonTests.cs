namespace AssetRipper.Primitives.Tests;

public class ComparisonTests
{
	private static readonly UnityVersion lowValue = new UnityVersion(2017, 3, 0, UnityVersionType.Patch, 1);
	private static readonly UnityVersion mediumValue = new UnityVersion(2019, 4, 3, UnityVersionType.Final, 1);
	private static readonly UnityVersion highValue = new UnityVersion(2020, 2, 0, UnityVersionType.Final, 1);

	[Test]
	public void LessThanIsTransitive()
	{
		Assert.Multiple(() =>
		{
			Assert.That(lowValue, Is.LessThan(mediumValue));
			Assert.That(mediumValue, Is.LessThan(highValue));
			Assert.That(lowValue, Is.LessThan(highValue));
		});
	}

	[Test]
	public void GreaterThanIsTransitive()
	{
		Assert.Multiple(() =>
		{
			Assert.That(mediumValue, Is.GreaterThan(lowValue));
			Assert.That(highValue, Is.GreaterThan(mediumValue));
			Assert.That(highValue, Is.GreaterThan(lowValue));
		});
	}

	[Test]
	public void WeakInequalityHolds()
	{
		UnityVersion low = lowValue;
		Assert.That(lowValue, Is.LessThanOrEqualTo(low));
	}

	[Test]
	public void MinimumValueDefinition()
	{
		Assert.That(UnityVersion.MinVersion, Is.EqualTo(new UnityVersion(ushort.MinValue, ushort.MinValue, ushort.MinValue, UnityVersionType.Alpha, byte.MinValue)));
	}

	[Test]
	public void MaximumValueDefinition()
	{
		Assert.That(UnityVersion.MaxVersion, Is.EqualTo(new UnityVersion(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue, (UnityVersionType)byte.MaxValue, byte.MaxValue)));
	}

	[Test]
	public void AnIncreaseOfTypeNumberMakesInequal()
	{
		Assert.Multiple(() =>
		{
			Assert.That(UnityVersion.MinVersion, Is.LessThan(new UnityVersion(ushort.MinValue, ushort.MinValue, ushort.MinValue, UnityVersionType.Alpha, 1)));
			Assert.That(lowValue, Is.LessThan(new UnityVersion(lowValue.Major, lowValue.Minor, lowValue.Build, lowValue.Type, (byte)(lowValue.TypeNumber + 1))));
		});
	}

	[Test]
	public void AlphaZeroIsTheDefault()
	{
		Assert.That(new UnityVersion(2019, 1, 0), Is.LessThan(new UnityVersion(2019, 1, 0, UnityVersionType.Final, 1)));
	}

	[Test]
	public void ComparisonMethodsIgnoreUnspecifiedValues()
	{
		Assert.Multiple(() =>
		{
			Assert.That(new UnityVersion(2019, 1, 0, UnityVersionType.Final, 1).IsGreaterEqual(2019, 1, 0), Is.True);
			Assert.That(new UnityVersion(2019, 1, 0, UnityVersionType.Final, 1).IsLessEqual(2019, 1, 0), Is.True);
		});
	}
}
