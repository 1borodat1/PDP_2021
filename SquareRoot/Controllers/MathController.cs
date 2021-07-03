namespace SquareRoot.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System;

	[Route("api/math")]
	public class MathController: ControllerBase
	{

		[HttpGet("sqrt")]
		public double GetSquareRoot(double number) {
			return Math.Sqrt(number);
		}

	}
}
