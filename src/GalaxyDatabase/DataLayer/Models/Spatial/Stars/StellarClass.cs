using System.Text.Unicode;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

/// <summary>
///     The stellar classification of a star.
/// </summary>
public class StellarClass : IUtf8SpanFormattable, ISpanFormattable
{
	/// <summary>
	///     The stellar classification of a star.
	/// </summary>
	/// <param name="spectralClass">The spectral class of the star.</param>
	/// <param name="subClass">The spectral subclass of the star.</param>
	/// <param name="luminosity">The luminosity classification of the star.</param>
	public StellarClass(SpectralClass spectralClass, byte subClass, StarLuminosity luminosity)
	{
		if (!Enum.IsDefined(spectralClass))
			throw new ArgumentOutOfRangeException(nameof(spectralClass), spectralClass,
				"The spectral class is not defined.");

		if (subClass > 9)
			throw new ArgumentOutOfRangeException(nameof(subClass), subClass,
				"The spectral subclass must be between 0 and 9.");

		if (!Enum.IsDefined(luminosity))
			throw new ArgumentOutOfRangeException(nameof(luminosity), luminosity,
				"The luminosity classification is not defined.");

		Class = spectralClass;
		SubClass = subClass;
		Luminosity = luminosity;
	}

	/// <summary>
	///     This constructor is for internal use by Entity Framework and should not be called
	/// </summary>
	[Obsolete("For use by EF core only", true)]
	[EntityFrameworkInternal]
	public StellarClass()
	{
	}

	/// <summary>The spectral class of the star.</summary>
	public SpectralClass Class { get; set; }

	/// <summary>The spectral subclass of the star.</summary>
	public byte SubClass { get; set; }

	/// <summary>The luminosity classification of the star.</summary>
	public StarLuminosity Luminosity { get; set; }

	/// <summary>
	///     Indicates whether the star is can be scooped for fuel.
	/// </summary>
	public bool CanBeScooped { get; set; }
	// public bool CanBeScooped =>
	// 	Class is
	// 		SpectralClass.O or
	// 		SpectralClass.B or
	// 		SpectralClass.A or
	// 		SpectralClass.F or
	// 		SpectralClass.G or
	// 		SpectralClass.K or
	// 		SpectralClass.M or
	// 		SpectralClass.ASuperGiant or
	// 		SpectralClass.BSuperGiant or
	// 		SpectralClass.FSuperGiant or
	// 		SpectralClass.GSuperGiant or
	// 		SpectralClass.KGiant or
	// 		SpectralClass.MGiant or
	// 		SpectralClass.MSuperGiant;

	/// <inheritdoc />
	public string ToString(string? format, IFormatProvider? formatProvider)
	{
		var fmt = $"{Class}{SubClass} {Luminosity}";
		return fmt.ToString(formatProvider);
	}

	/// <inheritdoc />
	public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
		IFormatProvider? provider)
	{
		return destination.TryWrite(provider, $"{Class}{SubClass} {Luminosity}", out charsWritten);
	}

	/// <inheritdoc />
	public bool TryFormat(Span<byte> destination, out int bytesWritten, ReadOnlySpan<char> format,
		IFormatProvider? provider)
	{
		return Utf8.TryWrite(destination, provider, $"{Class}{SubClass} {Luminosity}", out bytesWritten);
	}

	/// <summary>
	///     Deconstructs the stellar class into its components.
	/// </summary>
	public void Deconstruct(out SpectralClass spectralClass, out byte subClass, out StarLuminosity luminosity)
	{
		spectralClass = Class;
		subClass = SubClass;
		luminosity = Luminosity;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		return ToString(null, null);
	}
}