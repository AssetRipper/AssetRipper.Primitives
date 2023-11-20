namespace AssetRipper.Primitives;

public readonly partial struct UnityVersion
{
	private const ulong subMajorMask = 0x0000FFFFFFFFFFFFUL;
	private const ulong subMinorMask = 0x00000000FFFFFFFFUL;
	private const ulong subBuildMask = 0x000000000000FFFFUL;
	private const ulong subTypeMask = 0x00000000000000FFUL;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public bool Equals(ushort major) => this == From(major);
	public bool Equals(ushort major, ushort minor) => this == From(major, minor);
	public bool Equals(ushort major, ushort minor, ushort build) => this == From(major, minor, build);
	public bool Equals(ushort major, ushort minor, ushort build, UnityVersionType type) => this == From(major, minor, build, type);
	public bool Equals(ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => this == new UnityVersion(major, minor, build, type, typeNumber);
	public bool Equals(string version) => this == Parse(version);

	public bool LessThan(ushort major) => this < From(major);
	public bool LessThan(ushort major, ushort minor) => this < From(major, minor);
	public bool LessThan(ushort major, ushort minor, ushort build) => this < From(major, minor, build);
	public bool LessThan(ushort major, ushort minor, ushort build, UnityVersionType type) => this < From(major, minor, build, type);
	public bool LessThan(ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => this < new UnityVersion(major, minor, build, type, typeNumber);
	public bool LessThan(string version) => this < Parse(version);

	public bool LessThanOrEquals(ushort major) => this <= From(major);
	public bool LessThanOrEquals(ushort major, ushort minor) => this <= From(major, minor);
	public bool LessThanOrEquals(ushort major, ushort minor, ushort build) => this <= From(major, minor, build);
	public bool LessThanOrEquals(ushort major, ushort minor, ushort build, UnityVersionType type) => this <= From(major, minor, build, type);
	public bool LessThanOrEquals(ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => this <= new UnityVersion(major, minor, build, type, typeNumber);
	public bool LessThanOrEquals(string version) => this <= Parse(version);

	public bool GreaterThan(ushort major) => this > From(major);
	public bool GreaterThan(ushort major, ushort minor) => this > From(major, minor);
	public bool GreaterThan(ushort major, ushort minor, ushort build) => this > From(major, minor, build);
	public bool GreaterThan(ushort major, ushort minor, ushort build, UnityVersionType type) => this > From(major, minor, build, type);
	public bool GreaterThan(ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => this > new UnityVersion(major, minor, build, type, typeNumber);
	public bool GreaterThan(string version) => this > Parse(version);

	public bool GreaterThanOrEquals(ushort major) => this >= From(major);
	public bool GreaterThanOrEquals(ushort major, ushort minor) => this >= From(major, minor);
	public bool GreaterThanOrEquals(ushort major, ushort minor, ushort build) => this >= From(major, minor, build);
	public bool GreaterThanOrEquals(ushort major, ushort minor, ushort build, UnityVersionType type) => this >= From(major, minor, build, type);
	public bool GreaterThanOrEquals(ushort major, ushort minor, ushort build, UnityVersionType type, byte typeNumber) => this >= new UnityVersion(major, minor, build, type, typeNumber);
	public bool GreaterThanOrEquals(string version) => this >= Parse(version);
	
	private UnityVersion From(ushort major)
	{
		ulong data = ((ulong)major << majorOffset) | subMajorMask & m_data;
		return new UnityVersion(data);
	}
	private UnityVersion From(ushort major, ushort minor)
	{
		ulong data = ((ulong)major << majorOffset) | ((ulong)minor << minorOffset) | subMinorMask & m_data;
		return new UnityVersion(data);
	}
	private UnityVersion From(ushort major, ushort minor, ushort build)
	{
		ulong data = ((ulong)major << majorOffset) | ((ulong)minor << minorOffset) | ((ulong)build << buildOffset) |
			subBuildMask & m_data;
		return new UnityVersion(data);
	}
	private UnityVersion From(ushort major, ushort minor, ushort build, UnityVersionType type)
	{
		ulong data = ((ulong)major << majorOffset) | ((ulong)minor << minorOffset) | ((ulong)build << buildOffset) |
			((ulong)(ushort)type << typeOffset) | subTypeMask & m_data;
		return new UnityVersion(data);
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
