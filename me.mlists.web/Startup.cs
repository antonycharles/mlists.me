using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using me.mlists.service.Services;
using me.mlists.data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using me.mlists.service.Repositories;
using me.mlists.domain.Models;
using System.IO;
using Newtonsoft.Json;

namespace me.mlists.web
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
            var connection = Configuration["ConnectionStrings:AppContextConnection"];
            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql(connection)
            );

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddTransient<IEmailService, EmailService>(i =>
                new EmailService(
                    Configuration["EmailService:Host"],
                    Configuration.GetValue<int>("EmailService:Port"),
                    Configuration.GetValue<bool>("EmailService:EnableSSL"),
                    Configuration["EmailService:UserName"],
                    Configuration["EmailService:Password"]
                )
            );

            services.AddAntiforgery(opts => opts.Cookie.Name = "mlists.antiforgery");

            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddTransient<IListaRepository, ListaRepository>();
            services.AddTransient<IMonsterRepository, MonsterRepository>();
            services.AddTransient<ITarefaRepository, TarefeRepository>();
            services.AddTransient<IParticipanteRepository, ParticipanteRepository>();
            services.AddTransient<IListaSecaoRepository, ListaSecaoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InsertMonstersJson(serviceProvider);
        }

        private void InsertMonstersJson(IServiceProvider serviceProvider)
        {
            if (serviceProvider.GetService<IMonsterRepository>().CountListaMonsters() == 0)
            {
                var json = File.ReadAllText("monsters.json");
                var monsters = JsonConvert.DeserializeObject<List<Monster>>(json);
                serviceProvider.GetService<IMonsterRepository>().SetListaMonsters(monsters);
            }
        }
    }
}
