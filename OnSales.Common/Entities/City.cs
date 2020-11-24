using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnSales.Common.Entities
{
    public class City : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50,ErrorMessage="The field{0} must containt less then {1} characters.")]
        [Required]
        public string Name { get; set; }

        [NotMapped]
        [JsonIgnore]
        public int IdDepartment { get; set; }

    }
}
