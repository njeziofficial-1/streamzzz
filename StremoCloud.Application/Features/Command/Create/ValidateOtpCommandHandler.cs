using MediatR;
using StremoCloud.Domain.Interface;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public class ValidateOtpCommandHandler(IOtpService otpService) : IRequestHandler<ValidateOtpCommand, GenericResponse<bool>>
{
    public async Task<GenericResponse<bool>> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
    {
        var validateTupleResponse = await Task.FromResult(otpService.ValidateOtp(request.Email, request.Otp));
        bool isSuccess = validateTupleResponse.Item1;
        return new GenericResponse<bool>
        {
            Data = isSuccess,
            IsSuccess = isSuccess,
            Message = validateTupleResponse.Item2
        };
    }
}
