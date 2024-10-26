using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PTA.GalaxyDatabase.DataLayer;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial.IdTypes;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

namespace PTA.GalaxyDatabase.Tests.Orm;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class StellarClassOrmTests
{
	public static IEnumerable<(SpectralClass, byte, StarLuminosity, bool)> ScoopableSpectralClasses()
	{
		SpectralClass[] spectral = [SpectralClass.O, SpectralClass.A, SpectralClass.G, SpectralClass.MSuperGiant, SpectralClass.BlackHole];
		byte[] subClass = [0, 1, 2, 3, 4];
		StarLuminosity[] luminosity = [StarLuminosity.I, StarLuminosity.Ia, StarLuminosity.Ib, StarLuminosity.II, StarLuminosity.IV];

		return from s in spectral
			from sc in subClass
			from l in luminosity
			select (s, sc, l, s < (SpectralClass)14);
	}

	public static IEnumerable<(SpectralClass spectral, byte subclass, StarLuminosity luminosity)> SpectralClassMatrix()
	{
		return from spectral in Enum.GetValues<SpectralClass>()//.Where((x, i) => i % 2 == 0)
			from subclass in Enumerable.Range(0, 5).Select(i => (byte)i)
			from luminosity in Enum.GetValues<StarLuminosity>()//.Where((x, i) => i % 2 == 0)
			select (spectral, subclass, luminosity);
	}

	[Test]
	// [ParallelLimiter<SqliteBulkLimiter>] // Limit the number of parallel tests to prevent SQLite from running out of connections
	public async Task CanAddClassToDatabase(
		[Matrix(SpectralClass.O, SpectralClass.A, SpectralClass.G, SpectralClass.MSuperGiant, SpectralClass.BlackHole)]
		SpectralClass spectralClass,
		[Matrix((byte)0, (byte)1, (byte)2, (byte)3, (byte)4)]
		byte subClass,
		[Matrix(StarLuminosity.I, StarLuminosity.Ia, StarLuminosity.Ib, StarLuminosity.II, StarLuminosity.IV)]
		StarLuminosity luminosity
	)
	{
		await using var connection = new SqliteConnection("Data Source=:memory:");
		connection.Open();
		var factory = new DesignTimeFactory();

		var db = factory.CreateDbContext(connection);

		await db.Database.EnsureCreatedAsync();

		db.StellarClasses.Add(new StellarClass(spectralClass, subClass, luminosity));

		await db.SaveChangesAsync();

		await Assert.That(db.StellarClasses).HasCount().EqualTo(1);

		await using var cmd = connection.CreateCommand();
		cmd.CommandText = "SELECT COUNT(*) FROM StellarClasses";

		await Assert.That(cmd.ExecuteScalar())
			.IsEqualTo((long)1);
	}

	[Test]
	// [Explicit]
	[Property("TestType", "Unit")]
	[Property("Database", "true")]
	[Property("Spatial", "false")]
	[MethodDataSource(nameof(ScoopableSpectralClasses))]
	// [ParallelLimiter<SqliteBulkLimiter>]
	public async Task ComputesScoopableCorrectly(SpectralClass spectralClass, byte subClass, StarLuminosity luminosity,
		bool shouldBeScoopable)
	{
		await using var connection = new SqliteConnection("Data Source=:memory:");
		connection.Open();
		var factory = new DesignTimeFactory();

		var db = factory.CreateDbContext(connection);

		await db.Database.EnsureCreatedAsync();

		await using var cmd = connection.CreateCommand();
		cmd.CommandText = $"INSERT INTO StellarClasses (Class, SubClass, Luminosity) VALUES ({(byte)spectralClass}, {subClass}, {(byte)luminosity})";
		cmd.ExecuteNonQuery();

		var scoopable = db.StellarClasses
			.Where(s => s.CanBeScooped) //Only select scoopable stars
			.AsNoTracking(); // Read only query so don't set up tracking

		await Assert.That(scoopable).HasCount().EqualTo(shouldBeScoopable ? 1 : 0);
	}

	[Test]
	[Explicit]
	[Property("TestType", "Unit")]
	[Property("Database", "true")]
	[Property("Spatial", "false")]
	// [MethodDataSource(nameof(SpectralClassMatrix))]
	// [ParallelLimiter<SqliteBulkLimiter>]
	public async Task HandlesDuplicateStellarClasses(
		[Matrix(SpectralClass.O, SpectralClass.A, SpectralClass.G, SpectralClass.MSuperGiant, SpectralClass.BlackHole)]
		SpectralClass spectralClass,
		[Matrix((byte)0, (byte)1, (byte)2, (byte)3, (byte)4)]
		byte subClass,
		[Matrix(StarLuminosity.I, StarLuminosity.Ia, StarLuminosity.Ib, StarLuminosity.II, StarLuminosity.IV)]
		StarLuminosity luminosity
	)
	{
		await using var connection = new SqliteConnection("Data Source=:memory:");
		connection.Open();
		var factory = new DesignTimeFactory();

		var db = factory.CreateDbContext(connection);

		await db.Database.EnsureCreatedAsync();

		db.StarSystems.Add(new StarSystem
		{
			Id = new SystemId(1),
			Position = new Point(0, 0, 0),
			Stars =
			[
				new Star
				{
					Id = new StarId(1),
					Class = new StellarClass(spectralClass, subClass, luminosity),
					DistanceFromMainStar = 0
				}
			]
		});

		await db.SaveChangesAsync();

		db.StarSystems.Add(new StarSystem
		{
			Id = new SystemId(2),
			Position = new Point(1, 0, 0),
			Stars =
			[
				new Star
				{
					Id = new StarId(1),
					Class = db.StellarClasses.FirstOrDefault(x =>
						        x.Class == spectralClass && x.SubClass == subClass && x.Luminosity == luminosity) ??
					        new StellarClass(spectralClass, subClass, luminosity),
					DistanceFromMainStar = 0
				}
			]
		});

		await db.SaveChangesAsync();

		await using var cmd = connection.CreateCommand();
		cmd.CommandText = "SELECT COUNT(*) FROM StellarClasses";

		await Assert.That(cmd.ExecuteScalar())
			.IsEqualTo((long)1);

		cmd.CommandText = "SELECT COUNT(*) FROM StarSystems";

		await Assert.That(cmd.ExecuteScalar())
			.IsEqualTo((long)2);

		await Assert.That(db.StellarClasses).HasCount().EqualTo(1);
		await Assert.That(db.StarSystems).HasCount().EqualTo(2);
	}
}