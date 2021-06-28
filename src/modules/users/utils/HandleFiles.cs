using fs = System.IO;

namespace ourbank.Utils {
  public static class HandleFiles {
    public static void deleteFile(string filepath) {
      if (fs.File.Exists(filepath)) {
        fs.File.Delete(filepath);
      }
    }
  }
}