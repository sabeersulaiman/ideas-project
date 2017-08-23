using System;
using DbUp;
using System.Reflection;
using System.Linq;

namespace DBUpgraderCore
{
    class Program
    {
		static int Main(string[] args)
		{
			Console.WriteLine("I am starting..." + args.FirstOrDefault());
			var connectionString =
				args.FirstOrDefault()
				?? @"Server=DESKTOP-JOFGSH2;Database=IdeaDB;Trusted_Connection=True;";

			EnsureDatabase.For.SqlDatabase(connectionString);

			var upgrader =
				DeployChanges.To
					.SqlDatabase(connectionString)
					.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
					.LogToConsole()
					.Build();

			var result = upgrader.PerformUpgrade();

			if (!result.Successful)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(result.Error);
				Console.ResetColor();
#if DEBUG
				Console.ReadLine();
#endif
				return -1;
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Success!");
			Console.ResetColor();
			return 0;
		}
	}
}
