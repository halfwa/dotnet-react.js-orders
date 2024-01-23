using OrderCreator.DataAccess;

namespace OrderCreator.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Container
        builder.Services
            .AddPresentation()
            .AddDataAccess(builder.Configuration);

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("reactApp", builder => 
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()   
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        var app = builder.Build();
        
        //Pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.UseCors("reactApp");

        PrepDb.PrepPopulation(app);
        app.Run();
        
    }
}

