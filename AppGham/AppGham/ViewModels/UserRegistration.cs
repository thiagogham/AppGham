using AppGham.Extensions;
using AppGham.Services;
using AppGham.Services.Interfaces;
using AppGham.Shared.Models;
using MvvmHelpers.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.ViewModels
{
    public class UserRegistration : BaseViewModel
    {
        private readonly IUserService _userService;

        //public UserRegistrationViewModel(IUserService userService = null)
        public UserRegistration()
        {
            _userService = new UserService();
            PhotoPath = "icon.png";
            RegisterCommand = new AsyncCommand(RegisterAsync);
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
        }

        bool RegisterIsEnabled => !string.IsNullOrWhiteSpace(Name) ||
                                  !string.IsNullOrWhiteSpace(Email) ||
                                  !string.IsNullOrWhiteSpace(PhotoPath);

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhotoPath { get; set; }

        public AsyncCommand RegisterCommand { get; set; }

        public AsyncCommand TakePhotoCommand { get; set; }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                fnsEx.ErrorAlert();
            }
            catch (PermissionException pEx)
            {
                pEx.ErrorAlert();
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
                return;
            
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }

        async Task RegisterAsync()
        {
            try
            {
                if (!RegisterIsEnabled)
                    return;

                var user = new User
                {
                    Name = Name,
                    Email = Email,
                    Photo = PhotoPath,
                    Date = DateTime.Now
                };

                await _userService.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}

