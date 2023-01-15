#if NET7_0_OR_GREATER
using System.Numerics;

namespace AssetRipper.VersionUtilities;
partial struct UnityVersion : IComparisonOperators<UnityVersion, UnityVersion, bool>, IEqualityOperators<UnityVersion, UnityVersion, bool>, IMinMaxValue<UnityVersion>
{
	static UnityVersion IMinMaxValue<UnityVersion>.MaxValue => MaxVersion;

	static UnityVersion IMinMaxValue<UnityVersion>.MinValue => MinVersion;
}
#endif