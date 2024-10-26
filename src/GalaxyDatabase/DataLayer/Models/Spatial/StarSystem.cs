using NetTopologySuite.Geometries;
using PTA.GalaxyDatabase.DataLayer.Models.IdTypes;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial.IdTypes;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial;

/// <summary>
///     A known system in the galaxy.
/// </summary>
public class StarSystem : IHaveId<SystemId>
{
	/// <summary>The position of this system in the galaxy</summary>
	public Point Position { get; set; } = null!;

	// /// <summary>The primary star of this system</summary>
	// public Star? PrimaryStar => Stars?.First(s => s.DistanceFromMainStar == 0);

	/// <summary>The stars in this system</summary>
	public ICollection<Star>? Stars { get; set; }

	/// <summary>The ID of this system</summary>
	public SystemId Id { get; set; }
}