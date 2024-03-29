using System;

namespace RednitDating.Api.DTOs
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public bool IsProfile { get; set; }
 
        public string PublicId { get; set; }
    
    }
}