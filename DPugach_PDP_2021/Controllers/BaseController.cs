namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	public abstract class BaseController: Controller
	{

		#region Propertis: Private

		/// <summary>
		/// ViewData wrapper for page title.
		/// </summary>
		private string _pageTitle {
			get {
				return (string)ViewData["Title"];
			}
			set {
				ViewData["Title"] = value;
			}
		}

		#endregion

		#region Propertis: Protected

		/// <summary>
		/// Page title.
		/// </summary>
		protected abstract string PageTitle { get; }

		#endregion

		#region Metods: Public

		/// <inheritdoc cref="Controller.OnActionExecuting(ActionExecutingContext)"/>
		public override void OnActionExecuting(ActionExecutingContext context) {
			base.OnActionExecuting(context);
			_pageTitle = PageTitle;
		}

		#endregion

	}
}
