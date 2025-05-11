using System;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
    }
} 