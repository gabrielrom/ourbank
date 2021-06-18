using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ourbank.Error {
  public class GlobalError {
    private readonly RequestDelegate _next;

    public GlobalError(RequestDelegate next) {
      _next = next;
    }

    public async Task Invoke(HttpContext context) {
      try {
        await _next(context);
      } catch (Exception error) {
        HttpResponse response = context.Response;
        response.ContentType = "application/json";

        string result;

        switch(error) {
          case AppError err: 
            response.StatusCode = err.statusCode;

            result = JsonSerializer.Serialize(new {
              message = err.messageError,
              statusCode = err.statusCode
            });

            break;

          default: 
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            result = JsonSerializer.Serialize(new {
              message = error.Message,
            });

            break;
        }

        await response.WriteAsync(result);
      }
    }
  }
}