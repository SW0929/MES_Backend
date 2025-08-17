
using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.Repositories.EmployeeRepository;
using SW_MES_API.Repositories.EquipmentRepository;
using SW_MES_API.Repositories.LoginRepository;
using SW_MES_API.Repositories.LotProcessRepository;
using SW_MES_API.Repositories.LotRepository;
using SW_MES_API.Repositories.WorkOrderRepository;
using SW_MES_API.Services.Admin.LotService;
using SW_MES_API.Services.Admin.WorkOrderService;
using SW_MES_API.Services.Common.EmployeeService;
using SW_MES_API.Services.Common.EquipmentService;
using SW_MES_API.Services.Login;
using SW_MES_API.Services.Operator;

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

            //ASP.NET Core�� ���񽺿� �������丮�� ����ؼ�
            //**������ ����(Dependency Injection) * *�� �����ϰ� �մϴ�.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddScoped<IWorkOrderRepository,WorkOrderRepository>();
            builder.Services.AddScoped<IWorkOrderService, WorkOrderService>();

            builder.Services.AddScoped<ILotRepository, LotsRepository>();
            builder.Services.AddScoped<ILotProcessRepository, LotProcessRepository>();
            builder.Services.AddScoped<ILotService,LotsService>();

            builder.Services.AddScoped<IEquipmentService, EquipmentService>();
            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            
            builder.Services.AddScoped<IAssignedLotsListService, AssignedLotsListService>();
            
            builder.Services.AddScoped<ILotProcessRepository, LotProcessRepository>();
            builder.Services.AddScoped<IWorkStartService, WorkStartService>();


            builder.Services.AddSingleton<JwtService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173") // Vue �� �ּ�
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            // JWT ������ �����մϴ�.(�̵����) AddJwtBearer �̵����
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

            // JWT ���� �̵��� ����Ͽ� ��û�� �����մϴ�.
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.Run();
        }
    }
}
