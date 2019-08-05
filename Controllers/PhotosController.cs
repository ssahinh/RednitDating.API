using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RednitDating.Api.Data;
using RednitDating.Api.DTOs;
using RednitDating.Api.Helpers;
using RednitDating.Api.Models;

namespace RednitDating.Api.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDatingRepository _repo;
        private readonly IOptions<CloudinarySettings> _cloudinarySettings;
        
        private Cloudinary _cloudinary;
        public PhotosController(IDatingRepository repo, IMapper mapper, 
            IOptions<CloudinarySettings> cloudinarySettings)
        {
            _mapper = mapper;
            _repo = repo;
            _cloudinarySettings = cloudinarySettings;

            Account acc = new Account(
                _cloudinarySettings.Value.CloudName,
                _cloudinarySettings.Value.ApiKey,
                _cloudinarySettings.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }


        // CM 08/02/2019
        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var userFromRepo = await _repo.GetUser(userId);

            var file =  photoForCreationDto.File;
            
            // image result from cloudinary
            var uploadResult = new ImageUploadResult();
            // file length validation
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            // if user doesn't have a main photo set this photo to the main.
            if (!userFromRepo.Photos.Any(u => u.IsProfile))
                photo.IsProfile = true;

            userFromRepo.Photos.Add(photo);
            
            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn );
            }

            return BadRequest("Failed to add photo");
        }
    }


}