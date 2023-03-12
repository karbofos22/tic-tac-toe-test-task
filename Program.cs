using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using tic_tac_toe_testTask.Models;

namespace tic_tac_toe_testTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GameContext>(options =>
           options.UseSqlServer((builder.Configuration.GetConnectionString("DefaultConnection"))));

            builder.Services.AddControllers();
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


            app.MapControllers();

            app.Run();
        }
    }
}