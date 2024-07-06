using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
    //Entity Framework Code First
    //Quais classes vão representar as tabelas no banco
    public DbSet<Produto> Produtos { get; set; }
    // public DbSet<Categoria> Categorias { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Configuração da String de Conexão
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
