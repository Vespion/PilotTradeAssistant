using TUnit.Core.Interfaces;

namespace PTA.GalaxyDatabase.Tests;

public class SqliteBulkLimiter: IParallelLimit
{
	public int Limit { get; } = Math.Min(Environment.ProcessorCount - (Environment.ProcessorCount / 4), 64);
}