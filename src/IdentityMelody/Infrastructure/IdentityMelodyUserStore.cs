using IdentityMelody.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityMelody.Infrastructure
{
    /// <summary>
    /// Additional information: For the container to be able to create UserStore<MusicUser> it should have only one public constructor: it has 2. See https://simpleinjector.org/one-constructor for more information.
    /// </summary>
    public class IdentityMelodyUserStore : UserStore<MusicUser>
    {
        public IdentityMelodyUserStore(IdentityMelodyDbContext context)
            : base(context)
        {
        }
    }
}