using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MasjidPro.Models;
using System.Data.SqlClient;

namespace MasjidPro.Controllers
{
	[Route("api/[controller]")]
	public class IdeasController : Controller
	{
		public IdeasController()
		{
		}

		[HttpPut, Route("")]
		public async Task<IActionResult> addIdea([FromBody] Idea idea)
		{
			try
			{
				var result = await idea.Save();
				if (result)
				{
					var response = new ResponseModel<Idea>("IA100", "Idea Added successfully.", idea);
					return new ObjectResult(response);
				}
				else
				{
					var response = new ResponseModel<Idea>("IA300", "Data Validation Failed", null);
					return new ObjectResult(response);
				}
			}
			catch (Exception e) {
				var response = new ResponseModel<Idea>("IA200", "Database Error", null);
				return new ObjectResult(response);
			}
		}
	}
}
