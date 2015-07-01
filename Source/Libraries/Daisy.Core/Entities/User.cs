using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
