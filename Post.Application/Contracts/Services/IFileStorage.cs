using Microsoft.AspNetCore.Http;
using Post.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Services
{
    public interface IFileStorage
    {
        FileUploadResponse SaveFile(IFormFile formFile);
    }
}
