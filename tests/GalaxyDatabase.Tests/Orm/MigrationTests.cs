using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PTA.GalaxyDatabase.DataLayer;

namespace PTA.GalaxyDatabase.Tests.Orm;

public class MigrationTests
{
	[Test]
	public async Task CanApplyMigrationsToBlankDatabase()
	{
		await using var connection = new SqliteConnection("Data Source=:memory:");
		connection.Open();

		var factory = new DesignTimeFactory();

		var db = factory.CreateDbContext(connection, s => TestContext.Current?.OutputWriter.WriteLine(s));

		await db.Database.MigrateAsync();

		await using var cmd = connection.CreateCommand();

		cmd.CommandText = "SELECT COUNT(*) FROM \"sqlite_master\" WHERE \"name\" = '__EFMigrationsHistory' AND \"type\" = 'table';";
		await Assert.That(cmd.ExecuteScalar()).IsEqualTo((long)1);

		cmd.CommandText = "SELECT COUNT(*) FROM \"__EFMigrationsHistory\";";
		await Assert.That(cmd.ExecuteScalar()).IsEqualTo((long)1);

		cmd.CommandText = "SELECT COUNT(*) FROM \"StellarClasses\";";
		await Assert.That(cmd.ExecuteScalar()).IsEqualTo((long)0);
	}

	[Test]
	public async Task CanApplyMigrationsToDatabaseWithEmptyHistory()
	{
		await using var connection = new SqliteConnection("Data Source=:memory:");
		connection.Open();

		await using var cmd = connection.CreateCommand();
		cmd.CommandText = "CREATE TABLE \"__EFMigrationsHistory\" (\"MigrationId\" TEXT NOT NULL CONSTRAINT \"PK___EFMigrationsHistory\" PRIMARY KEY, \"ProductVersion\" TEXT NOT NULL);";

		cmd.ExecuteNonQuery();

		var factory = new DesignTimeFactory();

		var db = factory.CreateDbContext(connection);

		await db.Database.MigrateAsync();

		cmd.CommandText = "SELECT COUNT(*) FROM \"sqlite_master\" WHERE \"name\" = '__EFMigrationsHistory' AND \"type\" = 'table';";
		await Assert.That(cmd.ExecuteScalar()).IsEqualTo((long)1);

		cmd.CommandText = "SELECT COUNT(*) FROM \"__EFMigrationsHistory\";";
		await Assert.That(cmd.ExecuteScalar()).IsEqualTo((long)1);

		cmd.CommandText = "SELECT COUNT(*) FROM \"StellarClasses\";";
		await Assert.That(cmd.ExecuteScalar()).IsEqualTo((long)0);
	}
}