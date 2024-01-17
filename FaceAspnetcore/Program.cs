using DlibDotNet.Dnn;
using Face.ApplicationService.FaceService;
using FaceAspnetcore.Input;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using System.Collections.Concurrent;
using System.Drawing;

namespace FaceAspnetcore
{
    public class Program
    {
        public static ConcurrentDictionary<string, FaceService> serviceDic=new ConcurrentDictionary<string, FaceService>();
        private static object lockobj = new object();
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
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                try
                {
                    if (!serviceDic.TryGetValue(input.Type, out var faceService))
                    {
                        faceService = new FaceService(input.Type, AppDomain.CurrentDomain.BaseDirectory+ "Images", input.IsAction);
                    }
                    // 加载图片
                    using var bitmap = SKBitmap.Decode("Images/wzp3.jpg");
                    using var bitmap2 = SKBitmap.Decode("Images/wzp2.jpg");

                    var a = faceService.FaceDetector(bitmap2);
                    var b = faceService.FaceCompare(bitmap2, bitmap, out long ts);
                    logger.LogInformation("成功");
                    return b;


                }
                catch (Exception ex)
                {
                    logger.LogError($"异常{ex.Message},{ex.StackTrace}");
                    return false;

                }
            });

            app.MapPost("/Search", ([FromBody] InitInput input) =>
            {
                if (!serviceDic.TryGetValue(input.Type, out var faceService))
                {
                    faceService = new FaceService(input.Type, AppDomain.CurrentDomain.BaseDirectory + "Images", input.IsAction);
                }
                using var bitmap = SKBitmap.Decode("SourceImage/夏伟浩.jpg");
                var b = faceService.GetName(bitmap, out long ts);
                var result = b + "耗时：" + ts+"识别引擎："+ input.Type;
                return result;
            });
            app.Run();
        }
    }
}