using FluentValidation;
using System.Text.Json;

namespace GbsoDevExagonalTemplate.Api
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate Next;
		private readonly ILogger<ExceptionMiddleware> Logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			Next = next;
			Logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await Next(httpContext);
			}
			catch (ValidationException vex)
			{
				var errorResponse = new BadRequestResponse
				{
					mensaje = "La solicitud no pasó las validaciones",
					validations = vex.Errors.Select(x => x.ErrorMessage).ToArray(),
				};

				Logger.LogError(vex, errorResponse.mensaje);

				var jsonResponse = JsonSerializer.Serialize(errorResponse);
				httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				await httpContext.Response.WriteAsync(jsonResponse);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Error inesperado en el Middleware");
				var errorResponse = new ErrorResponse
				{
					mensaje = "Error inesperado",
				};
				var jsonResponse = JsonSerializer.Serialize(errorResponse);
				httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
				await httpContext.Response.WriteAsync(jsonResponse);
			}
		}

		public class ErrorResponse
		{
			public string mensaje { get; set; }
		}

		public class BadRequestResponse : ErrorResponse
		{
			public string[] validations { get; set; }
		}
	}
}
