using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AssetRipper.Primitives;

internal static partial class UnityVersionRegexes
{
#if NET35
	private const RegexOptions Options = RegexOptions.None;
#else
	private const RegexOptions Options = RegexOptions.Compiled;
#endif

	[StringSyntax("regex")]
	private const string Major = @"([0-9]+)";

	[StringSyntax("regex")]
	private const string MajorMinor = @"([0-9]+)\.([0-9]+)";

	[StringSyntax("regex")]
	private const string MajorMinorBuild = @"([0-9]+)\.([0-9]+)\.([0-9]+)";

	[StringSyntax("regex")]
	private const string Normal = @"([0-9]+)\.([0-9]+)\.([0-9]+)\.?([abcfpx])([0-9]+)((?:.|[\r\n])+)?";

	[StringSyntax("regex")]
	private const string China = @"([0-9]+)\.([0-9]+)\.([0-9]+)\.?f1c([0-9]+)((?:.|[\r\n])+)?";

#if NET7_0_OR_GREATER
	[GeneratedRegex(Major, Options)]
	public static partial Regex GetMajorRegex();
#else
	private static readonly Regex majorRegex = new Regex(Major, Options);
	public static Regex GetMajorRegex() => majorRegex;
#endif

#if NET7_0_OR_GREATER
	[GeneratedRegex(MajorMinor, Options)]
	public static partial Regex GetMajorMinorRegex();
#else
	private static readonly Regex majorMinorRegex = new Regex(MajorMinor, Options);
	public static Regex GetMajorMinorRegex() => majorMinorRegex;
#endif

#if NET7_0_OR_GREATER
	[GeneratedRegex(MajorMinorBuild, Options)]
	public static partial Regex GetMajorMinorBuildRegex();
#else
	private static readonly Regex majorMinorBuildRegex = new Regex(MajorMinorBuild, Options);
	public static Regex GetMajorMinorBuildRegex() => majorMinorBuildRegex;
#endif

#if NET7_0_OR_GREATER
	[GeneratedRegex(Normal, Options)]
	public static partial Regex GetNormalRegex();
#else
	private static readonly Regex normalRegex = new Regex(Normal, Options);
	public static Regex GetNormalRegex() => normalRegex;
#endif

#if NET7_0_OR_GREATER
	[GeneratedRegex(China, Options)]
	public static partial Regex GetChinaRegex();
#else
	private static readonly Regex chinaRegex = new Regex(China, Options);
	public static Regex GetChinaRegex() => chinaRegex;
#endif
}
