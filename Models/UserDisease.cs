using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectYogaMed.Models
{
    public partial class UserDisease
    {[Key]
        public int Udid { get; set; }
        public int? UserIdFk { get; set; }
        public string Disease { get; set; }
        public int? DiseaseIdfk { get; set; }

        public virtual DiseaseTable DiseaseIdfkNavigation { get; set; }
        public virtual UserDetails UserIdFkNavigation { get; set; }
    }
}
