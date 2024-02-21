using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Context;
using ToDoList.Server.Models;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[EnableCors("AllowCors")] //CORS
    public class TareasController : Controller
    {

        private readonly DbContextTareas _context;

        public TareasController(DbContextTareas context)
        {
            _context = context;
        }

        [HttpGet("Listado")]
        public async Task<List<Tarea>> Index()
        {
            var list = await _context.Tareas.ToListAsync();

            if (list == null)
            {
                //Se envia la lista vacia
                return new List<Tarea>();
            }
            else
            {
                //se envia la lista con los datos
                return list;
            }
        }

        [HttpGet("Buscar")]
        public async Task<Tarea> GetQueja(int numero)
        {
            //Se busca la queja especifica 
            var temp = await _context.Tareas.FirstOrDefaultAsync(x => x.ID == numero);

            //Se retorna la queja
            return temp;
        }

        [HttpPost("Agregar")]
        public string Agregar(Tarea tarea)
        {
            //variable para el mensaje de accion
            string mensaje = "";
            try
            {
                //Se agrega la Tarea al contexto
                _context.Tareas.Add(tarea);
                //se aplican cambios
                _context.SaveChanges();
                //se indica mensaje de exito
                mensaje = " Queja guardada correctamente";

            }
            catch (Exception ex)
            {
                //se indica mensaje de error
                mensaje = "Error" + ex.Message;
            }

            return mensaje; //Se retorna mensaje
        }

        [HttpDelete("Eliminar")]
        public async Task<string> Eliminar(int numero)
        {
            string mensaje = "No se eliminó la tarea";

            try
            {
                //Se utiliza el ORM para buscar la tarea
                var temp = await _context.Tareas.FirstOrDefaultAsync(f => f.ID == numero);

                if (temp != null)//Se verifica si hay datos
                {
                    //se elimina la tarea
                    _context.Tareas.Remove(temp);
                    //Se guardan los cambios
                    _context.SaveChangesAsync();

                    //retorna el mensaje de exito
                    mensaje = "Tarea " + temp.Descripcion + " eliminada correctamente";
                }
            }
            catch (Exception ex)
            {
                //en caso de error se almacena el texto de error
                mensaje += ex.Message;
            }
            return mensaje;//Se retorna el mensaje
        }

        [HttpPut("Modificar")]
        public string Modificar(Tarea ptarea)
        {
            //variable control de mensajes
            string mensaje = "No se logró aplicar los cambios";

            try
            {
                //Se aplican los cambios
                _context.Tareas.Update(ptarea);
                //Se aplican los cambios en la DB
                _context.SaveChanges();

                mensaje = "Cambios aplicados correctamente";
            }
            catch (Exception ex)
            {
                //en caso de error se almacena el texto
                mensaje = "Error => " + ex.Message;
            }
            return mensaje;
        }


    }
}
