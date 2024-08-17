﻿namespace CatalogService.Contracts
{
    public class BookUpdatedEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> AuthorIds { get; set; }
    }
}