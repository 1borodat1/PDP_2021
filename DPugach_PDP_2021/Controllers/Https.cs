namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	
	[Route("Https")]
	public class Https: BaseController
	{

		#region Properties: Protected

		/// <inheritdoc cref="BaseController.PageTitle"/>
		protected override string PageTitle => "Https";

		#endregion

		#region Methods: Public

		/// <summary>
		/// Tls handshake connection action.
		/// </summary>
		/// <returns>Tls handshake connection action view.</returns>
		//[Route("Tls")]
		public IActionResult Tls() {
			return View();
		}

		#endregion

	}
}
