
using Microsoft.EntityFrameworkCore;
using OrderCreator.Application.Services;
using OrderCreator.Core.Abstractions;
using OrderCreator.DataAccess;
using OrderCreator.DataAccess.Repositories;

namespace OrderCreator.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Container.
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderCreatorDbContext>(
                options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderCreatorDbContext"));
                });

            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

            var app = builder.Build();

            // Pipeline.
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
