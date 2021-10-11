using Identity.Shared.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Shared.Dtos
{
    public class AccountDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }

        public static explicit operator AccountViewModel(AccountDto accountDto)
        {
            return new AccountViewModel
            {
                UserId = accountDto.UserId,
                UserName = accountDto.UserName,
                RoleName = accountDto.RoleName
            };
        }
    }
}
