using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> AllowedExtensions = new List<string>() { ".png","jpg","jpeg"};
        private const int AllowedMaxSize = 2_097_152;
        public string? Upload(IFormFile File, string FolderName)
        {
            var extension = Path.GetExtension(File.FileName);
            if (!AllowedExtensions.Contains(extension))
                return null;

            if(File.Length > AllowedMaxSize) 
                return null;

            //var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName);
            
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            var FileName   = $"{Guid.NewGuid()}{extension}";
            var FilePath   = Path.Combine(FolderPath, FileName) ;

            using(var FileStream = new FileStream(FilePath, FileMode.Create))
                File.CopyTo(FileStream); 
            return FileName ;
        }
        public bool Delete(string FilePath)
        {
            if(!File.Exists(FilePath))
                return false;

            File.Delete(FilePath);
            return true;
        }
    }
}
