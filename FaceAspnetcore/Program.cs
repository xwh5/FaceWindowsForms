using Face.ApplicationService.FaceService;
using FaceAspnetcore.Input;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System.Drawing;

namespace FaceAspnetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapPost("/Init", ([FromBody] InitInput input) =>
            {
                var arcsoft = new FaceService(input.Type, null,input.IsAction);

                var a= arcsoft.FaceDetector(System.Drawing.Image.FromFile("/wzp3.jpg"),out long ts);
                return a;
            });
            app.Run();
        }
    }
}