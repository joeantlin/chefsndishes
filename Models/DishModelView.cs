using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using chefsnDishes.Models;
using System;

namespace chefsnDishes.Models
{
    public class DishView
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public int Tastiness {get;set;}
        public int Calories {get;set;}
        public int ChefId {get;set;}
        public List<Chef> ListofChefs {get;set;}
    }
}