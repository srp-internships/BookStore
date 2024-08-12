using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Application.Models;
using CatalogService.Application.UseCases;

namespace CatalogService.WebApi.Dto
{
    public class UpdatePublisherDto : IMapWith<UpdatePublisherCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IFormFile Logo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePublisherDto, UpdatePublisherCommand>()
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => new FileRequest
                {
                    ContentType = src.Logo.ContentType,
                    FileName = src.Logo.FileName,
                    Content = ConvertToByteArray(src.Logo)
                }));
        }

        private byte[] ConvertToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
