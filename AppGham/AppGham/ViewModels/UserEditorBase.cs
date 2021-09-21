using AppGham.Extensions;
using AppGham.Interfaces;
using AppGham.Services.Interfaces;
using AppGham.Shared;
using AppGham.Shared.Models;
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
        protected readonly IDialogService _dialogService;

        protected UserEditorBase(IUserService userService, IDialogService dialogService)
        {
            _userService = userService;
            _dialogService = dialogService;
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            SaveCommand = new AsyncCommand(SaveAsync);
            PhotoPath = "icon.png";
            User = new User();
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
                await LoadPhotoAsync(await MediaPicker.CapturePhotoAsync());
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