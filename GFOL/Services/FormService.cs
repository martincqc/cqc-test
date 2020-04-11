using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Helpers;
using GFOL.Models;
using GFOL.Repository;
using Newtonsoft.Json;

namespace GFOL.Services
{
    public interface IFormService
    {
        PageVM GetPageById(string pageId, bool useFile = false);
    }
    public class FormService : IFormService
    {
        private readonly IGenericRepository<Schema> _repo;
        public FormService(IGenericRepository<Schema> repo)
        {
            _repo = repo;
        }

        public PageVM GetPageById(string pageId, bool useFile = false)
        {
            FormVM formVm = null;
            if (useFile)
            {
                using (var r = new StreamReader("Content/form-schema.json"))
                {
                    var file = r.ReadToEnd();

                    formVm = JsonConvert.DeserializeObject<FormVM>(file);
                }
            }
            else
            {
                var schema = _repo.GetByIdAsync(1).Result;
                if (schema != null)
                {
                    formVm = JsonConvert.DeserializeObject<FormVM>(schema.SchemaJson);
                }
            }

            var pageVm = string.IsNullOrEmpty(pageId)
                ? formVm.Pages.FirstOrDefault()
                : formVm.Pages.FirstOrDefault(m => m.PageId == pageId);

            return pageVm;

        }
    }
}
