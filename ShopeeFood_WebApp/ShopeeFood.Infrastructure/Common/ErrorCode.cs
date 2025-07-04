using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common
{
    public enum ErrorCode
    {
        Error = 1,
        Validation = 2,
        NoPermission = 3,
        NoAccessData = 4,
        NullReference = 5,
        NullGeneralSetting = 6,
        InvalidDomain = 7,
        NotFoundLanguage = 8,
        ExistedDomain = 9,
        NotFoundWebsite = 10,
        InvalidBinNumber = 11,
        InvalidEmail = 12,
        BinNumberRequired = 13,
        BinNumberOnlyAllowNumber = 14,
        SalutationRequire = 15,
        FirstNameRequire = 16,
        LastNameRequire = 17,
        ContactNumberRequire = 18,
        PasswordRequire = 19,
        PasswordDoesNotMatch = 20,
        CustomInvalidEmail = 21,
        ExistUser = 22,
        ChallengeQuestionRequired = 23,
        ChallengeAnswerRequired = 24,
        ChallengeAnswerMinLength = 25,
        ChallengeAnswerDuplicateQuestion = 26,
        CCNumberRequired = 27,
        CCNumberOnlyAllowNumber = 28
    }
}
