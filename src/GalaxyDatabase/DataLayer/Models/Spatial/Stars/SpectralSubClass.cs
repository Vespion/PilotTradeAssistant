using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

/// <summary>
///     The spectral subclass of a star.
/// </summary>
/// <remarks>
///     This is a value between 0 and 9. With 0 being the hottest and 9, the coolest.
/// </remarks>
public readonly struct SpectralSubClass : ISpanFormattable, IUtf8SpanFormattable,
	ISpanParsable<SpectralSubClass>,
	IComparable<SpectralSubClass>,
	IEquatable<SpectralSubClass>,
	IComparisonOperators<SpectralSubClass, SpectralSubClass, bool>
{
	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		return obj is SpectralSubClass other && Equals(other);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="SpectralSubClass" /> struct.
	/// </summary>
	/// <param name="value">The number value of the subclass</param>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value" /> is greater than 9</exception>
	public SpectralSubClass(byte value)
	{
		if (value > 9) throw new ArgumentOutOfRangeException(nameof(value), "Value must be less than or equal to 9");

		Value = value;
	}

	/// <summary>
	///     The integer value of the spectral subclass.
	/// </summary>
	public byte Value { get; }

	/// <inheritdoc />
	public string ToString(string? format, IFormatProvider? formatProvider)
	{
		return Value.ToString(format, formatProvider);
	}

	/// <inheritdoc />
	public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
		IFormatProvider? provider)
	{
		return Value.TryFormat(destination, out charsWritten, format, provider);
	}

	/// <inheritdoc />
	public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format,
		IFormatProvider? provider)
	{
		return Value.TryFormat(utf8Destination, out bytesWritten, format, provider);
	}

	/// <inheritdoc />
	public static SpectralSubClass Parse(string s, IFormatProvider? provider)
	{
		return new SpectralSubClass(byte.Parse(s, provider));
	}

	/// <inheritdoc />
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out SpectralSubClass result)
	{
		return TryParse(s.AsSpan(), provider, out result);
	}

	/// <inheritdoc />
	public static SpectralSubClass Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
	{
		return new SpectralSubClass(byte.Parse(s, provider));
	}

	/// <inheritdoc />
	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out SpectralSubClass result)
	{
		if (byte.TryParse(s, provider, out var value))
		{
			result = new SpectralSubClass(value);
			return true;
		}

		result = default;
		return false;
	}

	/// <inheritdoc />
	public int CompareTo(SpectralSubClass other)
	{
		return Value.CompareTo(other.Value);
	}

	/// <inheritdoc />
	public bool Equals(SpectralSubClass other)
	{
		return Value == other.Value;
	}

	/// <inheritdoc />
	public static bool operator ==(SpectralSubClass left, SpectralSubClass right)
	{
		return left.Value == right.Value;
	}

	/// <inheritdoc />
	public static bool operator !=(SpectralSubClass left, SpectralSubClass right)
	{
		return left.Value != right.Value;
	}

	/// <inheritdoc />
	public static bool operator >(SpectralSubClass left, SpectralSubClass right)
	{
		return left.Value > right.Value;
	}

	/// <inheritdoc />
	public static bool operator >=(SpectralSubClass left, SpectralSubClass right)
	{
		return left.Value >= right.Value;
	}

	/// <inheritdoc />
	public static bool operator <(SpectralSubClass left, SpectralSubClass right)
	{
		return left.Value < right.Value;
	}

	/// <inheritdoc />
	public static bool operator <=(SpectralSubClass left, SpectralSubClass right)
	{
		return left.Value <= right.Value;
	}
}