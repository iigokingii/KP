using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KP.dbClasses;
using System.Threading;
using KP.DBMethods.UnitOfWork;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;
using KP.DBMethods.HashPasswordMD5;

namespace KP.ViewModel
{
    internal class UserProfileViewModel:ViewModelBase
    {
        UserProfile _oldUserProfile;
        UnitOfWork unit;
        UserProfile updatedUser;

        string _userName;
        SecureString _userPassword;
        string _userEmail;
        SecureString _newUserPassword;
        SecureString _repeatNewUserPassword;
        ImageSource _userAvatar;
        string _pathToUserAvatar;
        string _errormsg;
        string _successMsg;



        public ICommand ChangeUserAvatarCommand { get; }
        public ICommand ChangeUserProfileDataCommand { get; }

        public UserProfileViewModel()
        {
            unit = new UnitOfWork();
            updatedUser = new UserProfile();
            ChangeUserAvatarCommand = new ViewModelCommandBase(ChangeUserAvatar);
            ChangeUserProfileDataCommand = new ViewModelCommandBase(ChangeUserProfileData);
            _oldUserProfile = unit.Users.GetByLogin(Thread.CurrentPrincipal.Identity.Name);
            UserName = _oldUserProfile.Login;
            UserEmail = _oldUserProfile.Email;
            UserAvatar = ConvertoBitmapImage(_oldUserProfile.Avatar);
        }

        private void ChangeUserProfileData(object obj)
        {
            if (!string.IsNullOrEmpty(SecureStringToString(UserPassword)))
            {
                ErrorMsg = "";
                if (HashMD5.HashPasswordWithMD5(SecureStringToString(UserPassword)) == _oldUserProfile.Password)
                {
                    updatedUser.reviews = _oldUserProfile.reviews;
                    updatedUser.Email = UserEmail;
                    updatedUser.ID = _oldUserProfile.ID;
                    updatedUser.Login = UserName;
                    if (!string.IsNullOrEmpty(PathToUserAvatar))
                    {
                        byte[] imageData;
                        using (FileStream fs = new FileStream(PathToUserAvatar, FileMode.Open))
                        {
                            imageData = new byte[fs.Length];
                            fs.Read(imageData, 0, imageData.Length);
                        }
                        updatedUser.Avatar = imageData;
                    }
                    else
                    {
                        updatedUser.Avatar = _oldUserProfile.Avatar;
                    }
                    updatedUser.Password = _oldUserProfile.Password;
                    if (!(string.IsNullOrEmpty(SecureStringToString(NewUserPassword))) && !(string.IsNullOrEmpty(SecureStringToString(RepeatNewUserPassword))))
                    {
                        if (SecureStringToString(NewUserPassword) == SecureStringToString(RepeatNewUserPassword))
                            updatedUser.Password = HashMD5.HashPasswordWithMD5(SecureStringToString(NewUserPassword));
                        else
                        {
                            ErrorMsg = "*Введеные новые пароли не совпадают";
                            return;
                        }
                    }
                    unit.Users.Remove(_oldUserProfile);
                    unit.Users.Add(updatedUser);
                    unit.Save();
                    SuccessMsg = "Успешно изменено! Перезапустите приложение,чтобы изменения вступили в силу.";
                }
                else
                {
                    ErrorMsg = "*Введен неправильный старый пароль";
                    return;
                }
            }
            else
            {
                ErrorMsg = "*Введите пароль для изменения данных";
                return;
            }
            
        }

        private ImageSource ConvertoBitmapImage(byte[]? frame)
        {
            MemoryStream stream = new MemoryStream(frame);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            return bitmap;
        }
        String SecureStringToString(SecureString value)
        {
            if (value != null)
            {
                IntPtr valuePtr = IntPtr.Zero;
                try
                {
                    valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                    return Marshal.PtrToStringUni(valuePtr);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                }
            }
            else
                return "";
        }
        private void ChangeUserAvatar(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image|*.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
                    image.EndInit();

                    // Замораживаем изображение
                    image.Freeze();

                    // Устанавливаем изображение в качестве источника для элемента Image
                    UserAvatar = image;

                    PathToUserAvatar = openFileDialog.FileName;
                    

                }
                catch
                {
                    System.Windows.MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public string SuccessMsg
        {
            get
            {
                return _successMsg;
            }
            set
            {
                _successMsg = value;
                OnPropertyChanged("SuccessMsg");
            }
        }
        public string ErrorMsg
        {
            get
            {
                return _errormsg;
            }
            set
            {
                _errormsg = value;
                OnPropertyChanged("ErrorMsg");
            }
        }
        public ImageSource UserAvatar
        {
            get { return _userAvatar; }
            set
            {
                _userAvatar = value;
                OnPropertyChanged(nameof(UserAvatar));
            }
        }
        public string PathToUserAvatar
        {
            get { return _pathToUserAvatar; }
            set
            {
                _pathToUserAvatar = value;
                OnPropertyChanged(nameof(PathToUserAvatar));
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public SecureString UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }
        public SecureString NewUserPassword
        {
            get { return _newUserPassword; }
            set
            {
                _newUserPassword = value;
                OnPropertyChanged("NewUserPassword");
            }
        }
        public SecureString RepeatNewUserPassword
        {
            get { return _repeatNewUserPassword; }
            set
            {
                _repeatNewUserPassword = value;
                OnPropertyChanged("RepeatNewUserPassword");
            }
        }
    }
}
