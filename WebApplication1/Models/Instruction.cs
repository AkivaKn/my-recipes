using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Dish Dish { get; set; }
    }
}
