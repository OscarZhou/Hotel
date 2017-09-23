
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    public class SysAdmin
    {
        [DisplayName("登录名")]
        [Required(ErrorMessage = "{0}不能为空")]
        public int LoginId { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string LoginPwd { get; set; }

        public string LoginName { get; set; }

    }
}
