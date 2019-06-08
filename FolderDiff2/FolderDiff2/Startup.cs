using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FolderDiff2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FolderDiff2
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
			IConfiguration authSection = Configuration.GetSection("AuthServerOptions");
			services.Configure<AuthServerOptions>(authSection);

			IConfiguration apiSection = Configuration.GetSection("ApiOptions");
			services.Configure<ApiOptions>(apiSection);

			services.AddScoped<AuthorizationHelperService>();
			services.AddScoped<ApiHelperService>();
			services.AddScoped<FolderComparisonService>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
