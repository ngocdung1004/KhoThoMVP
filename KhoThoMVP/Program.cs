
using KhoThoMVP.Interfaces;
using KhoThoMVP.Mappers;
using KhoThoMVP.Models;
using KhoThoMVP.Services;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<KhoThoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Thêm các dịch vụ vào DI container
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IJobTypeService, JobTypeService>();
            builder.Services.AddScoped<IWorkerJobTypeService, WorkerJobTypeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
