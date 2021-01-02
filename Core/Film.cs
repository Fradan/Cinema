using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core
{
    [Table("Film", Schema = "Directory")]
    public class Film
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
