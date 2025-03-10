using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.DTO
{
    public class UpdateCategoryRequestDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
