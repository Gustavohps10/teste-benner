using System.ComponentModel.DataAnnotations;

namespace microwave_benner.Application.DTOs
{
    public class HatingTaskDTO
    {
        [Required]
        public int id { get; set; }

        [Required]
        [Range(1, 120, ErrorMessage = "O campo 'time' deve estar entre 1 e 120.")]
        public int time { get; set; } 

        [Required]
        [Range(1, 10, ErrorMessage = "O campo 'power' deve estar entre 1 e 10.")]
        public int power { get; set; }

        [Required]
        public DateTime startTime { get; set; }

        public DateTime? pauseTime { get; set; }

        public DateTime? endTime { get; set; } 
    }
}
