using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserControlAPI.Models
{
    public class GroupPermissions
    {
        [DisplayName("Group Permission ID")]
        public int GroupPermissionId { get; set; }
        [DisplayName("Group Permission Name"), StringLength(100)]
        public string GroupPermissionName { get; set; }
    }
}
