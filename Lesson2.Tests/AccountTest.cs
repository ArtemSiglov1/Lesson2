using Lesson2.Data;
using Lesson2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Tests
{
    public class AccountTest
    {
        
        private readonly Configuration _configuration = new Configuration();
        private AccountService _accountService => _configuration.GetRequiredService<AccountService>();
        [Fact(DisplayName = "Обновление аккаунта(успешный кейс)")]
        public async Task SuccessUpdateAccount()
        {
            string login = "ArtemSiglov";
            string password = "Qwerty555";
            
            await _accountService.UpdateAccount(1,login,password);
            var account = await _accountService.GetAccounts(1);
            Assert.Equal(account.Login,login);
            Assert.Equal(account.Password,password);
        }
        [Fact(DisplayName = "Удаление аккаунта(успешный кейс)")]
        public async Task SuccessRemoveAccount()
        {
            int id=1;
            await _accountService.RemoveAccounts(id);
           var account= await _accountService.GetAccounts(id);
            Assert.Null(account);
        }

    }
}
