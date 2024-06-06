using ComplaintSystem.Application.Features.Subordinates.Requests.Commands;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Application.Responses;
using MediatR;

namespace ComplaintSystem.Application.Features.Subordinates.Handlers.Commands
{
    public class DeleteSubordinateCommandHandler : IRequestHandler<DeleteSubordinateCommand, BaseResponseClass>
    {
        private readonly ISubordinateRepository _subordinateRepository;
        private readonly IComplaintLogRepository _complaintLogRepository;
        private readonly IUserRepository _userRepository;

        public DeleteSubordinateCommandHandler(
            ISubordinateRepository subordinateRepository,
            IComplaintLogRepository complaintLogRepository,
            IUserRepository userRepository)
        {
            _subordinateRepository = subordinateRepository;
            _complaintLogRepository = complaintLogRepository;
            _userRepository = userRepository;
        }


        public async Task<BaseResponseClass> Handle(DeleteSubordinateCommand request, CancellationToken cancellationToken)
        {
            var subordinate = await _subordinateRepository.GetAsync(request.DeleteSubordinateDto.Id);
            var response = new BaseResponseClass();

            if (subordinate == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Subordinate not found";
            }

            else
            {
                var complaintLogs = await _complaintLogRepository.GetComplaintLogsBySubordinateId(request.DeleteSubordinateDto.Id);
                var user = await _userRepository.GetAsync(subordinate.UserEntityId);
                for (int i = 0; i < complaintLogs.Count; i++)
                {
                    complaintLogs[i].SubordinateId = new Guid("00000000-0000-0000-0000-000000000000");
                    complaintLogs[i].Status = "progressing";
                    complaintLogs[i].Report = null;
                    await _complaintLogRepository.Update(complaintLogs[i]);
                }

                user.User_Type = "User";
                await _userRepository.Update(user);
                await _subordinateRepository.Delete(subordinate);

                response.Success = true;
                response.StatusCode = 204;
                response.Message = "Subordinate deleted successfully";
            }

            return response;
        }
    }
}