using System.ComponentModel.DataAnnotations;

namespace FIlmesApi.Models;

/*
Validação de dados: data annotations

Required: torna o dado obrigatório
MaxLength: valida a quantidade máxima de caracteres
Range:  limita o intervalo inferior e superior para valores numéricos
ErrorMessage: Personaliza a mensagem de erro

StringLength:  limita o tamanho de caracteres de uma string
 */

public class Filme
{

    public int Id { get; set; }

    [Required(ErrorMessage = "O título do filme é obrigatório")]
    [MaxLength(200, ErrorMessage = "O título do filme não pode exceder 200 caracteres")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [MaxLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A duração do filme é obrigatório")]
    [Range(70, 600, ErrorMessage = "A duração deve ser entre 70 e 600 minutos")]
    public int Duracao { get; set; }
}
