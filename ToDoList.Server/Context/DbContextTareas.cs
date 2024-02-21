using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Models;

namespace ToDoList.Server.Context
{
    public class DbContextTareas : DbContext
    {

        public DbContextTareas(DbContextOptions<DbContextTareas> options) : base(options) 
        {
            
        }

        public DbSet<Tarea> Tareas { get; set; }

    }
}
