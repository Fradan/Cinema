using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    [Table("Film", Schema = "Directory")]
    public class Film
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
