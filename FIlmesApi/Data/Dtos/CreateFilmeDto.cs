﻿using System.ComponentModel.DataAnnotations;

namespace FIlmesApi.Data.Dtos
{
    public class CreateFilmeDto
    {

        [Required(ErrorMessage = "O título do filme é obrigatório")]
        [StringLength(200, ErrorMessage = "O título do filme não pode exceder 200 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "A duração do filme é obrigatório")]
        [Range(70, 600, ErrorMessage = "A duração deve ser entre 70 e 600 minutos")]
        public int Duracao { get; set; }
    }
}
