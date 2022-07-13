using AssetRipper.VersionUtilities.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace AssetRipper.VersionUtilities
{
	public readonly partial struct UnityVersion
	{
		private static readonly Regex majorMinorRegex = new Regex(@"^([0-9]+)\.([0-9]+)$", RegexOptions.Compiled);
		private static readonly Regex majorMinorBuildRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)$", RegexOptions.Compiled);
		private static readonly Regex normalRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)\.?([abfpx])([0-9]+)$", RegexOptions.Compiled);
		private static readonly Regex chinaRegex = new Regex(@"^([0-9]+)\.([0-9]+)\.([0-9]+)\.?f([0-9]+)c1$", RegexOptions.Compiled);
		
		/// <summary>
		/// Serialize the version as a string
		/// </summary>
		/// <returns>A new string like 2019.4.3f1</returns>
		public override string ToString()
		{
			return ToString(false, false, false, false);
		}

		/// <summary>
		/// Serialize the version as a string
		/// </summary>
		/// <param name="hasUnderscorePrefix">Include the _ prefix</param>
		/// <param name="useUnderscoresAsSeparators">Use underscores as separators instead of periods</param>
		/// <param name="hasDllExtension">Include the .dll extension</param>
		/// <param name="hasSeparatorAfterBuild">Include a separator between <see cref="Build"/> and <see cref="Type"/></param>
		/// <returns>A new string generated with those parameters</returns>
		public string ToString(bool hasUnderscorePrefix, bool useUnderscoresAsSeparators, bool hasDllExtension, bool hasSeparatorAfterBuild)
		{
			StringBuilder sb = new();

			char separator = useUnderscoresAsSeparators ? '_' : '.';

			if(hasUnderscorePrefix)
				sb.Append('_');

			sb.Append(Major);
			sb.Append(separator);
			sb.Append(Minor);
			sb.Append(separator);
			sb.Append(Build);
			if (hasSeparatorAfterBuild)
			{
				sb.Append(separator);
			}
			if (Type == UnityVersionType.China)
			{
				sb.Append('f');
				sb.Append(TypeNumber);
				sb.Append("c1");
			}
			else
			{
				sb.Append(Type.ToCharacter());
				sb.Append(TypeNumber);
			}
			
			if(hasDllExtension)
				sb.Append(".dll");

			return sb.ToString();
		}

		/// <summary>
		/// Serialize the version as a string
		/// </summary>
		/// <returns>A new string like 2019.4.3</returns>
		public string ToStringWithoutType()
		{
			return $"{Major}.{Minor}.{Build}";
		}

		/// <summary>
		/// Parse a dll name like _2019_4_3f1.dll
		/// </summary>
		/// <param name="dllName">The name of a dll file</param>
		/// <returns>The parsed Unity version</returns>
		/// <exception cref="ArgumentNullException">If the string is null or empty</exception>
		public static UnityVersion ParseFromDllName(string dllName)
		{
			if (string.IsNullOrEmpty(dllName))
			{
				throw new ArgumentNullException(nameof(dllName));
			}
			if (dllName[0] == '_')
			{
				dllName = dllName.Substring(1);
			}

			return Parse(dllName.Replace('_', '.').Replace(".dll", ""));
		}

		/// <summary>
		/// Parse a normal Unity version string
		/// </summary>
		/// <param name="version">A string to parse</param>
		/// <returns>The parsed Unity version</returns>
		/// <exception cref="ArgumentNullException">If the string is null or empty</exception>
		/// <exception cref="ArgumentException">If the string is in an invalid format</exception>
		public static UnityVersion Parse(string version)
		{
			if (string.IsNullOrEmpty(version))
			{
				throw new ArgumentNullException(nameof(version));
			}

			if (normalRegex.TryMatch(version, out Match? match))
			{
				int major = int.Parse(match.Groups[1].Value);
				int minor = int.Parse(match.Groups[2].Value);
				int build = int.Parse(match.Groups[3].Value);
				char type = match.Groups[4].Value[0];
				int typeNumber = int.Parse(match.Groups[5].Value);
				return new UnityVersion((ushort)major, (ushort)minor, (ushort)build, type.ToUnityVersionType(), (byte)typeNumber);
			}
			else if (majorMinorBuildRegex.TryMatch(version, out match))
			{
				int major = int.Parse(match.Groups[1].Value);
				int minor = int.Parse(match.Groups[2].Value);
				int build = int.Parse(match.Groups[3].Value);
				return new UnityVersion((ushort)major, (ushort)minor, (ushort)build, UnityVersionType.Final, 0);
			}
			else if (majorMinorRegex.TryMatch(version, out match))
			{
				int major = int.Parse(match.Groups[1].Value);
				int minor = int.Parse(match.Groups[2].Value);
				return new UnityVersion((ushort)major, (ushort)minor, 0, UnityVersionType.Final, 0);
			}
			else if (chinaRegex.TryMatch(version, out match))
			{
				int major = int.Parse(match.Groups[1].Value);
				int minor = int.Parse(match.Groups[2].Value);
				int build = int.Parse(match.Groups[3].Value);
				int typeNumber = int.Parse(match.Groups[4].Value);
				return new UnityVersion((ushort)major, (ushort)minor, (ushort)build, UnityVersionType.China, (byte)typeNumber);
			}
			else
			{
				throw new ArgumentException($"Invalid version format: {version}", nameof(version));
			}
		}
	}
}
