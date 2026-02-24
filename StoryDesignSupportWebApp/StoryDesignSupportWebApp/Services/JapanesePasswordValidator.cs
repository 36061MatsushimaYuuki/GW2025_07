using Microsoft.AspNetCore.Identity;

namespace StoryDesignSupportWebApp.Services {

    public class JapanesePasswordValidator<TUser> : PasswordValidator<TUser> where TUser : class {
        public override async Task<IdentityResult> ValidateAsync(
            UserManager<TUser> manager,
            TUser user,
            string password) {
            var result = await base.ValidateAsync(manager, user, password);

            if (result.Succeeded)
                return result;

            var errors = result.Errors.Select(e => new IdentityError {
                Code = e.Code,
                Description = Translate(e.Code)
            });

            return IdentityResult.Failed(errors.ToArray());
        }

        private string Translate(string code) {
            return code switch {
                "PasswordTooShort" => "パスワードが短すぎます。",
                "PasswordRequiresNonAlphanumeric" => "パスワードには記号を1つ以上含めてください。",
                "PasswordRequiresDigit" => "パスワードには数字を1つ以上含めてください。",
                "PasswordRequiresUpper" => "パスワードには大文字を1つ以上含めてください。",
                "PasswordRequiresLower" => "パスワードには小文字を1つ以上含めてください。",
                "PasswordRequiresUniqueChars" => "パスワードにはより多くの異なる文字を含めてください。",
                _ => "パスワードが無効です。"
            };
        }
    }
}
