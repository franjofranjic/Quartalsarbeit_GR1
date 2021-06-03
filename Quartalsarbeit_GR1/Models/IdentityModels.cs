using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;


namespace Quartalsarbeit_GR1.Models
{
    // Sie können Profildaten für den Benutzer hinzufügen, indem Sie der ApplicationUser-Klasse weitere Eigenschaften hinzufügen. Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(255)]
        public string Vorname { get; set; }

        [Required]
        [StringLength(255)]
        public string Nachname { get; set; }

        [Required]
        public int TelPrivat { get; set; }

        [Required]
        public int TelGeschaeft { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Beachten Sie, dass der "authenticationType" mit dem in "CookieAuthenticationOptions.AuthenticationType" definierten Typ übereinstimmen muss.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Benutzerdefinierte Benutzeransprüche hier hinzufügen
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Disziplin> Disziplinen { get; set; }
        public DbSet<Kategorie> Kategorien { get; set; }
        public DbSet<Verein> Vereine { get; set; }
        public DbSet<Athlet> Athleten { get; set; }
        public DbSet<Anlass> Anlaesse { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Teilnehmer> Teilnehmer { get; set; }
        public DbSet<Startnummernblock> Startnummernblock { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}