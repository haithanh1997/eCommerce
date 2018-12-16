using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Nhớ mật khẩu?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Trường {0} nhập không đúng định dạng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ mật khẩu")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [EmailAddress(ErrorMessage = "Trường {0} nhập không đúng định dạng.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất là {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không giống với mật khẩu đã nhập.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [EmailAddress(ErrorMessage = "Trường {0} nhập không đúng định dạng.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất là {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không giống với mật khẩu đã nhập.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Trường {0} bắt buộc nhập.")]
        [EmailAddress(ErrorMessage = "Trường {0} nhập không đúng định dạng.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
