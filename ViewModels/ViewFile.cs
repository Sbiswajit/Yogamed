using Microsoft.AspNetCore.Http;
using ProjectYogaMed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectYogaMed.ViewModels
{
    public class ViewFile
    {
        [Key]
        public int YogaId { get; set; }
        public string YogaName { get; set; }
        public IFormFile formFile { get; set; }
        public int? YdfkId { get; set; }

        public virtual DiseaseTable Ydfk { get; set; }
    }
}
