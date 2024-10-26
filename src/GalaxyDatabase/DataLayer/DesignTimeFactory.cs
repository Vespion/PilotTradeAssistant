using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SQLitePCL;

namespace PTA.GalaxyDatabase.DataLayer;

internal class DesignTimeFactory : IDesignTimeDbContextFactory<GalaxyDbContext>
{
	static DesignTimeFactory()
	{
		Batteries.Init();
	}

	public GalaxyDbContext CreateDbContext(string?[] args)
	{
		var connStringBuilder =
			new SqliteConnectionStringBuilder(args.Length > 0 ? args[0] ?? string.Empty : string.Empty);

		if (string.IsNullOrWhiteSpace(connStringBuilder.DataSource)) connStringBuilder.DataSource = ":memory:";

		var builder = new DbContextOptionsBuilder<GalaxyDbContext>();

		builder.UseSqlite(connStringBuilder.ConnectionString, db => { db.UseNetTopologySuite(); });
		builder.UseStronglyTypeConverters();

		return new GalaxyDbContext(builder.Options);
	}

	public GalaxyDbContext CreateDbContext(SqliteConnection connection, Action<string>? logFn = null)
	{
		var builder = new DbContextOptionsBuilder<GalaxyDbContext>();

		builder.UseSqlite(connection, db => { db.UseNetTopologySuite(); });
		builder.UseStronglyTypeConverters();

		if (logFn != null)
		{
			builder.LogTo(logFn);
		}

		return new GalaxyDbContext(builder.Options);
	}
}