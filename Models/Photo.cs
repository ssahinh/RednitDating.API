using System;

namespace RednitDating.Api.Models
{
    public class Photo
    {

        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsProfile { get; set; }

        public string PublicId { get; set; }
        public User User { get; set; }

        public int UserId { get; set; }

        
    }
}