namespace AssetRipper.VersionUtilities
{
	public partial struct CompactUnityVersion24
	{
		/// <summary>
		/// Expand to 64 bits
		/// </summary>
		/// <param name="version">The version to convert</param>
		public static implicit operator UnityVersion(CompactUnityVersion24 version)
		{
			return new UnityVersion(version.Major, version.Minor, version.Build, version.Type, version.TypeNumber);
		}

		/// <summary>
		/// Expand to 32 bits
		/// </summary>
		/// <param name="version">The version to convert</param>
		public static implicit operator CompactUnityVersion32(CompactUnityVersion24 version)
		{
			return new CompactUnityVersion32(version.Major, version.Minor, version.Build, version.Type, version.TypeNumber);
		}

		/// <summary>
		/// Compact to 24 bits
		/// </summary>
		/// <param name="version"></param>
		public static explicit operator CompactUnityVersion24(UnityVersion version)
		{
			return new CompactUnityVersion24(version.Major, (byte)version.Minor, (byte)version.Build, version.Type, version.TypeNumber);
		}

		/// <summary>
		/// Compact to 24 bits
		/// </summary>
		/// <param name="version"></param>
		public static explicit operator CompactUnityVersion24(CompactUnityVersion32 version)
		{
			return new CompactUnityVersion24(version.Major, version.Minor, version.Build, version.Type, version.TypeNumber);
		}

		/// <summary>
		/// Equality operator
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator ==(CompactUnityVersion24 left, CompactUnityVersion24 right)
		{
			return left.m_MajorMinorByte == right.m_MajorMinorByte && left.m_BuildTypeShort == right.m_BuildTypeShort;
		}

		/// <summary>
		/// Inequality operator
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator !=(CompactUnityVersion24 left, CompactUnityVersion24 right)
		{
			return left.m_MajorMinorByte != right.m_MajorMinorByte || left.m_BuildTypeShort != right.m_BuildTypeShort;
		}

		/// <summary>
		/// Greater than
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator >(CompactUnityVersion24 left, CompactUnityVersion24 right)
		{
			return (left.m_MajorMinorByte > right.m_MajorMinorByte) ||
				(left.m_MajorMinorByte == right.m_MajorMinorByte && left.m_BuildTypeShort > right.m_BuildTypeShort);
		}

		/// <summary>
		/// Greater than or equal to
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator >=(CompactUnityVersion24 left, CompactUnityVersion24 right)
		{
			return (left.m_MajorMinorByte > right.m_MajorMinorByte) ||
				(left.m_MajorMinorByte == right.m_MajorMinorByte && left.m_BuildTypeShort >= right.m_BuildTypeShort);
		}

		/// <summary>
		/// Less than
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator <(CompactUnityVersion24 left, CompactUnityVersion24 right)
		{
			return (left.m_MajorMinorByte < right.m_MajorMinorByte) ||
				(left.m_MajorMinorByte == right.m_MajorMinorByte && left.m_BuildTypeShort < right.m_BuildTypeShort);
		}

		/// <summary>
		/// Less than or equal to
		/// </summary>
		/// <param name="left">The left Unity version</param>
		/// <param name="right">The right Unity version</param>
		/// <returns></returns>
		public static bool operator <=(CompactUnityVersion24 left, CompactUnityVersion24 right)
		{
			return (left.m_MajorMinorByte < right.m_MajorMinorByte) ||
				(left.m_MajorMinorByte == right.m_MajorMinorByte && left.m_BuildTypeShort <= right.m_BuildTypeShort);
		}
	}
}
