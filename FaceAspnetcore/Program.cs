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
                    // ��ImageSharp��Image<Rgba32>���浽�ڴ�����
                    using var memoryStream = new MemoryStream();
                    img.SaveAsBmp(memoryStream);

                    // ���ڴ������õ���ʼλ��
                    memoryStream.Position = 0;

                    // ���ڴ���ת��Ϊ�ֽ�����
                    byte[] imageData = memoryStream.ToArray();

                    // ��ȡͼ��Ŀ�Ⱥ͸߶�
                    int width = img.Width;
                    int height = img.Height;

                    // ���ڴ������õ���ʼλ��
                    memoryStream.Position = 0;
                    var a = arcsoft.FaceDetector(bitmap, out long ts);
                    return a;
                }
              
   

            });
            app.Run();
        }
    }
}