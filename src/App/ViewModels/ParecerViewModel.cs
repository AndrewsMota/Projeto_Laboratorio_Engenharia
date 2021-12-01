using Business.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class ParecerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Id do Protocolo")]
        public Guid ProtocoloId { get; set; }

        [Display(Name = "Justificativa do Parecer")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string JustificativaDoParecer { get; set; }

        [Display(Name = "Parecer")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Escolha Escolha { get; set; }
    }
}
