using Microsoft.EntityFrameworkCore;
using SW_MES_API.Models;

namespace SW_MES_API.Data
{
    // EF Core의 핵심 구성
    // EF Core가 실제로 DB와 연결하는 클래스
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> MESUsers { get; set; }
    }
}

