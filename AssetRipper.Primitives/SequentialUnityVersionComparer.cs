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

	public static bool LessThanOrEquals(UnityVersion x, UnityVersion y) => Compare(x, y) <= 0;

	public static bool Equals(UnityVersion x, UnityVersion y) => x == y;

	public static bool GreaterThan(UnityVersion x, UnityVersion y) => Compare(x, y) > 0;

	public static bool GreaterThanOrEquals(UnityVersion x, UnityVersion y) => Compare(x, y) >= 0;

	private static bool UsesYearNumbering(UnityVersion version) => version.Major is >= 2017 and <= 2023;

	private const int FirstNewVersion = 6;

	int IComparer<UnityVersion>.Compare(UnityVersion x, UnityVersion y) => Compare(x, y);

	bool IEqualityComparer<UnityVersion>.Equals(UnityVersion x, UnityVersion y) => x == y;

	int IEqualityComparer<UnityVersion>.GetHashCode(UnityVersion obj) => obj.GetHashCode();
}