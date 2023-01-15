using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AssetRipper.VersionUtilities.Extensions;

internal static class RegexExtensions
{
	public static bool TryMatch(this Regex regex, string input, [NotNullWhen(true)] out Match? match)
	{
		match = regex.Match(input);
		if (match.Success)
		{
			return true;
		}
		else
		{
			match = null;
			return false;
		}
	}
}
