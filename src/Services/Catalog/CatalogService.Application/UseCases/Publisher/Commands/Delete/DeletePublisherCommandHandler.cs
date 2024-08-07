using CatalogService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.UseCases
{
    public class DeletePublisherCommandHandler(
        IPublisherRepository publisherRepository) : IRequestHandler<DeletePublisherCommand>
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;

        public async Task Handle(DeletePublisherCommand request, CancellationToken token)
        {
            await _publisherRepository.DeleteAsync(request.Id, token);
        }
    }
}
