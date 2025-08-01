
using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.Repositories;
using SW_MES_API.Repositories.Admin;
using SW_MES_API.Repositories.Common;
using SW_MES_API.Repositories.Login;
using SW_MES_API.Services;
using SW_MES_API.Services.Admin;
using SW_MES_API.Services.Common;
using SW_MES_API.Services.Login;

namespace SW_MES_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //ASP.NET Core에 서비스와 레포지토리를 등록해서
            //**생성자 주입(Dependency Injection) * *을 가능하게 합니다.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddScoped<WorkOrderListRepository>();
            builder.Services.AddScoped<WorkOrderService>();

            builder.Services.AddScoped<ILotRepository, LotsRepository>();
            builder.Services.AddScoped<ILotProcessRepository, LotProcessRepository>();
            builder.Services.AddScoped<ILotService,LotsService>();
            builder.Services.AddScoped<IEquipmentListRepository, EquipmentListRepository>();
            builder.Services.AddScoped<IEquipmentService, EquipmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();



            builder.Services.AddSingleton<JwtService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173") // Vue 앱 주소
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            // JWT 인증을 설정합니다.(미들웨어) AddJwtBearer 미들웨어
            /*
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            */



            var app = builder.Build();

            app.UseCors("AllowVueApp");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // JWT 인증 미들웨어를 사용하여 요청을 인증합니다.
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.Run();
        }
    }
}
