using IdentityMelody.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityMelody.Infrastructure
{
    public class IdentityMelodyUserManager : UserManager<MusicUser>
    {
        public IdentityMelodyUserManager(IUserStore<MusicUser> store) : base(store)
        {
            UserValidator = new UserValidator<MusicUser>(this) { RequireUniqueEmail = true };
        }

        public static IdentityMelodyUserManager Create()
        {
            var manager = new IdentityMelodyUserManager(new UserStore<MusicUser>(new IdentityMelodyDbContext()));

            manager.UserValidator = new UserValidator<MusicUser>(manager) { RequireUniqueEmail = true };

            return manager;
        }
    }
}