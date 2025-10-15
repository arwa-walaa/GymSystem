using GymSystemBLL;
using GymSystemDAL.Data.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace GymSystemPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Dependency Injection
            //make the dbcontext pulic in the DAL project

            builder.Services.AddDbContext<GymSystemDAL.Data.Context.GymSystemDBContext>(options => {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Generic Repo
            builder.Services.AddScoped(typeof(GymSystemDAL.Repositroies.Interfaces.IGenericRepo<>), typeof(GymSystemDAL.Repositroies.Classes.GenericRepo<>));
            builder.Services.AddScoped<GymSystemDAL.Repositroies.Interfaces.IPlanRepo, GymSystemDAL.Repositroies.Classes.PlanRepo>();
            builder.Services.AddScoped<GymSystemDAL.Repositroies.Interfaces.IUnitOfWork, GymSystemDAL.Repositroies.Classes.UnitOfWork>();
            builder.Services.AddScoped<GymSystemDAL.Repositroies.Interfaces.ISessionRepo, GymSystemDAL.Repositroies.Classes.SessionRepo>();
            builder.Services.AddAutoMapper(X=>X.AddProfile(new MappingProfiles()));
            #endregion

            var app = builder.Build();

            #region Data Seed

            var Scope = app.Services.CreateScope();
            var DbContext = Scope.ServiceProvider.GetRequiredService<GymSystemDAL.Data.Context.GymSystemDBContext>();
            //check if there is migration pending 
            var PendingMigrations = DbContext.Database.GetPendingMigrations();
            if (PendingMigrations?.Any() ?? false)
            {
                DbContext.Database.Migrate();
            }
            GymDBContextSeeding.SeedData(DbContext);

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.Run();
        }
    }
}
