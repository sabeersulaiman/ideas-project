using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MasjidPro.Models
{
    public class Idea
    {
		public int ideaId { get; set; }
		public string ideaHeading { get; set; }
		public string ideaBody { get; set; }
		public int likes { get; set; }
		public int dislikes { get; set; }
		public int status { get; set; }

		public Idea(
			int ideaId,
			string ideaHeading,
			string ideaBody,
			int likes,
			int dislikes,
			int status
		) {
			this.ideaId = 0;
			this.ideaHeading = ideaHeading;
			this.ideaBody = ideaBody;
			this.likes = likes;
			this.dislikes = dislikes;
			this.status = status;
		}

		public async Task<Boolean> Save() {
			using (var db = new SQLDisposible())
			{
				var commandString = @"
					INSERT INTO ideas(ideaHeading, ideaBody, likes, dislikes, ideaStatus) output INSERTED.ideaId
					VALUES(@ideaHeading, @ideaBody, @likes, @dislikes, @status);";

				var command = new SqlCommand(commandString, db.Connection);
				command.Parameters.Add(new SqlParameter("@ideaHeading", this.ideaHeading));
				command.Parameters.Add(new SqlParameter("@ideaBody", this.ideaBody));
				command.Parameters.Add(new SqlParameter("@likes", this.likes));
				command.Parameters.Add(new SqlParameter("@dislikes", this.dislikes));
				command.Parameters.Add(new SqlParameter("@status", this.status));
				
				db.Connection.Open();
				var newId = (int)await command.ExecuteScalarAsync();
				if (newId > 0)
				{
					this.ideaId = newId;
					return true;
				}
				else return false;
			}
		}
    }
}
