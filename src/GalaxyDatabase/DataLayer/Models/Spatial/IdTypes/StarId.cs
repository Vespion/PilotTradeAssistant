using PTA.GalaxyDatabase.DataLayer.Models.IdTypes;
using Strongly;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.IdTypes;

/// <summary>
///     ID for a star.
/// </summary>
[Strongly(StronglyType.Long,
	StronglyConverter.TypeConverter | StronglyConverter.EfValueConverter | StronglyConverter.SystemTextJson)]
public readonly partial struct StarId : IPrimitiveId<StarId, long>;