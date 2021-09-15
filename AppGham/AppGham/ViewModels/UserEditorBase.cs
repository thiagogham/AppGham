using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Shared;
using MvvmHelpers.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.ViewModels
{
    public abstract class UserEditorBase : BaseViewModel
    {
        protected readonly IUserService _userService;

        protected UserEditorBase(IUserService userService)
        {
            _userService = userService;
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            SaveCommand = new AsyncCommand(SaveAsync);
            PhotoPath = "icon.png";
        }

        public abstract IUser User { get; set; }

        public string PhotoPath { get; private set; }

        public AsyncCommand TakePhotoCommand { get; }

        public AsyncCommand SaveCommand { get; }

        public abstract Task SaveAsync();

        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
                return;

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
            if(User != null)
                User.Photo = newFile;
        }

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
    }
}