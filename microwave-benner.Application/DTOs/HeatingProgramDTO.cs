using System.ComponentModel.DataAnnotations;

namespace microwave_benner.Application.DTOs
{
    public class HeatingProgramDTO
    {
        public int? id { get; set; }

        [MaxLength(100, ErrorMessage = "O campo 'name' deve ter no máximo 100 caracteres.")]
        public string? name { get; set; }

        [MaxLength(100, ErrorMessage = "O campo 'food' deve ter no máximo 100 caracteres.")]
        public string? food { get; set; }

        [Range(1, 120, ErrorMessage = "O campo 'time' deve estar entre 1 e 120 segundos.")]
        public int? time { get; set; }

        [Range(1, 10, ErrorMessage = "O campo 'power' deve estar entre 1 e 10.")]
        public int? power { get; set; }

        [MaxLength(500, ErrorMessage = "O campo 'instructions' deve ter no máximo 500 caracteres.")]
        public string? instructions { get; set; }

        [MaxLength(1, ErrorMessage = "O campo 'heatingChar' deve ter exatamente 1 caractere.")]
        public string? heatingChar { get; set; }
    }
}
