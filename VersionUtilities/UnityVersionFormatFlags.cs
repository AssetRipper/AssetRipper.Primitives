namespace AssetRipper.VersionUtilities;

/// <summary>
/// Flags used when formatting a <see cref="UnityVersion"/>
/// </summary>
[Flags]
public enum UnityVersionFormatFlags
{
	/// <summary>
	/// The default flags when formatting.
	/// </summary>
	Default = 0,
	/// <summary>
	/// Exclude <see cref="UnityVersion.Type"/>, <see cref="UnityVersion.TypeNumber"/>, and the custom engine number when formatting.
	/// </summary>
	ExcludeType = 1 << 0,
	/// <summary>
	/// Format chinese versions as 2019.4.3c1 rather than 2019.4.3f1c1
	/// </summary>
	/// <remarks>
	/// An example of the long version can be found in 'unity editor resources' and 'unity_builtin_extra' for the <see href="https://unity.cn/release-notes/full/2019/2019.4.40f1">2019.4.40f1c1</see> version.<br/>
	/// An example of the short version can be found in <see href="https://github.com/AssetRipper/AssetRipper/issues/841"/>.
	/// </remarks>
	UseShortChineseFormat = 1 << 1,
}
