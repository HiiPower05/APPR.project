using APPR.coreproject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APPR.coreproject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database connection
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Strengthened ASP.NET Identity configuration
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                // Sign-in settings
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;

                // Password requirements
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Account lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Optional test-account seeding
            // Optional test-account seeding
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                // Create roles if they do not already exist
                string[] roles = { "Employee", "Donor" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var employeeEmail =
                    builder.Configuration["TestAccounts:Employee:Email"];

                var employeePassword =
                    builder.Configuration["TestAccounts:Employee:Password"];

                var donorEmail =
                    builder.Configuration["TestAccounts:Donor:Email"];

                var donorPassword =
                    builder.Configuration["TestAccounts:Donor:Password"];

                // Create Employee test account if configured
                if (!string.IsNullOrWhiteSpace(employeeEmail) &&
                    !string.IsNullOrWhiteSpace(employeePassword))
                {
                    var employee = await userManager.FindByEmailAsync(employeeEmail);

                    if (employee == null)
                    {
                        employee = new ApplicationUser
                        {
                            UserName = employeeEmail,
                            Email = employeeEmail,
                            EmailConfirmed = true
                        };

                        var result = await userManager.CreateAsync(
                            employee,
                            employeePassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine(
                                    $"Employee test account creation error: {error.Description}");
                            }
                        }
                    }

                    if (employee != null &&
                        !await userManager.IsInRoleAsync(employee, "Employee"))
                    {
                        await userManager.AddToRoleAsync(employee, "Employee");
                    }
                }

                // Create Donor test account if configured
                if (!string.IsNullOrWhiteSpace(donorEmail) &&
                    !string.IsNullOrWhiteSpace(donorPassword))
                {
                    var donor = await userManager.FindByEmailAsync(donorEmail);

                    if (donor == null)
                    {
                        donor = new ApplicationUser
                        {
                            UserName = donorEmail,
                            Email = donorEmail,
                            EmailConfirmed = true
                        };

                        var result = await userManager.CreateAsync(
                            donor,
                            donorPassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine(
                                    $"Donor test account creation error: {error.Description}");
                            }
                        }
                    }

                    if (donor != null &&
                        !await userManager.IsInRoleAsync(donor, "Donor"))
                    {
                        await userManager.AddToRoleAsync(donor, "Donor");
                    }
                }
            }


            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Authentication must come before authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}