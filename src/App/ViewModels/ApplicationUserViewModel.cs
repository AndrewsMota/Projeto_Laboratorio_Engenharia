using Business.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class ApplicationUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, ErrorMessage = "O telefone deve conter {1} caracteres (incluindo DDD).", MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNasmimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A senha deve conter no mínimo {2} caracteres.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirmação de Senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public UserInfoViewModel UserInfo { get; set; }
    }
}
