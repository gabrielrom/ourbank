using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ourbank.Error;

namespace ourbank.Middlewares {
  public class Upload {
    private readonly RequestDelegate _next;
    private IWebHostEnvironment _environment;

    public Upload(
      RequestDelegate next, 
      IWebHostEnvironment environment
    ) {
      _next = next;
      _environment = environment;
    }

    public async Task Invoke(HttpContext context) {
      HttpResponse response = context.Response;
      response.ContentType = "application/json";

      string result;

      var file = context.Request.Form.Files.GetFile("file");

      if (file == null) {
        throw new AppError(
          "You cannot upload a non-existent file"
        );

      } else if (file.FileName.Contains(' ')) {
        throw new AppError(
          "You cannot have any white space on the file name, " +
          "rename the file name to avatar.png or remove the whites spaces"
        );
      }

      string generateFileName = $"{Guid.NewGuid()}-{file.FileName}";

      try {
        string pathFile = Path.Combine(
          _environment.ContentRootPath,
          "tmp/avatars"
        );

        bool isDirectoryExists = Directory.Exists(
          pathFile
        );

        if (!isDirectoryExists) {
          Directory.CreateDirectory(pathFile);
        }

        using (
          FileStream fileStream = File.Create(
            Path.Combine(pathFile, $"{generateFileName}")
          )
        ) {
          await file.CopyToAsync(fileStream);
          fileStream.Flush();

          context.Items["avatar_url"] = generateFileName;

          await _next(context);
        }

      } catch (Exception err) {
        var error = new AppError(err.Message);

        response.StatusCode = error.statusCode;

        result = JsonSerializer.Serialize(new {
          message = error.messageError
        });

        await response.WriteAsync(result);
      }
    }
  }
}