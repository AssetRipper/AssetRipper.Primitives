namespace AssetRipper.VersionUtilities.Tests;

public class ParsingTests
{
	[Test]
	public void UnityVersionParsesCorrectly()
	{
		string version = "2343.4.5f7";
		UnityVersion expected = new UnityVersion(2343, 4, 5, UnityVersionType.Final, 7);
		Assert.AreEqual(expected, UnityVersion.Parse(version));
	}

	[Test]
	public void UnityVersionParsesDllCorrectly()
	{
		string dllName = "_2343_4_5f7.dll";
		UnityVersion expected = new UnityVersion(2343, 4, 5, UnityVersionType.Final, 7);
		Assert.AreEqual(expected, UnityVersion.ParseFromDllName(dllName));
	}

	[Test]
	public void UnityVersionParsesDllCorrectlyWithoutExtension()
	{
		string dllName = "_2343_4_5f7";
		UnityVersion expected = new UnityVersion(2343, 4, 5, UnityVersionType.Final, 7);
		Assert.AreEqual(expected, UnityVersion.ParseFromDllName(dllName));
	}

	[Test]
	public void UnityVersionParsesDllCorrectlyWithoutLeadingUnderscore()
	{
		string dllName = "2343_4_5f7.dll";
		UnityVersion expected = new UnityVersion(2343, 4, 5, UnityVersionType.Final, 7);
		Assert.AreEqual(expected, UnityVersion.ParseFromDllName(dllName));
	}
	
	[Test]
	public void MajorMinorOnly()
	{
		string version = "2019.4";
		UnityVersion expected = new UnityVersion(2019, 4, 0, UnityVersionType.Final, 1);
		Assert.AreEqual(expected, UnityVersion.Parse(version));
	}

	[Test]
	public void MajorMinorBuildOnly()
	{
		string version = "2019.4.3";
		UnityVersion expected = new UnityVersion(2019, 4, 3, UnityVersionType.Final, 1);
		Assert.AreEqual(expected, UnityVersion.Parse(version));
	}

	[Test]
	public void ChinaVersionString()
	{
		string version = "2020.3.15f2c1";
		UnityVersion expected = new UnityVersion(2020, 3, 15, UnityVersionType.China, 2);
		Assert.Multiple(() =>
		{
			Assert.AreEqual(expected, UnityVersion.Parse(version));
			Assert.AreEqual(version, expected.ToString());
		});
	}
}
