namespace AssetRipper.Primitives;
partial struct UnityVersion
{
	/// <summary>
	/// Change <see cref="Major"/>.
	/// </summary>
	/// <param name="value">The new value</param>
	/// <returns>A new <see cref="UnityVersion"/> with the changed value.</returns>
	public UnityVersion ChangeMajor(ushort value) => new UnityVersion(value, Minor, Build, Type, TypeNumber);

	/// <summary>
	/// Change <see cref="Minor"/>.
	/// </summary>
	/// <param name="value">The new value</param>
	/// <returns>A new <see cref="UnityVersion"/> with the changed value.</returns>
	public UnityVersion ChangeMinor(ushort value) => new UnityVersion(Major, value, Build, Type, TypeNumber);

	/// <summary>
	/// Change <see cref="Build"/>.
	/// </summary>
	/// <param name="value">The new value</param>
	/// <returns>A new <see cref="UnityVersion"/> with the changed value.</returns>
	public UnityVersion ChangeBuild(ushort value) => new UnityVersion(Major, Minor, value, Type, TypeNumber);

	/// <summary>
	/// Change <see cref="Type"/>.
	/// </summary>
	/// <param name="value">The new value</param>
	/// <returns>A new <see cref="UnityVersion"/> with the changed value.</returns>
	public UnityVersion ChangeType(UnityVersionType value) => new UnityVersion(Major, Minor, Build, value, TypeNumber);

	/// <summary>
	/// Change <see cref="TypeNumber"/>.
	/// </summary>
	/// <param name="value">The new value</param>
	/// <returns>A new <see cref="UnityVersion"/> with the changed value.</returns>
	public UnityVersion ChangeTypeNumber(byte value) => new UnityVersion(Major, Minor, Build, Type, value);
}
