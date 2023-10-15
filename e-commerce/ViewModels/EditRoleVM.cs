using System.ComponentModel.DataAnnotations;

namespace e_commerce.ViewModels
{
    public class EditRoleVM
    {
        public string RoleId { get; set; }
        //[Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
