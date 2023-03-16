using AssetRipper.VersionUtilities.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace AssetRipper.VersionUtilities;

public readonly partial struct UnityVersion
{
	private static readonly Regex majorMinorRegex = new Regex(@"^([0-9]+)\.([0-9]+)$", RegexOptions.Compiled);
	private static readonly Regex majorMinorBuildRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)$", RegexOptions.Compiled);
	private static readonly Regex normalRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)\.?([abfpx])([0-9]+)$", RegexOptions.Compiled);
	private static readonly Regex chinaRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)\.?f1c([0-9]+)$", RegexOptions.Compiled);
	
	/// <summary>
	/// Serialize the version as a string
	/// </summary>
	/// <returns>A new string like 2019.4.3f1</returns>
	public override string ToString()
	{
		return Type == UnityVersionType.China
			? $"{Major}.{Minor}.{Build}f1c{TypeNumber}"
			: $"{Major}.{Minor}.{Build}{Type.ToCharacter()}{TypeNumber}";
	}

	/// <summary>
	/// Serialize the version as a string
	/// </summary>
	/// <returns>A new string like 2019.4.3</returns>
	public string ToStringWithoutType()
	{
		return $"{Major}.{Minor}.{Build}";
	}

	/// <summary>
	/// Parse a normal Unity version string
	/// </summary>
	/// <param name="version">A string to parse</param>
	/// <returns>The parsed Unity version</returns>
	/// <exception cref="ArgumentNullException">If the string is null or empty</exception>
	/// <exception cref="ArgumentException">If the string is in an invalid format</exception>
	public static UnityVersion Parse(string version)
	{
		if (string.IsNullOrEmpty(version))
		{
			throw new ArgumentNullException(nameof(version));
		}

		if (normalRegex.TryMatch(version, out Match? match))
		{
			int major = int.Parse(match.Groups[1].Value);
			int minor = int.Parse(match.Groups[2].Value);
			int build = int.Parse(match.Groups[3].Value);
			char type = match.Groups[4].Value[0];
			int typeNumber = int.Parse(match.Groups[5].Value);
			return new UnityVersion((ushort)major, (ushort)minor, (ushort)build, type.ToUnityVersionType(), (byte)typeNumber);
		}
		else if (majorMinorBuildRegex.TryMatch(version, out match))
		{
			int major = int.Parse(match.Groups[1].Value);
			int minor = int.Parse(match.Groups[2].Value);
			int build = int.Parse(match.Groups[3].Value);
			return new UnityVersion((ushort)major, (ushort)minor, (ushort)build, UnityVersionType.Final, 1);
		}
		else if (majorMinorRegex.TryMatch(version, out match))
		{
			int major = int.Parse(match.Groups[1].Value);
			int minor = int.Parse(match.Groups[2].Value);
			return new UnityVersion((ushort)major, (ushort)minor, 0, UnityVersionType.Final, 1);
		}
		else if (chinaRegex.TryMatch(version, out match))
		{
			int major = int.Parse(match.Groups[1].Value);
			int minor = int.Parse(match.Groups[2].Value);
			int build = int.Parse(match.Groups[3].Value);
			int typeNumber = int.Parse(match.Groups[4].Value);
			return new UnityVersion((ushort)major, (ushort)minor, (ushort)build, UnityVersionType.China, (byte)typeNumber);
		}
		else
		{
			throw new ArgumentException($"Invalid version format: {version}", nameof(version));
		}
	}
}
