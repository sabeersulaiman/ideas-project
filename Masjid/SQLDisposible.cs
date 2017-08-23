using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MasjidPro
{
    public class SQLDisposible : IDisposable
    {
		public SqlConnection Connection;

		public SQLDisposible()
		{
			//string envvariable = Environment.GetEnvironmentVariable("SQLCONNSTR_localdb");
			string envvariable = @"Server = (localdb)\mssqllocaldb; Database = IdeaDB; Trusted_Connection = True;";
#if DEBUG
			Connection = new SqlConnection(envvariable);
			Console.WriteLine("Debug Key Taken");
#else
            Connection = new SqlConnection(envvariable);
			Console.WriteLine("Debug Key Taken");
#endif
		}

		public void Dispose()
		{
			Connection.Close();
		}
	}
}