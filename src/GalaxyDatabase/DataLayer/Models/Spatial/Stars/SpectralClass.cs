using System.Diagnostics.CodeAnalysis;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

/// <summary>
///     Represents the spectral classification of stars.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming",
	Justification = "The names of the spectral classes are based on the standard astronomical notation.")]
public enum SpectralClass : byte
{
	/// <summary>
	///     O-type stars are the hottest and bluest stars. They are very rare and have a short lifespan.
	/// </summary>
	O,

	/// <summary>
	///     B-type stars are very bright and blue-white in color. They are rare and have a short lifespan.
	/// </summary>
	B,

	/// <summary>
	///     A-type stars are white or bluish-white stars. They are among the more common types of stars.
	/// </summary>
	A,

	/// <summary>
	///     F-type stars are white stars that are slightly yellowish. They are common in the galaxy.
	/// </summary>
	F,

	/// <summary>
	///     G-type stars are yellowish stars, including our Sun. They are common and have a moderate lifespan.
	/// </summary>
	G,

	/// <summary>
	///     K-type stars are orange stars. They are common and have a longer lifespan than G-type stars.
	/// </summary>
	K,

	/// <summary>
	///     M-type stars are the coolest and reddest stars. They are the most common type of star in the galaxy.
	/// </summary>
	M,

	/// <summary>
	///     BSuperGiant stars are extremely bright and massive B-type stars.
	/// </summary>
	BSuperGiant,

	/// <summary>
	///     ASuperGiant stars are extremely bright and massive A-type stars.
	/// </summary>
	ASuperGiant,

	/// <summary>
	///     FSuperGiant stars are extremely bright and massive F-type stars.
	/// </summary>
	FSuperGiant,

	/// <summary>
	///     GSuperGiant stars are extremely bright and massive G-type stars.
	/// </summary>
	GSuperGiant,

	/// <summary>
	///     KGiant stars are large and bright K-type stars.
	/// </summary>
	KGiant,

	/// <summary>
	///     MGiant stars are large and bright M-type stars.
	/// </summary>
	MGiant,

	/// <summary>
	///     MSuperGiant stars are extremely bright and massive M-type stars.
	/// </summary>
	MSuperGiant,

	/// <summary>
	///     HAe stars are Herbig Ae stars, which are pre-main-sequence stars of spectral type A.
	/// </summary>
	HAe,

	/// <summary>
	///     HBe stars are Herbig Be stars, which are pre-main-sequence stars of spectral type B.
	/// </summary>
	HBe,

	/// <summary>
	///     TTS stars are T Tauri stars, which are young variable stars less than 10 million years old.
	/// </summary>
	TTS,

	/// <summary>
	///     C-type stars are carbon stars, which have a higher abundance of carbon than oxygen.
	/// </summary>
	C,

	/// <summary>
	///     CH-type stars are carbon stars with strong CH molecular bands.
	/// </summary>
	CH,

	/// <summary>
	///     CHd-type stars are carbon stars with strong CH molecular bands and hydrogen deficiency.
	/// </summary>
	CHd,

	/// <summary>
	///     CJ-type stars are carbon stars with strong CN molecular bands.
	/// </summary>
	CJ,

	/// <summary>
	///     CN-type stars are carbon stars with strong CN molecular bands.
	/// </summary>
	CN,

	/// <summary>
	///     CS-type stars are carbon stars with strong CS molecular bands.
	/// </summary>
	CS,

	/// <summary>
	///     MS-type stars are carbon stars with strong MS molecular bands.
	/// </summary>
	MS,

	/// <summary>
	///     S-type stars are stars with nearly equal amounts of carbon and oxygen.
	/// </summary>
	S,

	/// <summary>
	///     W-type stars are Wolf-Rayet stars, which are evolved, massive stars with strong stellar winds.
	/// </summary>
	W,

	/// <summary>
	///     WC-type stars are carbon-rich Wolf-Rayet stars.
	/// </summary>
	WC,

	/// <summary>
	///     WN-type stars are nitrogen-rich Wolf-Rayet stars.
	/// </summary>
	WN,

	/// <summary>
	///     WNC-type stars are Wolf-Rayet stars with both nitrogen and carbon features.
	/// </summary>
	WNC,

	/// <summary>
	///     WO-type stars are oxygen-rich Wolf-Rayet stars.
	/// </summary>
	WO,

	/// <summary>
	///     BlackHole represents a black hole.
	/// </summary>
	BlackHole,

	/// <summary>
	///     SuperMassiveBlackHole represents a supermassive black hole.
	/// </summary>
	SuperMassiveBlackHole,

	/// <summary>
	///     NutronStar represents a neutron star.
	/// </summary>
	NutronStar,

	/// <summary>
	///     D-type stars are white dwarfs.
	/// </summary>
	D,

	/// <summary>
	///     DA-type stars are white dwarfs with hydrogen lines.
	/// </summary>
	DA,

	/// <summary>
	///     DAB-type stars are white dwarfs with hydrogen and helium lines.
	/// </summary>
	DAB,

	/// <summary>
	///     DAO-type stars are white dwarfs with hydrogen and ionized helium lines.
	/// </summary>
	DAO,

	/// <summary>
	///     DAV-type stars are pulsating white dwarfs with hydrogen lines.
	/// </summary>
	DAV,

	/// <summary>
	///     DAZ-type stars are white dwarfs with hydrogen lines and metal lines.
	/// </summary>
	DAZ,

	/// <summary>
	///     DB-type stars are white dwarfs with helium lines.
	/// </summary>
	DB,

	/// <summary>
	///     DBV-type stars are pulsating white dwarfs with helium lines.
	/// </summary>
	DBV,

	/// <summary>
	///     DBZ-type stars are white dwarfs with helium lines and metal lines.
	/// </summary>
	DBZ,

	/// <summary>
	///     DC-type stars are white dwarfs with continuous spectra.
	/// </summary>
	DC,

	/// <summary>
	///     DCV-type stars are pulsating white dwarfs with continuous spectra.
	/// </summary>
	DCV,

	/// <summary>
	///     DO-type stars are white dwarfs with ionized helium lines.
	/// </summary>
	DO,

	/// <summary>
	///     DOV-type stars are pulsating white dwarfs with ionized helium lines.
	/// </summary>
	DOV,

	/// <summary>
	///     DQ-type stars are white dwarfs with carbon lines.
	/// </summary>
	DQ,

	/// <summary>
	///     DX-type stars are white dwarfs with unknown spectral features.
	/// </summary>
	DX,

	/// <summary>
	///     L-type stars are brown dwarfs with metal hydride and alkali metal lines.
	/// </summary>
	L,

	/// <summary>
	///     T-type stars are brown dwarfs with methane lines.
	/// </summary>
	T,

	/// <summary>
	///     Y-type stars are brown dwarfs with ammonia lines.
	/// </summary>
	Y
}