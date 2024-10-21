﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace IKEA.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        public string Upload(IFormFile File, string FolderName);

        public bool Delete(string FilePath);
    }
    
}
