using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class BioterioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter {1} caracteres.", MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, ErrorMessage = "O telefone deve conter {1} caracteres (incluindo DDD).", MinimumLength = 11)]
        public string Telefone { get; set; }

        public EnderecoBioterioViewModel EnderecoBioterio { get; set; }
    }
}
