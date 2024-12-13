using BusinessAccessLayer;
using DataAccessLayer;
using Logger;
using WindowsFormApp.BusinessAccessLayer;
using WindowsFormApp.DataAccessLayer;
using WindowsFormApp.Logger;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<IBAL, BAL>();
        builder.Services.AddScoped<IDAL, DAL>();
        builder.Services.AddScoped<ILog, Log>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        //builder.Services.AddSwaggerGen();
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
