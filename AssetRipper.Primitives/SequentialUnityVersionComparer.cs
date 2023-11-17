using System.Collections.Generic;

namespace AssetRipper.Primitives;

/// <summary>
/// A comparer for <see cref="UnityVersion"/> that sorts versions sequentially.
/// Due to changes in Unity's versioning scheme, 2017 - 2023 are between 5 and 6.
/// </summary>
public sealed class SequentialUnityVersionComparer : IComparer<UnityVersion>, IEqualityComparer<UnityVersion>
{
	public static SequentialUnityVersionComparer Instance { get; } = new SequentialUnityVersionComparer();

	private SequentialUnityVersionComparer()
	{
	}

	public static int Compare(UnityVersion x, UnityVersion y)
	{
		if (UsesYearNumbering(x))
		{
			if (UsesYearNumbering(y))
			{
				return x.CompareTo(y);
			}
			else
			{
				return y.Major < FirstNewVersion ? -1 : 1;
			}
		}
		else
		{
			if (UsesYearNumbering(y))
			{
				return x.Major < FirstNewVersion ? -1 : 1;
			}
			else
			{
				return x.CompareTo(y);
			}
		}
	}

	public static bool LessThan(UnityVersion x, UnityVersion y) => Compare(x, y) < 0;

	public static bool LessThan(UnityVersion value, ushort major) => LessThan(value, new UnityVersion(major, ushort.MinValue, ushort.MinValue, UnityVersionTypeMinValue, byte.MinValue));

	public static bool LessThan(UnityVersion value, ushort major, ushort minor) => LessThan(value, new UnityVersion(major, minor, ushort.MinValue, UnityVersionTypeMinValue, byte.MinValue));

	public static bool LessThan(UnityVersion value, ushort major, ushort minor, ushort build) => LessThan(value, new UnityVersion(major, minor, build, UnityVersionTypeMinValue, byte.MinValue));

	public static bool LessThan(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type) => LessThan(value, new UnityVersion(major, minor, build, type, byte.MinValue));

	public static bool LessThan(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => LessThan(value, new UnityVersion(major, minor, build, type, typeNumber));

	public static bool LessThanOrEquals(UnityVersion x, UnityVersion y) => Compare(x, y) <= 0;

	public static bool LessThanOrEquals(UnityVersion value, ushort major) => LessThanOrEquals(value, new UnityVersion(major, ushort.MaxValue, ushort.MaxValue, UnityVersionTypeMaxValue, byte.MaxValue));

	public static bool LessThanOrEquals(UnityVersion value, ushort major, ushort minor) => LessThanOrEquals(value, new UnityVersion(major, minor, ushort.MaxValue, UnityVersionTypeMaxValue, byte.MaxValue));

	public static bool LessThanOrEquals(UnityVersion value, ushort major, ushort minor, ushort build) => LessThanOrEquals(value, new UnityVersion(major, minor, build, UnityVersionTypeMaxValue, byte.MaxValue));

	public static bool LessThanOrEquals(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type) => LessThanOrEquals(value, new UnityVersion(major, minor, build, type, byte.MaxValue));

	public static bool LessThanOrEquals(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => LessThanOrEquals(value, new UnityVersion(major, minor, build, type, typeNumber));

	public static bool Equals(UnityVersion x, UnityVersion y) => x == y;

	public static bool Equals(UnityVersion value, ushort major) => Equals(value, new UnityVersion(major, value.Minor, value.Build, value.Type, value.TypeNumber));

	public static bool Equals(UnityVersion value, ushort major, ushort minor) => Equals(value, new UnityVersion(major, minor, value.Build, value.Type, value.TypeNumber));

	public static bool Equals(UnityVersion value, ushort major, ushort minor, ushort build) => Equals(value, new UnityVersion(major, minor, build, value.Type, value.TypeNumber));

	public static bool Equals(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type) => Equals(value, new UnityVersion(major, minor, build, type, value.TypeNumber));

	public static bool Equals(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => Equals(value, new UnityVersion(major, minor, build, type, typeNumber));

	public static bool GreaterThan(UnityVersion x, UnityVersion y) => Compare(x, y) > 0;

	public static bool GreaterThan(UnityVersion value, ushort major) => GreaterThan(value, new UnityVersion(major, ushort.MaxValue, ushort.MaxValue, UnityVersionTypeMaxValue, byte.MaxValue));

	public static bool GreaterThan(UnityVersion value, ushort major, ushort minor) => GreaterThan(value, new UnityVersion(major, minor, ushort.MaxValue, UnityVersionTypeMaxValue, byte.MaxValue));

	public static bool GreaterThan(UnityVersion value, ushort major, ushort minor, ushort build) => GreaterThan(value, new UnityVersion(major, minor, build, UnityVersionTypeMaxValue, byte.MaxValue));

	public static bool GreaterThan(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type) => GreaterThan(value, new UnityVersion(major, minor, build, type, byte.MaxValue));

	public static bool GreaterThan(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => GreaterThan(value, new UnityVersion(major, minor, build, type, typeNumber));

	public static bool GreaterThanOrEquals(UnityVersion x, UnityVersion y) => Compare(x, y) >= 0;

	public static bool GreaterThanOrEquals(UnityVersion value, ushort major) => GreaterThanOrEquals(value, new UnityVersion(major, ushort.MinValue, ushort.MinValue, UnityVersionTypeMinValue, byte.MinValue));

	public static bool GreaterThanOrEquals(UnityVersion value, ushort major, ushort minor) => GreaterThanOrEquals(value, new UnityVersion(major, minor, ushort.MinValue, UnityVersionTypeMinValue, byte.MinValue));

	public static bool GreaterThanOrEquals(UnityVersion value, ushort major, ushort minor, ushort build) => GreaterThanOrEquals(value, new UnityVersion(major, minor, build, UnityVersionTypeMinValue, byte.MinValue));

	public static bool GreaterThanOrEquals(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type) => GreaterThanOrEquals(value, new UnityVersion(major, minor, build, type, byte.MinValue));

	public static bool GreaterThanOrEquals(UnityVersion value, ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => GreaterThanOrEquals(value, new UnityVersion(major, minor, build, type, typeNumber));

	private static bool UsesYearNumbering(UnityVersion version) => version.Major is >= 2017 and <= 2023;

	private const int FirstNewVersion = 6;

	private const UnityVersionType UnityVersionTypeMinValue = byte.MinValue;

	private const UnityVersionType UnityVersionTypeMaxValue = (UnityVersionType)byte.MaxValue;

	int IComparer<UnityVersion>.Compare(UnityVersion x, UnityVersion y) => Compare(x, y);

	bool IEqualityComparer<UnityVersion>.Equals(UnityVersion x, UnityVersion y) => x == y;

	int IEqualityComparer<UnityVersion>.GetHashCode(UnityVersion obj) => obj.GetHashCode();
}