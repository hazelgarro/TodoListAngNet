using System.ComponentModel.DataAnnotations;

namespace ToDoList.Server.Models
{
    public class Tarea
    {
        [Key]
        public  int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }

        
        [Required]
        public char Estado { get; set; }

    }
}
