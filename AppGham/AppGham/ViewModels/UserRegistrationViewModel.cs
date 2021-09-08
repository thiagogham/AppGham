using AppGham.Services;
using AppGham.Services.Interfaces;
using AppGham.Shared.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.ViewModels
{
    public class UserRegistrationViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        //public UserRegistrationViewModel(IUserService userService = null)
        public UserRegistrationViewModel()
        {
            _userService = new UserService();
            PhotoPath = "icon.png";
            RegisterCommand = new AsyncCommand(RegisterAsync);
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
        }

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
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
                return;
            
            // save the file into local storage
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
                var user = new User
                {
                    Name = Name,
                    Email = Email,
                    Date = DateTime.Now
                };

                await _userService.AddUserAsync(user);
                var result = await _userService.GetUsersAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}

