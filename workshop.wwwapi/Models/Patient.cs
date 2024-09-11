using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace workshop.wwwapi.Models
{
    //TODO: decorate class/columns accordingly    

    [Table("patients")]
    public class Patient
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column ("full_name")]
        [Required]
        public string FullName { get; set; }
        [Column ("appointments")]
        public List<Appointment> Appointments { get; set; }

    }
}
