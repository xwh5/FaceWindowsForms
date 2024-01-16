using Face.ApplicationService.FaceService;
using FaceAspnetcore.Input;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using System.Drawing;
using Image = SixLabors.ImageSharp.Image;


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

                using (var img = Image.Load("wzp3.jpg"))
                {
                    // 将ImageSharp的Image<Rgba32>保存到内存流中
                    using var memoryStream = new MemoryStream();
                    img.SaveAsBmp(memoryStream);

                    // 将内存流重置到起始位置
                    memoryStream.Position = 0;

                    // 将内存流转换为字节数组
                    byte[] imageData = memoryStream.ToArray();

                    // 获取图像的宽度和高度
                    int width = img.Width;
                    int height = img.Height;

                    // 将内存流重置到起始位置
                    memoryStream.Position = 0;
                    var a = arcsoft.FaceDetector(bitmap, out long ts);
                    return a;
                }
              
   

            });
            app.Run();
        }
    }
}