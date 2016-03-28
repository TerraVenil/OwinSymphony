using IdentityMelody.Models;
using Microsoft.AspNet.Identity;

namespace IdentityMelody.Infrastructure
{
    public class IdentityMelodyUserManager : UserManager<MusicUser>
    {
        public IdentityMelodyUserManager(IUserStore<MusicUser> store) : base(store)
        {
            UserValidator = new UserValidator<MusicUser>(this) { RequireUniqueEmail = true };
        }
    }
}