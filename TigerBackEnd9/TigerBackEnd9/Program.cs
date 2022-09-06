using Microsoft.EntityFrameworkCore;
using TigerBackEnd5.Data;

namespace TigerBackEnd5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Set CORS
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            // Add database context
            builder.Services.AddDbContext<TigerContext>(opts =>
                opts.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                await next();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}