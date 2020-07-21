using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocLocadora.Models
{
    public class Filme
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "É necessário informar o nome do filme.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do filme deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "É necessário informar a classificação.")]
        [Range(1, 5, ErrorMessage = "A classificação deve ser de 1 a 5.")]
        public int Classificacao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "É necessário informar o gênero.")]
        public virtual Guid GeneroId { get; set; }

        public virtual Genero Genero { get; set; }
    }
}
