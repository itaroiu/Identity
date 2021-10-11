using Identity.Shared.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Shared.Dtos
{
    public class RegisterAccountDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string RoleName { get; set; }
        public string AccessCode { get; set; }

        public static explicit operator RegisterAccountDto(RegisterAccountViewModel v)
        {
            return new RegisterAccountDto
            {
                UserName = v.UserName,
                RoleName = v.RoleName,
                Password = v.Password,
                PasswordConfirm = v.PasswordConfirm,
                AccessCode = v.AccessCode
            };
        }
    }
}
