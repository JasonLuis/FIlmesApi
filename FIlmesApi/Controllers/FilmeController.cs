using FIlmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIlmesApi.Controllers;

/*
 Para que essa classe seja um controlador e esteja hábil a lidar com requisições de usuários,
 precisamos adicionar alguns elementos. A primeira delas são as anotações
 [ApiController] e [Route],
 antes da definição da classe:
 */
[ApiController] 
[Route("[controller]")]
public class FilmeController: ControllerBase
{

    private static List<Filme> filmes = new List<Filme>();
    private static int id = 0;

    [HttpPost] // Requisição POST
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme); // retorna o objeto cadastrado na lista
        // A funcção acima basicamente, chama a função/endpoint RecuperarFilmePorId
    }


    /*
         A principal finalidade do IEnumerable é fornecer uma maneira padrão de iterar
         sobre os elementos de uma coleção sem expor os detalhes internos da 
         implementação da coleção. Ele define um método chamado GetEnumerator(),
         que retorna um objeto que implementa a interface IEnumerator.
         A interface IEnumerator possui métodos como MoveNext() para avançar
         para o próximo elemento e Current para obter o elemento atual da iteração.
     */

    [HttpGet] // Requisição GET
    public IEnumerable<Filme> RecuperaFilme([FromQuery]int skip = 0, [FromQuery] int take = 10)
    {
        /*  Método de Paginação */
        // FromQuery = RequestParam
        return filmes.Skip(skip).Take(take);
        // Skip = pula a quantidade informada na lista
        // Take = A quantidade que sera mostrada apos o skip
    }

    [HttpGet("{id}")] // Requisição GET que recebe uma queryParam
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();

        return Ok(filme);
    }
}
