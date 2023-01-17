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
		string version = "2021.3.11f1c2";//This is an actual version from a game.
										 //Versions like 2019.3.0f2 and 2020.3.15f2 don't have corresponding Chinese versions.
										 //Source: https://unity.cn/releases
		UnityVersion expected = new UnityVersion(2021, 3, 11, UnityVersionType.China, 2);
		Assert.Multiple(() =>
		{
			Assert.AreEqual(expected, UnityVersion.Parse(version));
			Assert.AreEqual(version, expected.ToString());
		});
	}
}
