namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	[Route("Routing")]
	public class Routing: BaseController
	{

		#region Properties: Protected

		/// <inheritdoc cref="BaseController.PageTitle"/>
		protected override string PageTitle => "Routing";

		#endregion

		#region Methods: Public

		/// <summary>
		/// Routing testing action.
		/// </summary>
		/// <returns>Routing testing action view.</returns>
		[HttpGet("Testing")]
		public IActionResult Testing() {
			return View("Testing");
		}

		#endregion
		
	}
}
