using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace AssetRipper.VersionUtilities.Tests;

public class ParsingTests
{
	[TestCase("4.2.2f1", 4, 2, 2, UnityVersionType.Final, 1)]
	[TestCase("2343.4.5f7", 2343, 4, 5, UnityVersionType.Final, 7)]
	public void UnityVersionParsesCorrectly(string version, int major, int minor, int build, UnityVersionType type, int typeNumber)
	{
		UnityVersion expected = new UnityVersion((ushort)major, (ushort)minor, (ushort)build, type, (byte)typeNumber);
		Assert.AreEqual(expected, UnityVersion.Parse(version));
	}

	[TestCaseSource(nameof(GenerateRandomVersions), new object[] { 20 })]
	public void UnityVersionToStringParsesCorrectly(UnityVersion version)
	{
		string versionString = version.ToString();
		UnityVersion parsedVersion = UnityVersion.Parse(versionString);
		Assert.AreEqual(version, parsedVersion);
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
		//This is an actual version from a game.
		//Versions like 2019.3.0f2 and 2020.3.15f2 don't have corresponding Chinese versions.
		//Source: https://unity.cn/releases
		string version = "2021.3.11f1c2";
		UnityVersion expected = new UnityVersion(2021, 3, 11, UnityVersionType.China, 2);
		Assert.Multiple(() =>
		{
			Assert.AreEqual(expected, UnityVersion.Parse(version));
			Assert.AreEqual(version, expected.ToString());
		});
	}

	private static IEnumerable<UnityVersion> GenerateRandomVersions(int count)
	{
		Randomizer random = TestContext.CurrentContext.Random;
		for (int i = 0; i < count; i++)
		{
			yield return new UnityVersion(random.NextUShort(), random.NextUShort(), random.NextUShort(), random.NextEnum<UnityVersionType>(), random.NextByte());
		}
	}
}
