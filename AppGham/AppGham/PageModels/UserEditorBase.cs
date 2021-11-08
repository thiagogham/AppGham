using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Shared;
using AppGham.Shared.Models;
using AppGham.Validations;
using FreshMvvm;
using MvvmHelpers.Commands;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.PageModels
{
    public abstract class UserEditorBase : FreshBasePageModel
    {
        protected readonly IUserService _userService;
        protected readonly UserValidator _userValidator;

        protected UserEditorBase(IUserService userService, UserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            SaveCommand = new AsyncCommand(SaveUserAsync);
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

        private async Task SaveUserAsync()
        {
            if (Validate())
            {
                await SaveAsync();
            }
        }

        private bool Validate()
        {
            var result = _userValidator.Validate(User);
            if (!result.IsValid)
            {
                StringBuilder erros = new StringBuilder();
                foreach (var failure in result.Errors)
                {
                    _ = erros.Append($"{failure.ErrorMessage}{Environment.NewLine}");
                }

                _ = CoreMethods.DisplayAlert("Error", erros.ToString(), "Ok");
            }

            return result.IsValid;
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
            if(User != null)
                User.Photo = newFile;
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var option = await CoreMethods.DisplayActionSheet("Get Photo", "Cancel", "Capture Photo", "Pick Photo");
                FileResult file = null;
                switch (option)
                {
                    case "Capture Photo":
                        file = await MediaPicker.CapturePhotoAsync();
                        break;
                    case "Pick Photo":
                        file = await MediaPicker.PickPhotoAsync();
                        break;
                }

                await LoadPhotoAsync(file);
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