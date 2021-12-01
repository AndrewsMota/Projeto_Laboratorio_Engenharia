using Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class AtribuirPareceristaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Id do Protocolo")]
        public Guid ProtocoloId { get; set; }
        [Display(Name = "Parecerista")]
        public string PareceristaId { get; set; }
        public IList<ApplicationUser> Pareceristas { get; set; }
    }
}
