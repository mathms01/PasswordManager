using PasswordsManager.Models;
using PasswordsManager.Utils;
using PasswordsManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordsManager.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private MainViewModel _viewModel;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> _propertyValues = new Dictionary<string, object>();

        public T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (_propertyValues.ContainsKey(propertyName))
                return (T)_propertyValues[propertyName];
            return default(T);
        }
        public bool SetValue<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            var currentValue = GetValue<T>(propertyName);
            if (currentValue == null && newValue != null
             || currentValue != null && !currentValue.Equals(newValue))
            {
                _propertyValues[propertyName] = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }

        public async Task sendPassAsync()
        {
            await _viewModel.AddPasswordToDB(labelText.Text.ToString(), urlText.Text.ToString(), loginText.Text.ToString(), passwordText.Text.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string words = tagsText.Text.ToString();
            List<Tag> tagsStr = new List<Tag>();

            string[] split = words.Split(new Char[] { ' ', ',', '.', ':', '\t' });

            foreach (string s in split)
            {

                if (s.Trim() != "")
                {
                    Tag tg = new Tag();
                    tg.Label = s;
                    tagsStr.Add(tg);
                }
            }

            sendPassAsync();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Models.Password pass = (Models.Password)ListBoxPass.SelectedItem;

            if (pass != null)
            {
                _viewModel.PassWordClear = Crypto.Decrypt(pass.Pass);
            }
        }

        public async Task DeletBDAsync()
        {
            Models.Password pass = (Models.Password)ListBoxPass.SelectedItem;

            if (pass != null)
            {
                await _viewModel.AddDeleteToDB(pass);
            }
        }

        public async Task SearchDB()
        {
            await _viewModel.SearchToDB(searchText.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DeletBDAsync();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SearchDB();
        }
    }
}
