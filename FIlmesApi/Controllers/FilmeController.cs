using AutoMapper;
using FIlmesApi.Data;
using FIlmesApi.Data.Dtos;
using FIlmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
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
    private FilmeContext _context;
    private IMapper _mapper;
    
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost] // Requisição POST
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme </param>
    /// <returns>IActionResul</returns>
    /// <response code="201">Caso inserção seja feita com sucesso </response>
    [HttpGet] // Requisição GET
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IEnumerable<ReadFilmeDto> RecuperaFilme(
        [FromQuery]int skip = 0,
        [FromQuery] int take = 10,
        [FromQuery] string? nomeCinema = null)
    {
        

        if(nomeCinema == null)
        {
            /*  Método de Paginação */
            // FromQuery = RequestParam
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
            // return _context.Filmes.Skip(skip).Take(take);
            // Skip = pula a quantidade informada na lista
            // Take = A quantidade que sera mostrada apos o skip
        }

        return _mapper
            .Map<List<ReadFilmeDto>>
            (_context
            .Filmes
            .Skip(skip)
            .Take(take)
            .Where(filme =>
                filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
    }

    [HttpGet("{id}")] // Requisição GET que recebe uma queryParam
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id,
        [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, 
        JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        
        if (filme == null) return NotFound();
        
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if(!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
