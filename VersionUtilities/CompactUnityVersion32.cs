namespace AssetRipper.VersionUtilities
{
	/// <summary>
	/// A compact value type for efficiently storing Unity versions inside 32 bits
	/// </summary>
	public partial struct CompactUnityVersion32
	{
		private const int majorOffset = 24;
		private const int minorOffset = 20;
		private const int buildOffset = 12;
		private const int typeOffset = 8;

		private const uint byteMask = 0x00FFU;
		private const uint bitMask4 = 0x000FU;

		private readonly uint m_data;

		/// <summary>
		/// 2266
		/// </summary>
		public const ushort MajorMaxValue = (ushort)(byte.MaxValue + 2011U);

		/// <summary>
		/// 8 bits
		/// </summary>
		private byte MajorRaw => unchecked((byte)((m_data >> majorOffset) & byteMask));

		/// <summary>
		/// The first number in a Unity version string. 8 bits.
		/// Valid values inclusive: 0 - 5, 2017 - 2266
		/// </summary>
		public ushort Major => ConvertMajorRawToNormal(MajorRaw);

		/// <summary>
		/// The second number in a Unity version string. 4 bits
		/// </summary>
		public byte Minor => unchecked((byte)((m_data >> minorOffset) & bitMask4));

		/// <summary>
		/// The third number in a Unity version string. 8 bits
		/// </summary>
		public byte Build => unchecked((byte)((m_data >> buildOffset) & byteMask));

		/// <summary>
		/// The letter in a Unity version string. 4 bits
		/// </summary>
		public UnityVersionType Type => (UnityVersionType)unchecked((byte)((m_data >> typeOffset) & bitMask4));

		/// <summary>
		/// The last number in a Unity version string. 8 bits
		/// </summary>
		public byte TypeNumber => unchecked((byte)(m_data & byteMask));

		/// <summary>
		/// The minimum value this type can have
		/// </summary>
		public static CompactUnityVersion32 MinVersion { get; } = new CompactUnityVersion32(0U);

		/// <summary>
		/// The maximum value this type can have
		/// </summary>
		public static CompactUnityVersion32 MaxVersion { get; } = new CompactUnityVersion32(uint.MaxValue);

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public CompactUnityVersion32(ushort major)
		{
			m_data = (uint)ConvertMajorRawToNormal(major) << majorOffset;
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public CompactUnityVersion32(ushort major, byte minor)
		{
			m_data = ((uint)ConvertMajorRawToNormal(major) << majorOffset) | ((uint)CastToFourBits(minor) << minorOffset);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public CompactUnityVersion32(ushort major, byte minor, byte build)
		{
			m_data = ((uint)ConvertMajorRawToNormal(major) << majorOffset) | ((uint)CastToFourBits(minor) << minorOffset) | ((uint)build << buildOffset);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public CompactUnityVersion32(ushort major, byte minor, byte build, UnityVersionType type)
		{
			m_data = ((uint)ConvertMajorRawToNormal(major) << majorOffset) | ((uint)CastToFourBits(minor) << minorOffset) | ((uint)build << buildOffset)
				| ((uint)CastToFourBits(type) << typeOffset);
		}

		/// <summary>
		/// Construct a new Unity version
		/// </summary>
		public CompactUnityVersion32(ushort major, byte minor, byte build, UnityVersionType type, byte typeNumber)
		{
			m_data = ((uint)ConvertMajorRawToNormal(major) << majorOffset) | ((uint)CastToFourBits(minor) << minorOffset) | ((uint)build << buildOffset)
				| ((uint)CastToFourBits(type) << typeOffset) | typeNumber;
		}

		private CompactUnityVersion32(uint data)
		{
			m_data = data;
		}

		/// <summary>
		/// Converts this to its binary representation
		/// </summary>
		/// <returns>An unsigned integer having the same bits as this</returns>
		public uint ToBits() => m_data;

		/// <summary>
		/// Converts a binary representation into its respective version
		/// </summary>
		/// <param name="bits">An unsigned integer having the relevant bits</param>
		/// <returns>A new Unity version with the cooresponding bits</returns>
		public static CompactUnityVersion32 FromBits(uint bits) => new CompactUnityVersion32(bits);

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

		private static byte CastToFourBits(byte b)
		{
			return b <= bitMask4 ? b : throw new ArgumentOutOfRangeException(nameof(b));
		}

		private static UnityVersionType CastToFourBits(UnityVersionType type)
		{
			return (byte)type <= bitMask4 ? type : throw new ArgumentOutOfRangeException(nameof(type));
		}
	}
}
