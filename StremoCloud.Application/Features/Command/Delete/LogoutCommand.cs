using Amazon.Runtime.Internal;
using MediatR;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Delete;

public record LogoutCommand(string userId) : IRequest<GenericResponse<string>>;

public class LogoutCommandHandler(IStremoUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, GenericResponse<string>>
{
    public async Task<GenericResponse<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var oldRefreshToken = await unitOfWork.Repository<RefreshToken>().FirstOrDefaultAsync(x => x.UserId == request.userId);
        bool isSuccess = false;
        string message = "Error in logging out";
        if (oldRefreshToken != null)
        {
            await unitOfWork.Repository<RefreshToken>().DeleteAsync(oldRefreshToken.Id);
            isSuccess = true;
            message = "Deleted successfully";
        }

        return new GenericResponse<string>
        {
            Data = message,
            Message = message,
            IsSuccess = isSuccess
        };
    }
}