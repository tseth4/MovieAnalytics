﻿namespace MovieAnalytics.Models.Domain
{
    public class Director
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; } = [];
    }
}
