using System.IO;
using System.IO.Pipes;

namespace PL.Helper
{
    public static class DocumentSettings
    {

        public static string Uploadfile(IFormFile file, string folderName) 
        {
            var Folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files",folderName);

            var FileName= $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}" ;

            var FilePath= Path.Combine(Folderpath, FileName);

            using var fileStream = new FileStream(FilePath, FileMode.Create);
          
            file.CopyTo(fileStream);



            return FileName;
        }
        public static void Deletefile(string file, string folderName)
        {
            var Folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", file);

            if (File.Exists(Folderpath)) 
            {
                var FilePath = Path.Combine(Folderpath, folderName);
                if(File.Exists(FilePath)) { File.Delete(FilePath); }
            }
        }
    }
}
