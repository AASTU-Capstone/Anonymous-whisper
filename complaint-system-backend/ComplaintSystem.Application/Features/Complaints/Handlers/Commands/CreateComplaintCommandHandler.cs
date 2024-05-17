using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Persistence.Contracts.APIs;
using ComplaintSystem.Application.Persistence.Contracts.Cloudinary;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Commands
{
    public class CreateComplaintCommandHandler : IRequestHandler<CreateComplaintCommand, BaseResponseClass>
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IImaggaService _imaggaService;
        private readonly IMapper _mapper;

        public CreateComplaintCommandHandler(
            IComplaintRepository complaintRepository, 
            IMapper mapper, 
            IUserRepository userRepository,
            ICloudinaryService cloudinaryService,
            IImaggaService imaggaService)
        {
            _complaintRepository = complaintRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
            _imaggaService = imaggaService;
        }

        public async Task<BaseResponseClass> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
        {
            var Validator = new CreateComplaintDtoValidator(_imaggaService);
            var validationResult = await Validator.ValidateAsync(request.CreateComplaintDto, cancellationToken);
            var response = new BaseResponseClass();
            var user = await _userRepository.GetAsync(request.UserId);

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else if(user == null)
            {
                response.StatusCode = 404;
                response.Success = false;
                response.Error = ["user does not exist"];
                response.Message = "Create Complaint Failed";
            }
            else
            {
                List<string> imageEvidences = new List<string>();
                List<string> videos = new List<string>();
                List<string> documents = new List<string>();
                List<string> audios = new List<string>();
                List<string> tags = new List<string>();
                if (request.CreateComplaintDto.ImageEvidence!= null)
                {
                    foreach (var image in request.CreateComplaintDto.ImageEvidence)
                    {
                        CloudinaryResponse currImage = await _cloudinaryService.UploadImageAsync(image);
                        tags.AddRange(await _imaggaService.Tagger(currImage.Link));
                        imageEvidences.Add(currImage.Link);
                    }
                }

                if(request.CreateComplaintDto.Videos!= null)
                {
                    foreach (var video in request.CreateComplaintDto.Videos)
                    {
                        CloudinaryResponse currVideo = await _cloudinaryService.UploadImageAsync(video);
                        tags.AddRange(await _imaggaService.Tagger(currVideo.Link));
                        imageEvidences.Add(currVideo.Link);
                    }

                }
                
                if(request.CreateComplaintDto.Documents!= null)
                {
                    foreach (var doc in request.CreateComplaintDto.Documents)
                    {
                        CloudinaryResponse currDoc = await _cloudinaryService.UploadImageAsync(doc);
                        documents.Add(currDoc.Link);
                    }
                }
                
                if(request.CreateComplaintDto.SoundTrack!= null)
                {
                    foreach (var audio in request.CreateComplaintDto.SoundTrack)
                    {
                        CloudinaryResponse currAudio = await _cloudinaryService.UploadImageAsync(audio);
                        audios.Add(currAudio.Link);
                    }
                }
               

                CreateComplaintDto createComplaintDto = new CreateComplaintDto
                {
                    Tag = tags,
                    Title = request.CreateComplaintDto.Title,
                    SoundTracks = audios,
                    Status = "recieved",
                    Category = request.CreateComplaintDto.Category,
                    Content = request.CreateComplaintDto.Content,
                    Documents = documents,
                    ImageEvidences = imageEvidences,
                    UserEntityId = request.UserId,
                };

                var complaint = _mapper.Map<Complaint>(createComplaintDto);
                complaint.UserEntityId = request.UserId;

                await _complaintRepository.Add(complaint);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Complaint created successfully";

            }

            return response;
        }

    }
}