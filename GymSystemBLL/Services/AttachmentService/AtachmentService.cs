using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.AttachmentService
{
    public class AtachmentService : IAtachmentService
    {
        private readonly string[] allowedExtentions = {".jpg",".jpeg",".png"};
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB
    

        public string? Upload(string FolderName, IFormFile file)
        {
            try
            {
                if (FolderName is null || file is null || file.Length == 0)
                {
                    return null;
                }
                if (file.Length > maxFileSize)
                {
                    return null;
                }
                var fileExtention = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtentions.Contains(fileExtention))
                {
                    return null;
                }
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", FolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtention;


                var filePath = Path.Combine(folderPath, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(stream);

                return uniqueFileName;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"failed to upload photo : {ex}");
                return null;
            }
        }

        public bool Delete(string FileName, string FolderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderName))
                {
                    return false;
                }
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", FolderName, FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to delete photo : {ex}");
                return false;
            }

        }

    }
}
