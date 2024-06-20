using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyRecipes.Models
{
    public class Instruction
    {
        [Key]
        public int Id { get; set; }
        public int InstructionNumber { get; set; }
        public string InstructionBody { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        [JsonIgnore]

        public Dish Dish { get; set; }
    }
}
