using MediatR;
using StremoCloud.Domain.Interface;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Command.Create;

public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, GenericResponse<bool>>
{
    private readonly IOtpService _otpService;

    public ValidateOtpCommandHandler(IOtpService otpService)
    {
        _otpService = otpService;
    }

    public async Task<GenericResponse<bool>> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
    {
        var validateTupleResponse = await Task.FromResult(_otpService.ValidateOtp(request.Email, request.Otp));
        bool isSuccess = validateTupleResponse.Item1;
        return new GenericResponse<bool>
        {
            Data = isSuccess,
            IsSuccess = isSuccess,
            Message = validateTupleResponse.Item2
        };
    }
}
