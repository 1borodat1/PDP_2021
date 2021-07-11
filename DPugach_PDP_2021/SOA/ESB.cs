namespace REST_API.SOA
{
	#region Class: ESB
	
	public class ESB
	{

		#region Methods: Public

		public static double GetSquareRoot(double number) {
			var service = new MathService();
			return service.GetSquareRoot(number);
		}

		public static void CallAnotherService() { }

		#endregion

	}

	#endregion

}
