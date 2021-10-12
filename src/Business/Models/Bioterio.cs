using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Bioterio : Entidade
    {
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }


        /*EF Relations*/
        public EnderecoBioterio EnderecoBioterio { get; set; }
    }
}
