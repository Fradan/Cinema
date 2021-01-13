using Newtonsoft.Json;
using System;

namespace CinemaWeb
{
    public class SessionDto
    {
        [JsonProperty("sessiontime")]
        public DateTime SessionTime { get; set; }

        [JsonProperty("filmid")]
        public int FilmId { get; set; }

        [JsonProperty("cinemaid")]
        public int CinemaId { get; set; }
    }
}
