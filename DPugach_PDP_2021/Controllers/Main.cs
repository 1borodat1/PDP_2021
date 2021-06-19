namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;


	public class Main: BaseController
    {

		#region Properties: Protected

		/// <inheritdoc cref="BaseController.PageTitle"/>
		protected override string PageTitle => "Home";

		#endregion

		#region Methods: Public

		/// <summary>
		/// Home action.
		/// </summary>
		/// <returns>Home action view.</returns>
		public IActionResult Home() {
			return View();
		}

		#endregion
		
    }
}
