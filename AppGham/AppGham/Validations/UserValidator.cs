using AppGham.Shared;
using FluentValidation;

namespace AppGham.Validations
{
    public enum UserPageValidator
    {
        LoginPage,
        UserSignUpPage,
        UserEditorPage
    }

    public class UserValidator : AbstractValidator<IUser>
    {
        public UserValidator(UserPageValidator page)
        {
            switch (page)
            {
                case UserPageValidator.LoginPage:
                    ValidateEmail();
                    ValidatePassword();
                    break;
                case UserPageValidator.UserSignUpPage:
                    ValidateName();
                    ValidateEmail();
                    ValidatePassword();
                    break;
                case UserPageValidator.UserEditorPage:
                    ValidateName();
                    ValidateEmail();
                    break;
            }
        }

        private void ValidateName()
        {
            _ = RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required")
                                    .MaximumLength(150).WithMessage("Name maximum length 150")
                                    .MinimumLength(5).WithMessage("Name minimum length 5");
        }

        private void ValidateEmail()
        {
            _ = RuleFor(u => u.Email).NotEmpty().WithMessage("Email invalid")
                                     .EmailAddress().WithMessage("Email invalid");
        }

        private void ValidatePassword()
        {
            _ = RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
                                        .MinimumLength(6).WithMessage("Password minimum length 6");
        }
    }
}
