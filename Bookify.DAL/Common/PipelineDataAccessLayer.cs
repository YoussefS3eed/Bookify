using Bookify.DAL.Seed;

namespace Bookify.DAL.Common
{
    public static class PipelineDataAccessLayer
    {
        public static async Task SeedDataAsync
            (RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            await DefaultRoles.SeedAsync(roleManager);
            await DefaultUsers.SeedAdminUserAsync(userManager);
        }
    }
}
