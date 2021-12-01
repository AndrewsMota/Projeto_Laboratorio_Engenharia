using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class ProtocoloViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Justificativa { get; set; }

        [Display(Name = "Resumo em Português")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string ResumoPt { get; set; }

        [Display(Name = "Resumo em Inglês")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string ResumoEn { get; set; }

        [Display(Name = "Data Prevista Para Início")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data Prevista Para Término")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataTermino { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public StatusProtocolo Status { get; set; }

        public IList<int> Quantidades { get; set; }

        public IList<EspecieViewModel> Especies { get; set; }

        public ApplicationUserViewModel ApplicationUserViewModel { get; set; }
    }
}
