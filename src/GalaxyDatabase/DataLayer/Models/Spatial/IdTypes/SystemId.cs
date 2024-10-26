using PTA.GalaxyDatabase.DataLayer.Models.IdTypes;
using Strongly;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.IdTypes;

/// <summary>
///     ID for a system.
/// </summary>
[Strongly(StronglyType.Long,
	StronglyConverter.TypeConverter | StronglyConverter.EfValueConverter | StronglyConverter.SystemTextJson)]
public readonly partial struct SystemId : IPrimitiveId<SystemId, long>;