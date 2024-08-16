using AutoMapper;
using CatalogService.Application.Mappers;
using CatalogService.Application.Models;
using CatalogService.Application.UseCases;
using CatalogService.WebApi.Attrtibutes;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.WebApi.Dto
{
    public class UpdateBookDto : IMapWith<UpdateBookCommand>
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [FileSize(5 * 1024 * 1024, ErrorMessage = "Image size cannot exceed 5 MB.")]
        [FileType("image/jpeg,image/png", ErrorMessage = "Only JPEG and PNG images are allowed.")]
        public IFormFile Image { get; set; }
        public Guid PublisherId { get; set; }
        public ICollection<Guid> CategoryIds { get; set; }
        public ICollection<Guid> AuthorIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBookDto, UpdateBookCommand>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new FileRequest
                {
                    ContentType = src.Image.ContentType,
                    FileName = src.Image.FileName,
                    Content = ConvertToByteArray(src.Image)
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
