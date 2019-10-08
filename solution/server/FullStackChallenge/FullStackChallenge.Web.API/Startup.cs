using System.Collections.Generic;
using FullStackChallenge.Data.CommandHandlers;
using FullStackChallenge.Data.Commands;
using FullStackChallenge.Data.Config;
using FullStackChallenge.Data.Dto.Employee;
using FullStackChallenge.Data.Models;
using FullStackChallenge.Data.Neo4j;
using FullStackChallenge.Data.Neo4j.Repositories;
using FullStackChallenge.Data.Queries;
using FullStackChallenge.Data.QueryHandlers;
using FullStackChallenge.Data.Repositories;
using FullStackChallenge.Data.Repositories.Core;
using FullStackChallenge.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullStackChallenge.Web.API
{
    public class Startup
    {
        public const string CookieScheme = "foo";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            // Add Repositories
            services.AddTransient<IEmployeeRepository, EmployeeRepository>(c => new EmployeeRepository(c.GetService<IBaseRepository<Employee>>()));
            services.AddTransient<IReviewRepository, ReviewRepository>(c => new ReviewRepository(c.GetService<INeo4jBaseRepository<Review>>()));
            
            // Add CommandHandlers
            services
                .AddTransient<ICommandHandler<UpdateEmployeeReviewAndAssigneeCommand>, UpdateEmployeeReviewAndAssigneeCommandHandler>();
            
            // Add QueryHandlers
            services
                .AddTransient<IQueryHandler<GetEmployeesWithReviewAndAssigneeQuery, List<EmployeeDto>>, EmployeesQueryHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddAuthentication(CookieScheme)
                .AddCookie(CookieScheme, options =>
                {
                    options.AccessDeniedPath = "/account/denied";
                    options.LoginPath = "/account/login";
                });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.Configure<DbOptions>(Configuration.GetSection("Database"));

            var dbDriver = Configuration.GetValue<string>("Database:driver");

            switch (dbDriver)
            {
                case "neo4j":
                    services.AddTransient(typeof(IBaseRepository<>), typeof(Neo4jBaseRepository<>));
                    services.AddTransient(typeof(IQueryBuilder), typeof(QueryBuilder));
                    services.AddTransient(typeof(INeo4jBaseRepository<>), typeof(Neo4jBaseRepository<>));
                    break;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
