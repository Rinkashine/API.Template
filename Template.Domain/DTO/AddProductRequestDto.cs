using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.DTO
{
    public class AddProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Precision(18, 2)]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
