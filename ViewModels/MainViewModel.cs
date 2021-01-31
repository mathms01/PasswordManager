using PasswordsManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordsManager.ViewModels
{
    public class MainViewModel : BaseNotifyPropertyChanged
    {
        public List<Models.Password> PasswordList
        {
            get
            {
                return DataAccess.PasswordsDbContext.Current.Passwords.ToList();
            }
        }

        public string PassWordClear
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public async Task AddPasswordToDB(string label, string url, string login, string password)
        {
            var context = DataAccess.PasswordsDbContext.Current;

            context.Add(
            new Models.Password()
            {
                Label = label,
                Login = login,
                Pass = Crypto.Encrypt(password),
                Url = url
            });

            await context.SaveChangesAsync();

            OnPropertyChanged(nameof(PasswordList));
        }

        public async Task AddDeleteToDB(Models.Password psw)
        {
            var context = DataAccess.PasswordsDbContext.Current;

            context.Remove(psw);

            await context.SaveChangesAsync();

            OnPropertyChanged(nameof(PasswordList));
        }

        public async Task SearchToDB(String str)
        {
            var context = DataAccess.PasswordsDbContext.Current;

            context.Passwords.Where(x => x.Label.Contains(str));

            await context.SaveChangesAsync();

            OnPropertyChanged(nameof(PasswordList));
        }
    }
}
