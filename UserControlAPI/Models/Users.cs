using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserControlAPI.Models
{
    public class Users
    {
        [DisplayName("User ID")]
        public int UsertId { get; set; }
        [DisplayName("User Name"), StringLength(100)]
        public string UserName { get; set; }
    }
}
