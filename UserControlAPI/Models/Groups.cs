using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserControlAPI.Models
{
    public class Groups
    {
        [DisplayName("Group ID")]
        public int GroupId { get; set; }
        [DisplayName("Group Name"), StringLength(100)]
        public string GroupName { get; set; }
        [DisplayName("User ID")]
        public int UsertId { get; set; }
        [DisplayName("Group Permission ID")]
        public int GroupPermissionId { get; set; }
    }
}
