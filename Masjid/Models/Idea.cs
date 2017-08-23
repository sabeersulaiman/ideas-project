using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MasjidPro.Models
{
    public class Idea
    {
		public int ideaId { get; set; }
		public string ideaHeading { get; set; }
		public string ideaBody { get; set; }
		public int likes { get; set; }
		public int dislikes { get; set; }
		public int ideaStatus { get; set; }
		public DateTime dateAdded { get; set; }
		public Boolean rowDeleted { get; set; }
		public string addedBy { get; set; }

		//public Idea(
		//	string ideaHeading,
		//	string ideaBody,
		//	int likes,
		//	int dislikes,
		//	string addedBy
		//)
		//{
		//	this.ideaId = 0;
		//	this.ideaHeading = ideaHeading;
		//	this.ideaBody = ideaBody;
		//	this.likes = likes;
		//	this.dislikes = dislikes;
		//	this.ideaStatus = 0;
		//	this.addedBy = addedBy;
		//	this.dateAdded = DateTime.Now;
		//}

		public Idea(
			int ideaId,
			string ideaHeading,
			string ideaBody,
			int likes,
			int dislikes,
			int status,
			DateTime dateAdded,
			Boolean rowDeleted,
			string addedBy
		) {
			this.ideaId = ideaId;
			this.ideaHeading = ideaHeading;
			this.ideaBody = ideaBody;
			this.likes = likes;
			this.dislikes = dislikes;
			this.ideaStatus = status;
			this.dateAdded = dateAdded;
			this.rowDeleted = rowDeleted;
			this.addedBy = addedBy;
		}

		public void New()
		{
			this.ideaId = 0;
			this.ideaStatus = 0;
			this.dateAdded = DateTime.Now;
		}

		public async Task<Boolean> Save() {
			using (var db = new SQLDisposible())
			{
				var commandString = "";
				if(this.ideaId == 0)
				{
					commandString = @"
					INSERT INTO ideas(ideaHeading, ideaBody, likes, dislikes, ideaStatus, addedBy, dateAdded) output INSERTED.ideaId
					VALUES(@ideaHeading, @ideaBody, @likes, @dislikes, @ideaStatus, @addedBy, @dateAdded);";
				}
				else
				{
					commandString = @"UPDATE ideas 
												SET ideaHeading = @ideaHeading,
													ideaBody = @ideaBody,
													likes = @likes,
													dislikes = @dislikes,
													ideaStatus = @ideaStatus
													WHERE ideaId = @ideaId;";
				}

				var command = new SqlCommand(commandString, db.Connection);
				command.Parameters.Add(new SqlParameter("@ideaHeading", this.ideaHeading));
				command.Parameters.Add(new SqlParameter("@ideaBody", this.ideaBody));
				command.Parameters.Add(new SqlParameter("@likes", this.likes));
				command.Parameters.Add(new SqlParameter("@dislikes", this.dislikes));
				command.Parameters.Add(new SqlParameter("@ideaStatus", this.ideaStatus));
				command.Parameters.Add(new SqlParameter("@addedBy", this.addedBy));
				command.Parameters.Add(new SqlParameter("@dateAdded", this.dateAdded));
				command.Parameters.Add(new SqlParameter("@ideaId", this.ideaId));

				db.Connection.Open();
				if (this.ideaId == 0)
				{
					var newId = (int)await command.ExecuteScalarAsync();
					command.Dispose();
					if (newId > 0)
					{
						this.ideaId = newId;
						return true;
					}
					else return false;
				}
				else
				{
					var effected = await command.ExecuteNonQueryAsync();
					command.Dispose();
					if (effected > 0) return true;
					else return false;
				}
			}
		}

		public static async Task<Idea> getIdea(int ideaId)
		{
			var commandString = @"SELECT * FROM ideas WHERE ideaId = @id;";
			using (var db = new SQLDisposible())
			{
				var command = new SqlCommand(commandString, db.Connection);
				command.Parameters.Add(new SqlParameter("@id", ideaId));

				db.Connection.Open();
				var reader = await command.ExecuteReaderAsync();
				
				if (reader.Read() == true)
				{
					var idea = Idea.getIdeaFromReader(reader);
					return idea;
				}
				else return null;
			}
		}

		public static async Task<List<Idea>> getList(int page)
		{
			var commandString = @"listIdeas";
			using (var db = new SQLDisposible())
			{
				var command = new SqlCommand(commandString, db.Connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@pageNumber", page));

				db.Connection.Open();
				var reader = await command.ExecuteReaderAsync();

				var ideas = new List<Idea>();

				while(reader.Read() == true)
				{
					ideas.Add(Idea.getIdeaFromReader(reader));
				}

				return ideas;
			}
		}

		public static Idea getIdeaFromReader(SqlDataReader reader)
		{
			var idea = new Idea(
				reader.GetInt32(reader.GetOrdinal("ideaId")),
				reader.GetString(reader.GetOrdinal("ideaHeading")),
				reader.GetString(reader.GetOrdinal("ideaBody")),
				reader.GetInt32(reader.GetOrdinal("likes")),
				reader.GetInt32(reader.GetOrdinal("dislikes")),
				reader.GetInt32(reader.GetOrdinal("ideaStatus")),
				(DateTime)reader.GetSqlDateTime(reader.GetOrdinal("dateAdded")),
				(Boolean)reader["rowDeleted"],
				(string)reader["addedBy"]
			);

			return idea;
		}

		public static async Task<List<Idea>> getSearchList(string search, int page)
		{
			var commandString = @"searchIdeas";
			using (var db = new SQLDisposible())
			{
				var command = new SqlCommand(commandString, db.Connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@pageNumber", page));
				command.Parameters.Add(new SqlParameter("@search", search));

				db.Connection.Open();
				var reader = await command.ExecuteReaderAsync();

				var ideas = new List<Idea>();

				while (reader.Read() == true)
				{
					ideas.Add(Idea.getIdeaFromReader(reader));
				}

				return ideas;
			}
		}
	}
}
