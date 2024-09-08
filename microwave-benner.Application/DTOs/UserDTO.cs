using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Application.DTOs
{
    internal class UserDTO
    {
        public int? id { get; set; }

        [MaxLength(100, ErrorMessage = "O campo 'username' deve ter no máximo 20 caracteres.")]
        public string? username { get; set; }

        [MinLength(6, ErrorMessage = "O campo 'password' deve ter no mínimo 6 caracteres.")]
        public string? password { get; set; }
    }
}
