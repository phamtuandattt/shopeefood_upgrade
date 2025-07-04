using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common
{
    public static class ErrorMessage
    {
        public static IDictionary<ErrorCode, string> Messages;

        static ErrorMessage()
        {
            Messages = new Dictionary<ErrorCode, string>
            {
                {ErrorCode.Error, "Error"},
                {ErrorCode.Validation, "Validation"},
                {ErrorCode.NoPermission, "No Permission"},
                {ErrorCode.NoAccessData, "No Access Data"},
                {ErrorCode.NullReference, "Null Reference"},
                {ErrorCode.NullGeneralSetting,  "Gereral Setting entry is null or empty"},
                {ErrorCode.InvalidDomain,  "Domain is invalid"},
                {ErrorCode.NotFoundLanguage,  "Not found language"},
                {ErrorCode.ExistedDomain,  "Website domain is existed! Please choose another one!"},
                {ErrorCode.NotFoundWebsite,  "Website domain has not existed !"},
                {ErrorCode.InvalidBinNumber,  "We're sorry but the first 6 digits of your credit card is not vaid. Please try again with a new value."},
                {ErrorCode.InvalidEmail,  "Have you entered a valid email address?"},
                {ErrorCode.CustomInvalidEmail,  "This email address is already registered. Please try another email address"},
                {ErrorCode.ExistUser,  "User profile has been registered. Would you like to proceed with previous details?"},
                {ErrorCode.BinNumberRequired,  "Please enter the first 6 digits of your credit card."},
                {ErrorCode.BinNumberOnlyAllowNumber,  "Only numeric characters are allowed. Please try again."},
                {ErrorCode.SalutationRequire,  "How should we address you?"},
                {ErrorCode.FirstNameRequire,  "Tell us your first name"},
                {ErrorCode.LastNameRequire,  "Tell us your last name"},
                {ErrorCode.ContactNumberRequire,  "Please enter only numbers"},
                {ErrorCode.PasswordRequire,  "Have you entered the correct password?"},
                {ErrorCode.PasswordDoesNotMatch,  "Please ensure that your passwords match"},
                {ErrorCode.ChallengeQuestionRequired,  "Please select question"},
                {ErrorCode.ChallengeAnswerRequired,  "Please enter answer"},
                {ErrorCode.ChallengeAnswerMinLength,  "The security question answer must be at least 4 characters in length"},
                {ErrorCode.ChallengeAnswerDuplicateQuestion,  "The security question answer is too weak, answer must not be part of the question"}
            };
        }

        public static string GetValue(ErrorCode key)
        {
            return Messages[key];
        }
    }
}
