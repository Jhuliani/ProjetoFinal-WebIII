using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.Core.Models
{
    public class CityEvent
    {
        
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(100)]
        public string? Title { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Data e Hora são obrigatórias")]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Local é obrigatório")]
        public string? Local { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public bool Status { get; set; }
    }
}
