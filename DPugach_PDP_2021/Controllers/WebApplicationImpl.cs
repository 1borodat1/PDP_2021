namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using REST_API.Models;
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;

	#region Class: WebApplicationImpl

	public class WebApplicationImpl: BaseController
	{

		#region Properties: Protected

		protected override string PageTitle => "Approach of web application implementations";

		#endregion

		#region Methods: Private

		private static int GetRandomNumber() {
			return new Random().Next(0, 100);
		}

		private void SetResult(string approachName, double result, long tick) {
			var model = new WebApplicationModel() {
				ApproachName = approachName,
				Result = result,
				ExecutingTime = tick
			};
			ViewData["Result"] = model;
		}

		#endregion

		#region Methods: Public

		public IActionResult Monolith() {
			var tickSnapshot = DateTime.Now.Ticks;
			var result = Math.Sqrt(GetRandomNumber());
			SetResult("Monolith", result, DateTime.Now.Ticks - tickSnapshot);
			return View("WebApplication");
		}

		public IActionResult Soa() {
			return View("WebApplication");
		}

		public async Task<IActionResult> Microservice() {
			var tickSnapshot = DateTime.Now.Ticks;
			var client = new HttpClient();
			var url = $"https://localhost:5678/api/Math/Sqrt?number={GetRandomNumber()}";
			var response = await client.GetStringAsync(url);
			SetResult("Microservice", double.Parse(response), DateTime.Now.Ticks - tickSnapshot);
			return View("WebApplication");
		}

		#endregion

	}

	#endregion

}
