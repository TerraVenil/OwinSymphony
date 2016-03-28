using IdentityMelody.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityMelody.Infrastructure
{
    public class IdentityMelodyDbContext : IdentityDbContext<MusicUser>
    {
        public IdentityMelodyDbContext() : base("IdentityDb")
        {
        }
    }
}