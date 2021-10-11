using Identity.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(15)]
        public string AccessCode { get; set; }

        public static explicit operator ApplicationUser(RegisterAccountDto v)
        {
            return new ApplicationUser
            {
                AccessCode = v.AccessCode,
                UserName = v.UserName,
            };
        }
        public static explicit operator AccountDto(ApplicationUser u)
        {
            return new AccountDto
            {
                UserId = u.Id,
                UserName = u.UserName
            };
        }

        public static explicit operator ApplicationUser(UpdateAccountDto v)
        {
            return new ApplicationUser
            {
                Id = v.Id,
                AccessCode = v.AccessCode,
                UserName = v.UserName
            };
        }
    }
}
