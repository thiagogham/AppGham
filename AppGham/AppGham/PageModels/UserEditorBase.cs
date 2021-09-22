using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Shared;
using AppGham.Shared.Models;
using FreshMvvm;
using MvvmHelpers.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.PageModels
{
    public abstract class UserEditorBase : FreshBasePageModel
    {
        protected readonly IUserService _userService;

        protected UserEditorBase(IUserService userService)
        {
            _userService = userService;
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            SaveCommand = new AsyncCommand(SaveAsync);
            PhotoPath = "icon.png";
            User = new User();
        }

        public override void Init(object initData)
        {
            User = initData as IUser ?? new User();
        }

        public IUser User { get; set; }

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