namespace PTA.GalaxyDatabase.DataLayer.Models.IdTypes;

/// <summary>
///     Interface for an identifier.
/// </summary>
/// <remarks>
///     <para>This interface ensures common functionality is implemented by all ID types.</para>
///     <para>ID types should be used instead of primitives in all cases to prevent 'primitive obsession'.</para>
/// </remarks>
/// <typeparam name="TSelf">A self reference to the ID type</typeparam>
public interface IId<TSelf> : IParsable<TSelf>, IFormattable, IComparable<TSelf>, IEquatable<TSelf>
	where TSelf : IId<TSelf>;