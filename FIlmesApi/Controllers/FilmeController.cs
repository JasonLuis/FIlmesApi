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
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
        Console.WriteLine(filme.Titulo);
        Console.WriteLine(filme.Duracao);
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
    public IEnumerable<Filme> RecuperaFilme()
    {
        return filmes;
    }

    [HttpGet("{id}")] // Requisição GET que recebe uma queryParam
    public Filme? RecuperaFilmePorId(int id)
    {
        return filmes.FirstOrDefault(filme => filme.Id == id);
    }
}
