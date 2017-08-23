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
			if (idea == null)
			{
				var response = new ResponseModel<Idea>("IA400", "Data Validation Failed", null);
				return new ObjectResult(response);
			}
			try
			{
				idea.New();
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
				var response = new ResponseModel<Idea>("IA200", "Unexpected Error", null);
				return new ObjectResult(response);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> getIdea(int id)
		{
			try
			{
				var idea = await Idea.getIdea(id);
				if(idea == null)
				{
					var response = new ResponseModel<Idea>("IB200", "Could not find the Idea.", null);
					return new ObjectResult(response);
				}
				else
				{
					var repsonse = new ResponseModel<Idea>("IB100", "Success", idea);
					return new ObjectResult(repsonse);
				}
			}
			catch(Exception e)
			{
				var response = new ResponseModel<Idea>("IB300", "Failed to fetch Data.", null);
				return new ObjectResult(response);
			}
		}

		[HttpGet("like/{id}")]
		public async Task<IActionResult> likeIdea(int id)
		{
			try
			{
				var idea = await Idea.getIdea(id);
				idea.likes ++;
				var ret = await idea.Save();
				if(ret)
				{
					var response = new ResponseModel<Idea>("IC100", "Success", idea);
					return new ObjectResult(response);
				}
				else
				{
					var response = new ResponseModel<Idea>("IC300", "Save Failed", null);
					return new ObjectResult(response);
				}
			}
			catch(Exception e)
			{
				var response = new ResponseModel<Idea>("IC200", "Unexpected Error", null);
				return new ObjectResult(response);
			}
		}


		[HttpGet("dislike/{id}")]
		public async Task<IActionResult> dislikeIdea(int id)
		{
			try
			{
				var idea = await Idea.getIdea(id);
				idea.dislikes ++;
				var ret = await idea.Save();
				if (ret)
				{
					var response = new ResponseModel<Idea>("ID100", "Success", idea);
					return new ObjectResult(response);
				}
				else
				{
					var response = new ResponseModel<Idea>("ID300", "Save Failed", null);
					return new ObjectResult(response);
				}
			}
			catch (Exception e)
			{
				var response = new ResponseModel<Idea>("ID200", "Unexpected Error", null);
				return new ObjectResult(response);
			}
		}
	}
}
