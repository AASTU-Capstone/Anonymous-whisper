using AutoMapper;
using ComplaintSystem.Application.DTOs.ComplaintDto.Validators;
using ComplaintSystem.Application.Features.Complaints.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using ComplaintSystem.Domain.Entities;
using MediatR;

namespace ComplaintSystem.Application.Features.Complaints.Handlers.Commands
{
    public class CreateComplaintRequestHandler : IRequestHandler<CreateComplaintRequest, BaseResponseClass>
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;

        public CreateComplaintRequestHandler(IComplaintRepository complaintRepository, IMapper mapper)
        {
            _complaintRepository = complaintRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseClass> Handle(CreateComplaintRequest request, CancellationToken cancellationToken)
        {
            var Validator = new CreateComplaintDtoValidator();
            var validationResult = await Validator.ValidateAsync(request.CreateComplaintDto, cancellationToken);
            var response = new BaseResponseClass();

            if (!validationResult.IsValid)
            {
                response.StatusCode = 400;
                response.Success = false;
                response.Error = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var complaint = _mapper.Map<Complaint>(request.CreateComplaintDto);
                await _complaintRepository.Add(complaint);

                response.StatusCode = 201;
                response.Success = true;
                response.Message = "Complaint created successfully";

            }

            return response;
        }

    }
}