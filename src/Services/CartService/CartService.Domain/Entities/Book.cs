﻿namespace CartService.Domain.Entities
{
        public class Book
        {
            public Guid Id { get; set; }
            public string? Title { get; set; }
            public string? Image { get; set; }
        }
}
