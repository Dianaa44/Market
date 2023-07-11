using DatabaseContext.Models;
using DatabaseContext.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Services;
using Web.Data;



namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MarketContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

          /*  services.AddDefaultIdentity<ApplicationUser>(options =>options.SignIn.RequireConfirmedEmail = false)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<MarketContext>().AddDefaultTokenProviders();*/
            
            //{
            //    configs.User.RequireUniqueEmail = true;
            //    configs.SignIn.RequireConfirmedPhoneNumber = false;
            //    configs.SignIn.RequireConfirmedEmail = false;
            //}).AddRoles<IdentityRole>().AddEntityFrameworkStores<MarketContext>();

            services.ConfigureApplicationCookie(configs =>
            {
                configs.LoginPath = "/Account/Login";
                configs.LogoutPath = "/Account/Logout";
            });


            //AddSingleton creates the object the first time it is requested and then every subsequent request will use the same instance
            //services.AddSingleton<IUnitOfWork, UnitOfWork>();

            //AddScoped creates the object once per request
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //AddTransient creates the object each time it is requested
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            // See example: https://www.c-sharpcorner.com/article/understanding-addtransient-vs-addscoped-vs-addsingleton-in-asp-net-core/
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            

            //services.AddScoped<UserManager<IdentityUser>>();
            //services.AddScoped<SignInManager<IdentityUser>>();


            services.AddScoped<IScopedService, AppService>();
            services.AddSingleton<ISingletonService, AppService>();
            services.AddTransient<ITransientService, AppService>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
               // app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
            /*   using (var scope = app.ApplicationServices.CreateScope())
               {
                   var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                   var roles = new[] { "Admin" };
                   foreach(var role in roles)
                   {
                       if (!await roleManager.RoleExistsAsync(role))
                           await roleManager.CreateAsync(new IdentityRole(role));
                   }
               }


               using (var scope = app.ApplicationServices.CreateScope())
               {
                   var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                   string email = "admin@admin.com";
                   string password = "test1234$$SS";


                   if (await userManager.FindByEmailAsync(email) == null)
                   {
                       var user = new ApplicationUser();
                       user.UserName = email;
                       user.Email = email;
                       //                    user.EmailConfirmed = true;

                       var result = await userManager.CreateAsync(user, password);
                       if (result.Succeeded)
                       {
                           await userManager.AddToRoleAsync(user, "Admin");
                       }
                       //await userManager.CreateAsync(user, password);

                       //await userManager.AddToRoleAsync(user, "Admin");

                   }

               }*/
            //DbSeeder.SeedAsync(userManager, roleManager).Wait();


        }
    }
}


//1. ibaseRepo + baseRepo
//2. iUOW + UOW
//3.icustomerRepo + customerRepo