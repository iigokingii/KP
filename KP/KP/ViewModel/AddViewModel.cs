using KP.dbClasses;
using KP.DBMethods.UnitOfWork;
using KP.ViewModel.BaseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KP.ViewModel
{
    internal class AddViewModel:ViewModelBase
    {
        UnitOfWork unit;
        List<byte[]> frames;
        string _movieName;
        string _genre;
        ImageSource _image;
        ImageSource _frame1;
        ImageSource _frame2;
        ImageSource _frame3;
        ImageSource _frame4;

        string _errorMsg;
        string PathToPoster;
        string _date;
        string _country;
        string _description;

        public ICommand AddPosterImage { get; }
        public ICommand AddMiniItemInfoCommand { get; }
        public ICommand AddFrame1Command { get; }
        public ICommand AddFrame2Command { get; }
        public ICommand AddFrame3Command { get; }


        public AddViewModel()
        {
            AddPosterImage = new ViewModelCommandBase(AddPoster);
            AddMiniItemInfoCommand = new ViewModelCommandBase(AddMiniItemInfo);
            AddFrame1Command = new ViewModelCommandBase(addFrame1);
            AddFrame2Command = new ViewModelCommandBase(addFrame2);
            AddFrame3Command = new ViewModelCommandBase(addFrame3);
            unit = new UnitOfWork();
            frames = new List<byte[]>();
        }

        private void addFrame3(object obj)
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
                    Frame3 = image;

                    string Path = openFileDialog.FileName;
                    byte[] imageData;
                    using (FileStream fs = new FileStream(Path, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }
                    frames.Add(imageData);
                    string t = "";
                }
                catch
                {
                    System.Windows.MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void addFrame2(object obj)
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
                    Frame2 = image;

                    string Path = openFileDialog.FileName;
                    byte[] imageData;
                    using (FileStream fs = new FileStream(Path, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }
                    frames.Add(imageData);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void addFrame1(object obj)
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
                    Frame1 = image;

                    string Path = openFileDialog.FileName;
                    byte[] imageData;
                    using (FileStream fs = new FileStream(Path, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }
                    frames.Add(imageData);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public ImageSource Frame4
        {
            get
            {
                return _frame4;
            }
            set
            {
                _frame4 = value;
                OnPropertyChanged("Frame4");
            }
        }
        public ImageSource Frame3
        {
            get
            {
                return _frame3;
            }
            set
            {
                _frame3 = value;
                OnPropertyChanged("Frame3");
            }
        }
        public ImageSource Frame2
        {
            get
            {
                return _frame2;
            }
            set
            {
                _frame2 = value;
                OnPropertyChanged("Frame2");
            }
        }
        public ImageSource Frame1
        {
            get
            {
                return _frame1;
            }
            set
            {
                _frame1 = value;
                OnPropertyChanged("Frame1");
            }
        }
        public ImageSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }
        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
                OnPropertyChanged("ErrorMsg");
            }
        }

        public string movieName
        {
            get
            {
                return _movieName;
            }
            set
            {
                _movieName = value;
                OnPropertyChanged("movieName");
            }
        }
        private void AddMiniItemInfo(object obj)
        {
            MiniItemInfo item = new MiniItemInfo();

            if (String.IsNullOrEmpty(movieName))
            {
                ErrorMsg = "* Введите название";
                return;
            }
            else if (String.IsNullOrEmpty(Genre))
            {
                ErrorMsg = "* Введите жанр";
                return;
            }
            else if (Image == null)
            {
                ErrorMsg = "* Добавьте постер";
                return;
            }
            else if (String.IsNullOrEmpty(Date))
            {
                ErrorMsg = "* Введите дату";
                return;
            }
            //TODO
            /*string template = "(0?[1-9]|[12][0-9]|3[01]) (янв(?:аря)?|фев(?:раля)?|мар(?:та)?|апр(?:еля)?|мая|июн(?:я)?|июл(?:я)?|авг(?:уста)?|сен(?:тября)?|окт(?:ября)?|ноя(?:бря)?|дек(?:абря)?)/";
            Regex regex = new Regex(template);
            MatchCollection match = regex.Matches(Date);
            if (match.Count != 1)
            {
                ErrorMsg = "* Дата введена некорректно (гггг месяц дд)";
            }*/
            else
            {
                item.Genre = Genre;
                item.Name = movieName;
                //img->byte[]
                byte[] imageData;
                using (FileStream fs = new FileStream(PathToPoster, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                item.SmallIMG = imageData;
                item.Year = Date;
                unit.MiniItemInfoRepository.Add(item);
                Date = "";
                ErrorMsg = "";
                Genre = "";
                movieName = "";
                Image = null;

            }

        }
        private void AddPoster(object obj)
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
                    Image = image;

                    PathToPoster = openFileDialog.FileName;

                }
                catch
                {
                    System.Windows.MessageBox.Show("Выберите файл подходящего формата.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}
