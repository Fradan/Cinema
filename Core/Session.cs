using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    [Table("Session", Schema = "dbo")]
    public class Session
    {
        public int Id { get; set; }
        
        public int FilmId { get; set; }
        
        public int CinemaId { get; set; }
        
        public DateTime SessionTime { get; set; }
    }
}
