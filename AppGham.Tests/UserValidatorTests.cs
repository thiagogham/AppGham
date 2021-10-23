using AppGham.Shared.Models;
using AppGham.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AppGham.Tests
{
    public class UserValidatorTests
    {
        [Fact]
        public void Should_return_false_validation_when_user_empty()
        {
            //Arrange 
            var userValidator = new UserValidator(UserPageValidator.LoginPage);

            //Act  
            var result = userValidator.Validate(new User());

            //Assert
            Assert.False(result.IsValid, GetErros(result.Errors));
        }

        private string GetErros(List<ValidationFailure> errors)
        {
            StringBuilder erros = new StringBuilder();
            foreach (var failure in errors)
            {
                _ = erros.Append($"{failure.ErrorMessage}{Environment.NewLine}");
            }

            return erros.ToString();
        }
    }
}
