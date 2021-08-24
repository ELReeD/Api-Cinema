using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Model
{
    public class Comment
    {
        public Comment(string UserId,string FilmId ,string Title, string Text)
        {
            this.UserId = UserId;
            this.FilmId = FilmId;
            this.DateTime = DateTime.Now;
            this.Title = Title;
            this.Text = Text;
        }
        public Comment()
        {

        }
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }        
        [Required]
        public string FilmId { get; set; }       
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }
    }
}
