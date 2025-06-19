using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class AddCommentVM
    {
        [Required]
        public string Content { get; set; }
        public int NewsId { get; set; }
        public int? MovieId { get; set; }
    }
} 