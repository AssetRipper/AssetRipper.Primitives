namespace AssetRipper.Primitives;

public readonly partial struct UnityVersion : IEquatable<UnityVersion>, IComparable, IComparable<UnityVersion>
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
		return obj is UnityVersion version ? CompareTo(version) : 1;
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
	public int CompareTo(UnityVersion other)
	{
		//Unity 6 is sequentially equivalent to Unity 2024 because
		//they switched to back to the old versioning scheme.
		const ushort FirstNewVersion = 6;

		if (this == other)
		{
			return 0;
		}

		if (UsesYearNumbering(this))
		{
			if (UsesYearNumbering(other))
			{
				return CompareByData(this, other);
			}
			else
			{
				return other.Major < FirstNewVersion ? 1 : -1;
			}
		}
		else
		{
			if (UsesYearNumbering(other))
			{
				return Major < FirstNewVersion ? -1 : 1;
			}
			else
			{
				return CompareByData(this, other);
			}
		}

		static int CompareByData(UnityVersion x, UnityVersion y)
		{
			return x.m_data < y.m_data ? -1 : 1;
		}

		static bool UsesYearNumbering(UnityVersion version)
		{
			return version.Major is >= 2017 and <= 2023;
		}
	}

	/// <summary>
	/// Check equality with another object
	/// </summary>
	/// <param name="obj">Another object</param>
	/// <returns>True if they're equal; false otherwise</returns>
	public override bool Equals(object? obj)
	{
		return obj is UnityVersion version && this == version;
	}

	/// <summary>
	/// Check equality with another Unity version
	/// </summary>
	/// <param name="other">Another Unity version</param>
	/// <returns>True if they're equal; false otherwise</returns>
	public bool Equals(UnityVersion other) => this == other;

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return unchecked(827 + 911 * m_data.GetHashCode());
	}
}
