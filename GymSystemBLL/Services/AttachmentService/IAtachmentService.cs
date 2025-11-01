using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.AttachmentService
{
    public interface IAtachmentService
    {
       string? Upload( string FolderName, IFormFile file);

         bool Delete (string FileName, string FolderName);


    }
}
