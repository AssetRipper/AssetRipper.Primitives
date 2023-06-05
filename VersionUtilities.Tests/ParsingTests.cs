﻿using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace AssetRipper.VersionUtilities.Tests;

public class ParsingTests
{
	[TestCase("4.2.2f1", 4, 2, 2, UnityVersionType.Final, 1, false, "Normal Unity 4 Version")]
	[TestCase("4.2.2f1\n1", 4, 2, 2, UnityVersionType.Final, 1, true, "Custom Unity 4 Version")]
	[TestCase("2343.4.5f7", 2343, 4, 5, UnityVersionType.Final, 7, false, "Fictitious Future Version")]
	[TestCase("2019.2.2f1c2\n2", 2019, 2, 2, UnityVersionType.China, 2, true, "Fake Custom Chinese Version")]
	[TestCase("2019.4", 2019, 4, 0, UnityVersionType.Final, 1, false, "Major Minor Only")]
	[TestCase("2019.4.3", 2019, 4, 3, UnityVersionType.Final, 1, false, "Major Minor Build Only")]
	public void UnityVersionParsesCorrectly(string versionString, int major, int minor, int build, UnityVersionType type, int typeNumber, bool customEngine, string name)
	{
		UnityVersion expected = new UnityVersion((ushort)major, (ushort)minor, (ushort)build, type, (byte)typeNumber);
		UnityVersion parsedVersion = UnityVersion.Parse(versionString, out bool parsedCustomEngine);
		Assert.Multiple(() =>
		{
			Assert.That(parsedCustomEngine, Is.EqualTo(customEngine), $"The custom engine boolean is incorrect for '{name}'");
			Assert.That(parsedVersion, Is.EqualTo(expected), $"The parsed version is incorrect for '{name}'");
		});
	}

	[TestCaseSource(nameof(GenerateRandomVersions), new object[] { 20 })]
	public void UnityVersionToStringParsesCorrectly(UnityVersion version)
	{
		string versionString = version.ToString();
		UnityVersion parsedVersion = UnityVersion.Parse(versionString, out bool customEngine);
		Assert.Multiple(() =>
		{
			Assert.That(customEngine, Is.False);
			Assert.That(parsedVersion, Is.EqualTo(version));
		});
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
			Assert.That(UnityVersion.Parse(version), Is.EqualTo(expected));
			Assert.That(expected.ToString(), Is.EqualTo(version));
		});
	}

	[Test]
	public void AlternateChinaVersionString()
	{
		//This is an actual version from a game.
		//Source: https://github.com/AssetRipper/AssetRipper/issues/841
		string version = "2017.4.40c1";
		UnityVersion expected = new UnityVersion(2017, 4, 40, UnityVersionType.China, 1);
		Assert.That(UnityVersion.Parse(version), Is.EqualTo(expected));
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
