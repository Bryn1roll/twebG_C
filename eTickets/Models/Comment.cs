using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public int? NewsId { get; set; }
        [ForeignKey("NewsId")]
        public News News { get; set; }

        public string UserId { get; set; }
        // public ApplicationUser User { get; set; } // если захотите навигацию
    }
} 