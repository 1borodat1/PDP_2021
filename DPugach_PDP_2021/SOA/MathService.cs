namespace REST_API.SOA
{
	using System;

	#region Class: MathService

	public class MathService
	{

		#region Methods: Public

		public double GetSquareRoot(double number) {
			var sqrt = Math.Sqrt(number);
			ESB.CallAnotherService();
			return sqrt;
		}

		#endregion

	}

	#endregion

}
