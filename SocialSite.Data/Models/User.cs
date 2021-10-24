using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SocialSite.Data.Models
{
    [Read(SecurityPermissionLevels.DenyAll)]
    [Edit(SecurityPermissionLevels.DenyAll)]
    [Create(SecurityPermissionLevels.DenyAll)]
    [Delete(SecurityPermissionLevels.DenyAll)]
    public class User
    {
        public int UserId { get; set; }

        public string ScreenName { get; set; }

        public string ProfilePictureLink { get; set; }

        [Coalesce]
        public static async Task<ICollection<User>> GetUsersOnPage(
            [Inject] AppDbContext db,
            params string[] screenNames)
        {
            return await db.Users.Where(x => screenNames.Contains(x.ScreenName)).Distinct().ToListAsync();
        }
    }
}
