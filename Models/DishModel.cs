using System.ComponentModel.DataAnnotations;
using System;

namespace chefsnDishes.Models
{
    public class Dish
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public int Tastiness {get;set;}
        public int Calories {get;set;}
        public int ChefId {get;set;}
        public Chef DishChef {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}