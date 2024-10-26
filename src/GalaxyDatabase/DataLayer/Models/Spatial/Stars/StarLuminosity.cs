namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

/// <summary>
///     Represents the luminosity classification of stars.
/// </summary>
public enum StarLuminosity : byte
{
	/// <summary>
	///     Luminosity class I: Supergiants.
	/// </summary>
	I,

	/// <summary>
	///     Luminosity class Ia: Luminous supergiants.
	/// </summary>
	Ia,

	/// <summary>
	///     Luminosity class Iab: Intermediate luminous supergiants.
	/// </summary>
	Iab,

	/// <summary>
	///     Luminosity class Ib: Less luminous supergiants.
	/// </summary>
	Ib,

	/// <summary>
	///     Luminosity class II: Bright giants.
	/// </summary>
	II,

	/// <summary>
	///     Luminosity class III: Giants.
	/// </summary>
	III,

	/// <summary>
	///     Luminosity class IV: Subgiants.
	/// </summary>
	IV,

	/// <summary>
	///     Luminosity class V: Main-sequence stars (dwarfs).
	/// </summary>
	V,

	/// <summary>
	///     Luminosity class Va: Main-sequence stars with slightly higher luminosity.
	/// </summary>
	Va,

	/// <summary>
	///     Luminosity class Vab: Main-sequence stars with intermediate luminosity.
	/// </summary>
	Vab,

	/// <summary>
	///     Luminosity class Vb: Main-sequence stars with slightly lower luminosity.
	/// </summary>
	Vb,

	/// <summary>
	///     Luminosity class Vz: Zero-age main-sequence stars.
	/// </summary>
	Vz,

	/// <summary>
	///     Luminosity class VI: Subdwarfs.
	/// </summary>
	VI,

	/// <summary>
	///     Luminosity class VII: White dwarfs.
	/// </summary>
	VII
}