using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API.Controllers
{
	public class Https: Controller
	{
		public IActionResult Tls() {
			return View();
		}
	}
}
