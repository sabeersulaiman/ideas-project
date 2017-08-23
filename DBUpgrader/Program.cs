using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUp;
using System.Reflection;

namespace DBUpgrader
{
	class Program
	{
		static int Main(string[] args)
		{
			var connectionString =
				args.FirstOrDefault()
				?? @"Server=(localdb)\mssqllocaldb;Database=IdeaDB;Trusted_Connection=True;";

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
