using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API.Controllers
{
	[Route("Routing")]
	public class Routing: Controller
	{
		[HttpGet("Testing/{id:int?}")]
		public IActionResult Testing(int? id) {
			ViewData["Title"] = "Routing";
			return View("Testing"); ;
		}
	}
}
