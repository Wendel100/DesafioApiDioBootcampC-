using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
             var tarefa = _context.Tarefas.Find(id);
            if(tarefa == null)
            return NotFound();
            
            return Ok(tarefa);
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos(Tarefa tarefa)
        {
            var tarefas = _context.Tarefas.Find(tarefa);
            if(tarefas ==null)
            return NotFound();

            return Ok(tarefas);
            // TODO: Buscar todas as tarefas no banco utilizando o EF
         
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
               var contato = _context.Tarefas.Where(x =>x.Titulo.Contains(titulo));
                return Ok(contato);
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData   
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
            return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            _context.Add(tarefa);
            _context.SaveChanges();
            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
                
             tarefaBanco.Titulo = tarefa.Titulo;
             tarefaBanco.Descricao = tarefa.Descricao;
             tarefaBanco.Status = tarefa.Status;
              tarefaBanco.Data = tarefa.Data;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

        // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
        // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
            var tarefa = _context.Tarefas.Find(id);
             if(tarefa == null)
             return NotFound();
             
            _context.Tarefas.Remove(tarefaBanco);
             _context.SaveChanges();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            return NoContent();
        }
    }
}
