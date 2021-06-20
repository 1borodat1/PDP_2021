namespace REST_API.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Net.Http;
	using System.Reflection;
	using System.Threading.Tasks;
	using IoFile = System.IO.File;

	[Route("TAP")]
	public class TAP: BaseController {

		private const string _methodNamePattern= "bound";

		private bool _isAsyncExecuting;

		#region Properties: Protected

		/// <inheritdoc cref="BaseController.PageTitle"/>
		protected override string PageTitle => "TAP";

		public string RequestTiming {
			get => ViewData[MethodBase.GetCurrentMethod().Name].ToString();
			set => ViewData[MethodBase.GetCurrentMethod().Name] = value;
		}

		#endregion

		#region Methods: Private

		private static float Calculate(int i, int j) {
			var tempvalue = i - 0.1f;
			tempvalue += j;
			tempvalue *= i;
			tempvalue += tempvalue % 9999;
			tempvalue += tempvalue / 7777;
			return tempvalue;
		}

		private static string GetPatternMethodName() {
			var st = new StackTrace();
			var frameName = st.GetFrames().Select(f => f.GetMethod().Name).First(fName => fName.ToLower().Contains(_methodNamePattern));
			return frameName;
		}

		private static void CalculateParallerFor() {
			Parallel.For(0, 1000, (i) => {
				Parallel.For(0, 1000, (j) => {
					Calculate(i, j);
				});
			});
		}

		private async Task ExecuteAction(string message, Func<Task> func) {
			var boundMethodName = GetPatternMethodName();
			var time = await GetExecutingActionTimeAsync(func);
			ViewData[boundMethodName] = $"{boundMethodName}. Executing time  {time:s\\.fff} seconds. {message}.";
		}

		private async Task<TimeSpan> GetExecutingActionTimeAsync(Func<Task> func) {
			var timeSnapshot = DateTime.Now;
			await func();
			return DateTime.Now - timeSnapshot;
		}

		/// <summary>
		/// Get request string.
		/// </summary>
		private async Task IoBound1() {
			await ExecuteAction("Request.",
				async () => {
					var client = new HttpClient();
					var currentMethodName = GetPatternMethodName();
					var timeSnapshot = DateTime.Now;
					var responseString = _isAsyncExecuting
						? await client.GetStringAsync("https://www.creatio.com/")
						: client.GetStringAsync("https://www.creatio.com/").Result;
				});
		}

		/// <summary>
		/// Read random file from temp folder.
		/// </summary>
		private async Task IoBound2() {
			await ExecuteAction("Read file from disk.",
				async () => {
					var random = new Random();
					var filesNames = Directory.GetFiles(Path.GetTempPath());
					var fileName = filesNames.GetValue(random.Next(0, filesNames.Length - 1)).ToString();
					var fileBytes = _isAsyncExecuting
						? await IoFile.ReadAllBytesAsync(fileName)
						: IoFile.ReadAllBytesAsync(fileName).Result;
				});
		}

		/// <summary>
		/// Imitation of CPU load.
		/// </summary>
		/// <returns></returns>
		public async Task CpuBound1() {
			await ExecuteAction("Imitation of CPU load => delay.",
				async () => {
					var random = new Random();
					var delay = random.Next(0, 1000);
					if (_isAsyncExecuting) {
						await Task.Delay(delay);
					} else {
						Task.Delay(delay).Wait();
					}
				});
		}

		/// <summary>
		/// Performs some arithmetic logic synchronously.
		/// </summary>
		private async Task CpuBound2() {
			await ExecuteAction("Performs some arithmetic logic synchronously",
				() => {
					for (int i = 0; i < 1000; i++) {
						for (int j = 0; j < 1000; j++) {
							Calculate(i, j);
						}
					}
					return Task.CompletedTask;
				});
		}

		/// <summary>
		/// Performs same arithmetic logic asynchronous.
		/// </summary>
		/// <remarks>
		/// An illustrative example of why it is not always worth using <see cref="Parallel.For(int, int, Action{int})"/>.
		/// </remarks>
		private async Task CpuBound3() {
			await ExecuteAction("Performs same arithmetic logic asynchronous", 
				async () => {
					if (_isAsyncExecuting) {
						await Task.Factory.StartNew(() => {
							CalculateParallerFor();
						});
					} else {
						CalculateParallerFor();
					}
				});
		}

		#endregion

		#region Methods: Public

		[HttpGet("CalculateRequestTimingAsync")]
		public async Task<IActionResult> CalculateRequestTimingAsync() {
			_isAsyncExecuting = true;
			await IoBound1();
			await IoBound2();
			await CpuBound1();
			await CpuBound2();
			await CpuBound3();
			return View();
		}

		[HttpGet("CalculateRequestTiming")]
		public IActionResult CalculateRequestTiming() {
			Task.Factory.StartNew(IoBound1)
				.ContinueWith((t) => IoBound2().Wait())
				.ContinueWith((t) => CpuBound1().Wait())
				.ContinueWith((t) => CpuBound2().Wait())
				.ContinueWith((t) => CpuBound3().Wait())
				.Wait();
			return View();
		}

		#endregion

	}
}
