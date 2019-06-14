using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace chefsnDishes.Models
{
    public class Chef
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime BDay {get;set;}
        public int Age {get;set;}
        public List<Dish> Dishes {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}