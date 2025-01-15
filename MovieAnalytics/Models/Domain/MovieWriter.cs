﻿namespace MovieAnalytics.Models.Domain
{
    public class MovieWriter
    {
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int WriterId { get; set; }
        public Writer Writer { get; set; }
    }
}
