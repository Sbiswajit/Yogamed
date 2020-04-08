using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectYogaMed.Models
{
    public partial class UserDetails
    {
        public UserDetails()
        {
            UserDisease = new HashSet<UserDisease>();
        }
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Useremail { get; set; }
        public long Usercontact { get; set; }
        public DateTime Dob { get; set; }
        public string Userpassword { get; set; }

        public virtual ICollection<UserDisease> UserDisease { get; set; }
    }
}
