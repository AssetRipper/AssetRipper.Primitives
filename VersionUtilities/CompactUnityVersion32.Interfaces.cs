namespace AssetRipper.VersionUtilities;

public partial struct CompactUnityVersion32 : IEquatable<CompactUnityVersion32>, IComparable, IComparable<CompactUnityVersion32>
{
	/// <summary>
	/// Compare to another object
	/// </summary>
	/// <param name="obj">Another object</param>
	/// <returns>
	/// 1 if this is larger or the other is not a Unity version<br/>
	/// -1 if this is smaller<br/>
	/// 0 if equal
	/// </returns>
	public int CompareTo(object? obj)
	{
		return obj is CompactUnityVersion32 version ? CompareTo(version) : 1;
	}

	/// <summary>
	/// Compare two Unity versions
	/// </summary>
	/// <param name="other">Another Unity version</param>
	/// <returns>
	/// 1 if this is larger<br/>
	/// -1 if this is smaller<br/>
	/// 0 if equal
	/// </returns>
	public int CompareTo(CompactUnityVersion32 other)
	{
		if (this > other)
			return 1;
		else if (this < other)
			return -1;
		else
			return 0;
	}

	/// <summary>
	/// Check equality with another object
	/// </summary>
	/// <param name="obj">Another object</param>
	/// <returns>True if they're equal; false otherwise</returns>
	public override bool Equals(object? obj)
	{
		return obj is CompactUnityVersion32 version && this == version;
	}

	/// <summary>
	/// Check equality with another Unity version
	/// </summary>
	/// <param name="other">Another Unity version</param>
	/// <returns>True if they're equal; false otherwise</returns>
	public bool Equals(CompactUnityVersion32 other) => this == other;

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return m_data.GetHashCode();
	}
}
