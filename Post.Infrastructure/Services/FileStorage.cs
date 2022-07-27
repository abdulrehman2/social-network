using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Post.Application.Contracts.Services;
using Post.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Services
{
    public class FileStorage : IFileStorage
    {
        private readonly IHostingEnvironment _environment;

        public FileStorage(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public FileUploadResponse SaveFile(IFormFile formFile)
        {
            var fileUploadResponse = new FileUploadResponse();
            try
            {
                // string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                string path = Path.Combine(_environment.ContentRootPath, "wwwroot/uploads");
                //string path = Path.Combine(AppContext.BaseDirectory, "wwwroot/uploads"); 

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                
                FileInfo fileInfo = new FileInfo(formFile.FileName);

                string fileNameWithPath = Path.Combine(path, formFile.FileName);
                
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                fileUploadResponse.IsUploaded = true;
                fileUploadResponse.FilePath = formFile.FileName;
            }
            catch(Exception ex)
            {
                fileUploadResponse.Message = ex.Message.ToString();
            }
            return fileUploadResponse;
        }
    }
}
