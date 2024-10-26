using PTA.GalaxyDatabase.DataLayer.Models.IdTypes;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial.IdTypes;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

/// <summary>
///     A known star in the galaxy.
/// </summary>
public class Star : IHaveId<StarId>
{
	/// <summary>
	///     The ID of the system this star is in.
	/// </summary>
	public SystemId SystemId { get; set; }

	/// <summary>The system this star is in</summary>
	public StarSystem? System { get; set; }

	/// <summary>The stellar classification of this star</summary>
	public StellarClass? Class { get; set; }

	/// <summary>
	///     How far is this star from the primary star in the system
	/// </summary>
	/// <remarks>
	///     A value of 0 indicates that this star *is* the primary star in a system
	/// </remarks>
	public float DistanceFromMainStar { get; set; }

	/// <summary>The ID of this star</summary>
	public StarId Id { get; set; }
}