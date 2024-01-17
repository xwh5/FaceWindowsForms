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
                    // ����ͼƬ
                    using var bitmap = SKBitmap.Decode("Images/wzp3.jpg");
                    using var bitmap2 = SKBitmap.Decode("Images/wzp2.jpg");

                    var a = faceService.FaceDetector(bitmap2);
                    var b = faceService.FaceCompare(bitmap2, bitmap, out long ts);
                    logger.LogInformation("�ɹ�");
                    return b;


                }
                catch (Exception ex)
                {
                    logger.LogError($"�쳣{ex.Message},{ex.StackTrace}");
                    return false;

                }
            });

            app.MapPost("/Search", ([FromBody] InitInput input) =>
            {
                if (!serviceDic.TryGetValue(input.Type, out var faceService))
                {
                    faceService = new FaceService(input.Type, AppDomain.CurrentDomain.BaseDirectory + "Images", input.IsAction);
                }
                using var bitmap = SKBitmap.Decode("SourceImage/��ΰ��.jpg");
                var b = faceService.GetName(bitmap, out long ts);
                var result = b + "��ʱ��" + ts+"ʶ�����棺"+ input.Type;
                return result;
            });
            app.Run();
        }
    }
}