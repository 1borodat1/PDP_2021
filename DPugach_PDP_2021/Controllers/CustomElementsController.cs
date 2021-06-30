namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class CustomElementsController: BaseController
	{

		#region Properties: Protected

		protected override string PageTitle => "Custom elements";

		#endregion

		#region Methods: Public

		public IActionResult Timers() {
			return View();
		}

		#endregion

	}
}
