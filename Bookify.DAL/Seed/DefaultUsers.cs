using Bookify.DAL.Common.Consts;

namespace Bookify.DAL.Seed
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser adminUser = new()
            {
                UserName = "admin",
                Email = "admin@bookify.com",
                FullName = "Admin",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(adminUser, "P@ssword123");
                await userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
            }
        }
    }
}
