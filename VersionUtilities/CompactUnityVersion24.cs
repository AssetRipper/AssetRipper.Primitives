namespace AssetRipper.VersionUtilities;

/// <summary>
/// A compact value type for efficiently storing Unity versions inside 24 bits
/// </summary>
public partial struct CompactUnityVersion24
{
	private const int majorOffset = 3;
	private const int buildOffset = 9;
	private const int typeOffset = 6;

	private const uint bitMask3 = 0x0007U;
	private const uint bitMask5 = 0x001FU;
	private const uint bitMask6 = 0x003FU;
	private const uint bitMask7 = 0x007FU;

	/// <summary>
	/// 5 bits for the major, 
	/// 3 bits for the minor
	/// </summary>
	private readonly byte m_MajorMinorByte;

	/// <summary>
	/// 7 bits for the build, 
	/// 3 bits for the type, 
	/// 6 bits for the type number
	/// </summary>
	private readonly ushort m_BuildTypeShort;

	/// <summary>
	/// 2042
	/// </summary>
	public const ushort MajorMaxValue = (ushort)(31U + 2011U);

	/// <summary>
	/// 5 bits
	/// </summary>
	private byte MajorRaw => unchecked((byte)((m_MajorMinorByte >> majorOffset) & bitMask5));

	/// <summary>
	/// The first number in a Unity version string. 5 bits.
	/// Valid values inclusive: 0 - 5, 2017 - 2042
	/// </summary>
	public ushort Major => ConvertMajorRawToNormal(MajorRaw);

	/// <summary>
	/// The second number in a Unity version string. 3 bits
	/// </summary>
	public byte Minor => unchecked((byte)(m_MajorMinorByte & bitMask3));

	/// <summary>
	/// The third number in a Unity version string. 7 bits
	/// </summary>
	public byte Build => unchecked((byte)((m_BuildTypeShort >> buildOffset) & bitMask7));

	/// <summary>
	/// The letter in a Unity version string. 3 bits
	/// </summary>
	public UnityVersionType Type => (UnityVersionType)unchecked((byte)((m_BuildTypeShort >> typeOffset) & bitMask3));

	/// <summary>
	/// The last number in a Unity version string. 6 bits
	/// </summary>
	public byte TypeNumber => unchecked((byte)(m_BuildTypeShort & bitMask6));

	/// <summary>
	/// The minimum value this type can have
	/// </summary>
	public static CompactUnityVersion24 MinVersion { get; } = new CompactUnityVersion24((byte)0, (ushort)0);

	/// <summary>
	/// The maximum value this type can have
	/// </summary>
	public static CompactUnityVersion24 MaxVersion { get; } = new CompactUnityVersion24(byte.MaxValue, ushort.MaxValue);

	/// <summary>
	/// Construct a new Unity version
	/// </summary>
	public CompactUnityVersion24(ushort major)
	{
		m_MajorMinorByte = unchecked((byte)(ConvertMajorRawToNormal(major) << majorOffset));
		m_BuildTypeShort = 0;
	}

	/// <summary>
	/// Construct a new Unity version
	/// </summary>
	public CompactUnityVersion24(ushort major, byte minor)
	{
		m_MajorMinorByte = unchecked((byte)((ConvertMajorRawToNormal(major) << majorOffset) | CastToThreeBits(minor)));
		m_BuildTypeShort = 0;
	}

	/// <summary>
	/// Construct a new Unity version
	/// </summary>
	public CompactUnityVersion24(ushort major, byte minor, byte build)
	{
		unchecked
		{
			m_MajorMinorByte = (byte)((ConvertMajorRawToNormal(major) << majorOffset) | CastToThreeBits(minor));
			m_BuildTypeShort = (ushort)(CastToSevenBits(build) << buildOffset);
		}
	}

	/// <summary>
	/// Construct a new Unity version
	/// </summary>
	public CompactUnityVersion24(ushort major, byte minor, byte build, UnityVersionType type)
	{
		unchecked
		{
			m_MajorMinorByte = (byte)((ConvertMajorRawToNormal(major) << majorOffset) | CastToThreeBits(minor));
			m_BuildTypeShort = (ushort)(
				(CastToSevenBits(build) << buildOffset) | 
				(CastToThreeBits((byte)type) << typeOffset));
		}
	}

	/// <summary>
	/// Construct a new Unity version
	/// </summary>
	public CompactUnityVersion24(ushort major, byte minor, byte build, UnityVersionType type, byte typeNumber)
	{
		unchecked
		{
			m_MajorMinorByte = (byte)((ConvertMajorRawToNormal(major) << majorOffset) | CastToThreeBits(minor));
			m_BuildTypeShort = (ushort)(
				(CastToSevenBits(build) << buildOffset) |
				(CastToThreeBits((byte)type) << typeOffset) |
				CastToSixBits(typeNumber));
		}
	}

	private CompactUnityVersion24(byte majorMinorByte, ushort buildTypeShort)
	{
		m_MajorMinorByte = majorMinorByte;
		m_BuildTypeShort = buildTypeShort;
	}

	/// <summary>
	/// Converts this to its binary representation
	/// </summary>
	public void GetBits(out byte majorMinorByte, out ushort buildTypeShort)
	{
		majorMinorByte = m_MajorMinorByte;
		buildTypeShort = m_BuildTypeShort;
	}

	/// <summary>
	/// Converts a binary representation into its respective version
	/// </summary>
	/// <param name="majorMinorByte"></param>
	/// <param name="buildTypeShort"></param>
	/// <returns>A new Unity version with the cooresponding bits</returns>
	public static CompactUnityVersion24 FromBits(byte majorMinorByte, ushort buildTypeShort) => new CompactUnityVersion24(majorMinorByte, buildTypeShort);

	private static ushort ConvertMajorRawToNormal(byte raw)
	{
		return raw < 6 ? raw : unchecked((ushort)(raw + 2011U));
	}

	private static byte ConvertMajorRawToNormal(ushort major)
	{
		if (major < 6)
		{
			return unchecked((byte)major);
		}
		else if (major >= 2017 && major <= MajorMaxValue)
		{
			return unchecked((byte)(major - 2011U));
		}
		else
		{
			throw new ArgumentOutOfRangeException(nameof(major));
		}
	}

	private static byte CastToThreeBits(byte b)
	{
		return b <= bitMask3 ? b : throw new ArgumentOutOfRangeException(nameof(b));
	}

	private static byte CastToSixBits(byte b)
	{
		return b <= bitMask6 ? b : throw new ArgumentOutOfRangeException(nameof(b));
	}

	private static byte CastToSevenBits(byte b)
	{
		return b <= bitMask7 ? b : throw new ArgumentOutOfRangeException(nameof(b));
	}

	/// <summary>
	/// Serialize the version as a string
	/// </summary>
	/// <returns>A new string like 2019.4.3f1</returns>
	public override string ToString()
	{
		return ((UnityVersion)this).ToString();
	}
}
