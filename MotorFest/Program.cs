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
           builder.Services.AddLogging();
            builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        var provider = options.ModelBindingMessageProvider;

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
