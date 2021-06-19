namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Net;

	/// <summary>
	/// Controller for demonstration swagger capabilities
	/// </summary>
	[Route("Swagger")]
	public class Swagger: Controller
	{

		public Swagger() { }

		/// <summary>
		/// Action for generated expected error <paramref name="code"/> response <see cref="HttpStatusCode"/>.
		/// </summary>
		/// <param name="code">Error code.</param>
		/// <returns>Expected error code in response.</returns>
		/// <response code="400">Return response HttpStatusCode BadRequest if param <paramref name="code"/> is 400.</response>
		/// <response code="401">Return response HttpStatusCode Unauthorized if param <paramref name="code"/> is 401.</response>
		/// <response code="403">Return response HttpStatusCode Forbidden if param <paramref name="code"/> is 403.</response>
		/// <response code="404">Return response HttpStatusCode NotFound if param <paramref name="code"/> is 404.</response>
		/// <response code="408">Return response HttpStatusCode RequestTimeout if param <paramref name="code"/> is 408.</response>
		/// <response code="500">Return response HttpStatusCode InternalServerError if param <paramref name="code"/> is 500.</response>
		/// <response code="502">Return response HttpStatusCode BadGatewayif param <paramref name="code"/> is 502.</response>
		/// <response code="503">Return response HttpStatusCode ServiceUnavailableif param <paramref name="code"/> is 503.</response>
		/// <response code="504">Return response HttpStatusCode GatewayTimeout if param <paramref name="code"/> is 504.</response>
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet("/error/{code:int}")]
		public IActionResult Error(int code) {
			try {
				StatusCodeResult result;
				return ((HttpStatusCode)code) switch {
					HttpStatusCode.BadRequest => StatusCode(code, "BadRequest"),
					HttpStatusCode.Unauthorized => StatusCode(code, "Unauthorized"),
					HttpStatusCode.Forbidden => StatusCode(code, "Forbidden"),
					HttpStatusCode.NotFound => StatusCode(code, "NotFound"),
					HttpStatusCode.RequestTimeout => StatusCode(code, "RequestTimeout"),
					HttpStatusCode.InternalServerError => StatusCode(code, "InternalServerError"),
					HttpStatusCode.BadGateway => StatusCode(code, "BadGateway"),
					HttpStatusCode.ServiceUnavailable => StatusCode(code, "ServiceUnavailable"),
					HttpStatusCode.GatewayTimeout => StatusCode(code, "GatewayTimeout"),
					_ => StatusCode(code, $"Error code {code} not supported."),
				};
			} catch(Exception e) {
				return StatusCode(500, "Error code not supported." + e.Message);
			}
		}

		/// <summary>
		/// Allowed request with 'application/json' media type.
		/// </summary>
		/// <returns>Request media type</returns>
		[Produces("application/json")]
		[HttpGet("json")]
		public string Json() {
			return Request.Headers["Accept"];
		}

		/// <summary>
		/// Allowed request with 'application/xml' media type.
		/// </summary>
		/// <returns>Request media type</returns>
		[Produces("application/xml")]
		[HttpGet("xml")]
		public string Xml() {
			return Request.Headers["Accept"];
		}

	}
}
