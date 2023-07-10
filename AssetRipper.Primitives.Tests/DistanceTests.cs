namespace AssetRipper.Primitives.Tests;

public class DistanceTests
{
	private static readonly UnityVersion version3 = new UnityVersion(3, 0, 0, UnityVersionType.Final, 1);
	private static readonly UnityVersion version4 = new UnityVersion(4, 4, 0, UnityVersionType.Alpha, 1);
	private static readonly UnityVersion version5 = new UnityVersion(5, 6, 1, UnityVersionType.Final, 11);
	private static readonly UnityVersion version6 = new UnityVersion(6, 0, 0, UnityVersionType.Final, 1);
	private static readonly UnityVersion version7 = new UnityVersion(7, 0, 0, UnityVersionType.Final, 1);
	private static readonly UnityVersion version8 = new UnityVersion(8, 0, 0, UnityVersionType.Final, 1);
	private static readonly UnityVersion version9 = new UnityVersion(9, 0, 0, UnityVersionType.Final, 1);
	private static readonly UnityVersion[] versionArray = new UnityVersion[] { version3, version4, version5, version6, version7, version8, version9 };

	[Test]
	public void DistanceFunctionIsSymmetric()
	{
		ulong distance1 = UnityVersion.Distance(version4, version5);
		ulong distance2 = UnityVersion.Distance(version5, version4);
		Assert.That(distance1, Is.EqualTo(distance2));
	}

	[Test]
	public void DistanceIsZeroForEquivalentVersions()
	{
		ulong distance = UnityVersion.Distance(version4, version4);
		Assert.That(distance, Is.EqualTo(0));
	}

	[Test]
	public void CorrectlyReturnsVersion6()
	{
		UnityVersion version = new UnityVersion(6, 1);
		UnityVersion closest = version.GetClosestVersion(versionArray);
		Assert.That(closest, Is.EqualTo(version6));
	}

	[Test]
	public void CorrectlyReturnsVersion5()
	{
		UnityVersion version = new UnityVersion(5, 4, 9);
		UnityVersion closest = version.GetClosestVersion(versionArray);
		Assert.That(closest, Is.EqualTo(version5));
	}
}
