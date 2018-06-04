using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Entities;
    using AlgorithmsRanking.Models;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AttachmentsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AttachmentsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFile()
        {
            try
            {
                var folderName = "files";
                var webRootPath = _hostingEnvironment.WebRootPath;
                var newPath = Path.Combine(webRootPath, folderName);
                var urlBasePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{folderName}";

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                var files = new List<Attachment>();

                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        files.Add(new Attachment(0, $"{urlBasePath}/{fileName}", file.Length, file.Name));
                    }
                }

                return Ok(new { message = "Загрузка успешно завершена", files });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError("500", $"Не удалось загрузить файл", ex.Message));
            }
        }
    }
}
