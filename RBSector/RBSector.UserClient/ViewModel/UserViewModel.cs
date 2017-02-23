using RBSector.UserClient.Models;
using RBSector.UserClient.Mvvm;
using System;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media;

namespace RBSector.UserClient.ViewModel
{
    internal class UserViewModel : ViewModelBase
    {
        private User _model;
        private ImageSource picture = null;
        private DelegateCommand uploadImageCommand;

        public UserViewModel(User model)
        {
            _model = model;
            uploadImageCommand = new DelegateCommand(UploadImage_Executed);
        }

        public User Model
        {
            get
            {
                return _model;
            }

            set
            {
                this.SetProperty(ref _model, value);
                picture = null;
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged(string.Empty);
            }
        }

        public int Id
        {
            get
            {
                return _model?.Id ?? int.MaxValue;
            }

            set
            {
                if (_model != null)
                {
                    _model.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Login
        {
            get
            {
                return _model?.Login ?? string.Empty;
            }

            set
            {
                if (_model != null)
                {
                    _model.Login = value;
                    OnPropertyChanged();
                }
            }
        }


        public string Password
        {
            get
            {
                return _model?.Password ?? string.Empty;
            }

            set
            {
                if (_model != null)
                {
                    _model.Password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get
            {
                return _model?.Email ?? string.Empty;
            }

            set
            {
                if (_model != null)
                {
                    _model.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public uint Pin
        {
            get
            {
                return _model?.Pin ?? uint.MaxValue;
            }

            set
            {
                if (_model != null)
                {
                    _model.Pin = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DayOfBirth
        {
            get
            {
                return _model?.DayOfBirth ?? new DateTime();
            }

            set
            {
                if (_model != null)
                {
                    _model.DayOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get
            {
                return _model?.FirstName ?? string.Empty;
            }

            set
            {
                if (_model != null)
                {
                    _model.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get
            {
                return _model?.LastName ?? string.Empty;
            }

            set
            {
                if (_model != null)
                {
                    _model.LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MiddleName
        {
            get
            {
                return _model?.MiddleName ?? string.Empty;
            }

            set
            {
                if (_model != null)
                {
                    _model.MiddleName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                if (_model != null && picture == null)
                {
                    picture = _model.Picture.AsBitmapImage();
                }

                return picture;
            }
        }

        public byte[] Picture
        {
            set
            {
                picture = null;
                _model.Picture = value;
                OnPropertyChanged("ImageSource");
            }
        }

        public ICommand UploadImageCommand
        {
            get { return uploadImageCommand; }
        }

        private async void UploadImage_Executed()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile imgFile = await openPicker.PickSingleFileAsync();
            if (imgFile != null)
            {
                Picture = await imgFile.AsByteArray();
            }
        }
    }
}
