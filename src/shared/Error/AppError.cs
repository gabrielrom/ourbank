using System;

namespace ourbank.Error {
  public class AppError : Exception {
    public string messageError { get; private set; }
    public int statusCode { get; private set; }

    public AppError(string messageError, int statusCode = 400) {
      this.messageError = messageError;
      this.statusCode = statusCode;
    }
  }
}