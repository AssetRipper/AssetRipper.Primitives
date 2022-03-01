namespace AssetRipper.VersionUtilities.Extensions
{
	/// <summary>
	/// Extensions for <see cref="char"/>
	/// </summary>
	public static class CharacterExtensions
	{
		internal static int ParseDigit(this char _this)
		{
			return _this - '0';
		}

		/// <summary>
		/// Parse a character into a Unity Version Type
		/// </summary>
		/// <param name="c">The character</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">No version type for character</exception>
		public static UnityVersionType ToUnityVersionType(this char c)
		{
			return c switch
			{
				'a' => UnityVersionType.Alpha,
				'b' => UnityVersionType.Beta,
				'c' => UnityVersionType.China,
				'f' => UnityVersionType.Final,
				'p' => UnityVersionType.Patch,
				'x' => UnityVersionType.Experimental,
				_ => throw new ArgumentException($"There is no version type {c}", nameof(c)),
			};
		}
	}
}
