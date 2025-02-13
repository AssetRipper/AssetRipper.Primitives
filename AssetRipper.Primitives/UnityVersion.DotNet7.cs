#if NET7_0_OR_GREATER
using System.Numerics;

namespace AssetRipper.Primitives;
partial struct UnityVersion :
	IComparisonOperators<UnityVersion, UnityVersion, bool>,
	IEqualityOperators<UnityVersion, UnityVersion, bool>,
	IMinMaxValue<UnityVersion>,
	IParsable<UnityVersion>
{
	static UnityVersion IMinMaxValue<UnityVersion>.MaxValue => MaxVersion;

	static UnityVersion IMinMaxValue<UnityVersion>.MinValue => MinVersion;

	static UnityVersion IParsable<UnityVersion>.Parse(string s, IFormatProvider? provider)
	{
		return Parse(s);
	}

	static bool IParsable<UnityVersion>.TryParse(string? s, IFormatProvider? provider, out UnityVersion result)
	{
		return TryParse(s, out result, out _);
	}
}
#endif