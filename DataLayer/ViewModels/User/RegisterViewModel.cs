using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد")]
        [StringLength(150,ErrorMessage = "تعداد کاراکتر ها بیشتر از حد مجاز می باشد")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "برای نام کاربری حتما باید از کاراکتر های لاتین استفاده کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        [StringLength(500,ErrorMessage = "تعداد کاراکتر ها بیشتر از حد مجاز می باشد")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل خود را با فرمت صحیح وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد نمایید")]
        [StringLength(100, ErrorMessage = "تعداد کاراکتر ها بیشتر از حد مجاز می باشد")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "رمز عبور با تاییدش مغایرت دارد" )]
        public string RePassword { get; set; }

        public bool IsAccept { get; set; }

    }
}
