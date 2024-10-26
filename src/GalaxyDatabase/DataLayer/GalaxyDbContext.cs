using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PTA.GalaxyDatabase.DataLayer;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial;
using PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars;

namespace PTA.GalaxyDatabase.DataLayer;

/// <summary>
///     Main interaction between us and the underlying database
/// </summary>
#pragma warning disable IL3050, IL2026
public class GalaxyDbContext(DbContextOptions<GalaxyDbContext> options) : DbContext(options)
#pragma warning restore IL3050, IL2026
{
	/// <summary>
	///     Star systems in the galaxy
	/// </summary>
	public DbSet<StarSystem> StarSystems { get; set; }

	/// <summary>
	///     Known stellar classes
	/// </summary>
	public DbSet<StellarClass> StellarClasses { get; set; }

	// /// <summary>
	// /// Native AOT can't use normal migrations, so we need to apply them manually using this helper method
	// /// </summary>
	// public void ApplyScriptedMigrations()
	// {
	// 	var conn = Database.GetDbConnection();
	//
	// 	var closeConn = conn.State != ConnectionState.Open;
	//
	// 	if(conn.State != ConnectionState.Open)
	// 	{
	// 		conn.Open();
	// 	}
	//
	// 	using var cmd = conn.CreateCommand();
	// 	cmd.CommandText = "SELECT COUNT(*) FROM \"sqlite_master\" WHERE \"name\" = '__EFMigrationsHistory' AND \"type\" = 'table';";
	//
	// 	var newDatabase = (cmd.ExecuteScalar() is long ? (long)(cmd.ExecuteScalar() ?? 0) : 0) == 0;
	//
	// 	IReadOnlyCollection<string> pendingMigrations;
	// 	if (newDatabase)
	// 	{
	// 		pendingMigrations = GetMigrationPaths();
	// 	}
	// 	else
	// 	{
	// 		cmd.CommandText = "SELECT MigrationId FROM __EFMigrationsHistory;";
	//
	// 		using var reader = cmd.ExecuteReader();
	// 		var appliedMigrations = new List<string>();
	// 		while (reader.Read())
	// 		{
	// 			var appliedId = reader.GetString(0);
	// 			appliedMigrations.Add(appliedId);
	// 		}
	//
	// 		pendingMigrations = GetMigrationPaths().Where(m => !appliedMigrations.Contains(m)).ToList().AsReadOnly();
	// 	}
	//
	// 	foreach (var migration in pendingMigrations)
	// 	{
	// 		var sql = Assembly.GetExecutingAssembly().GetManifestResourceStream(migration)!;
	// 		using var reader = new StreamReader(sql);
	// 		Database.ExecuteSqlRaw(reader.ReadToEnd());
	// 	}
	//
	// 	if(closeConn)
	// 	{
	// 		conn.Close();
	// 	}
	// }
	//
	// private IReadOnlyCollection<string> GetMigrationPaths()
	// {
	// 	var assembly = Assembly.GetExecutingAssembly();
	// 	var resources = assembly.GetManifestResourceNames();
	//
	// 	var migrations = resources
	// 		.Where(r => r.Contains("Scripts"))
	// 		.ToList()
	// 		.AsReadOnly();
	//
	// 	return migrations;
	// }

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<StarSystem>(e =>
		{
			e.Property(p => p.Position)
				.HasSrid(4979)
				.IsRequired();

			e.HasIndex(p => p.Position)
				.IsUnique();

			e.OwnsMany(n => n.Stars, nb => nb
				.ToTable("Stars")
				.HasOne(n => n.Class)
				.WithMany()
				.OnDelete(DeleteBehavior.SetNull)
			);
		});

		modelBuilder.Entity<StellarClass>(e =>
		{
			e.Property<int>("Id");
			e.HasKey("Id");

			e.HasAlternateKey(i => new { i.Class, i.SubClass, i.Luminosity });

			e.Property(p => p.CanBeScooped)
				.HasComputedColumnSql("Class BETWEEN 0 AND 13");
		});
	}
}