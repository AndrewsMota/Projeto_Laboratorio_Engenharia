using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserInfo : Entidade
    {
        public string UserId { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public Sexo Sexo { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        /*EF Relations*/
        public ApplicationUser ApplicationUser { get; set; }
    }
}
