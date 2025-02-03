using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorFest.Data;
using MotorFest.Data.Entities;
using MotorFest.Services.LocationService;
using MotorFest.Services.EngineTypeService;
using MotorFest.Services.VehicleCategoryService;
using MotorFest.Services.VehiclesService;
using MotorFest.Services.EventService;
using MotorFest.Services.EventsService;
using Microsoft.Extensions.Options;
using MotorFest.Services.UsersViewService;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using MotorFest.Services.EventsViewService;


namespace MotorFest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<MotorFestDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<ILocationService,LocationService>();
            builder.Services.AddScoped<IVehicleService,VehicleService>();
            builder.Services.AddScoped<IVehicleCategoryService,VehicleCategoryService>();
            builder.Services.AddScoped<IEngineTypeService,EngineTypeService>();
            builder.Services.AddScoped<IEventService,EventService>();
            builder.Services.AddScoped<IUsersViewService,UsersViewService>();
            builder.Services.AddScoped<IEventsViewService,EventsViewService>();
            var supportedCultures = new[] { new CultureInfo("bg-BG") }; // или "bg-BG" ако искаш запетая
            builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        var provider = options.ModelBindingMessageProvider;

        provider.SetAttemptedValueIsInvalidAccessor((x, y) => $"Стойността \"{x}\" не е валидна за {y}.");
        provider.SetMissingBindRequiredValueAccessor(x => $"Полето {x} е задължително.");
        provider.SetMissingKeyOrValueAccessor(() => "Това поле е задължително.");
        provider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => $"Стойността \"{x}\" не е валидна.");
        provider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "Въведена е невалидна стойност.");
        provider.SetNonPropertyValueMustBeANumberAccessor(() => "Трябва да бъде число.");
        provider.SetUnknownValueIsInvalidAccessor(x => $"Стойността за {x} е невалидна.");
        provider.SetValueIsInvalidAccessor(x => $"Стойността за {x} е невалидна.");
        provider.SetValueMustBeANumberAccessor(x => $"Полето {x} трябва да бъде число.");
        provider.SetValueMustNotBeNullAccessor(x => $"Полето {x} не може да бъде празно.");
    });
            builder.Services.AddDefaultIdentity<MFUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<MotorFestDbContext>();
            builder.Services.AddControllersWithViews();
            
            var app = builder.Build();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("bg-BG"),
                SupportedCultures = new List<CultureInfo> { new CultureInfo("bg-BG") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("bg-BG") }
            });
            using (var scope = app.Services.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<MotorFestDbContext>();

                dbcontext.Database.EnsureCreated();
                if (!dbcontext.Roles.Any())
                {
                    dbcontext.Add(new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR",
                        ConcurrencyStamp = Guid.NewGuid().ToString()

                    });
                    dbcontext.Add(new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Participant",
                        NormalizedName = "PARTICIPANT",
                        ConcurrencyStamp = Guid.NewGuid().ToString()

                    });
                    dbcontext.Add(new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Organizer",
                        NormalizedName = "ORGANIZER",
                        ConcurrencyStamp = Guid.NewGuid().ToString()

                    });
                }
                if (!dbcontext.EngineTypes.Any())
                {
                    dbcontext.Add(new EngineType
                    {

                        Name = "Бензинов двигател"
                    });
                    dbcontext.Add(new EngineType
                    {

                        Name = "Дизелов двигател"
                    });
                    dbcontext.Add(new EngineType
                    {

                        Name = "Електрически двигател"
                    }); dbcontext.Add(new EngineType
                    {

                        Name = "Хибриден двигател"
                    });
                }
                if (!dbcontext.VehicleCategories.Any())
                {
                    dbcontext.Add(new VehicleCategory
                    {
                        Name = "Автомобил"
                    });
                    dbcontext.Add(new VehicleCategory
                    {
                        Name = "Мотор"
                    });
                    dbcontext.Add(new VehicleCategory
                    {
                        Name = "Прототип"
                    });

                }
                dbcontext.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
