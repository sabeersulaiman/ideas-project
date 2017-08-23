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
#if DEBUG
			string envvariable = @"Server = DESKTOP-JOFGSH2; Database = IdeaDB; Trusted_Connection = True;"; //@"Server=tcp:idea-addin.database.windows.net,1433;Initial Catalog=idea-sql;Persist Security Info=False;User ID=sabeersulaiman;Password=Db#128$AbEeR;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
			Connection = new SqlConnection(envvariable);
			Console.WriteLine("Debug Key Taken");
#else
            string envvariable = Environment.GetEnvironmentVariable("MSSQL_CONNECTION");
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