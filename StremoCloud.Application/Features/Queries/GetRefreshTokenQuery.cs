using Amazon.Runtime.Internal;
using MediatR;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Application.Features.Queries;

public record GetRefreshTokenQuery(string Token) : IRequest<GenericResponse<RefreshToken>>;

public class GetRefreshTokenQueryHandler(IStremoUnitOfWork unitOfWork) : IRequestHandler<GetRefreshTokenQuery, GenericResponse<RefreshToken>>
{
    public async Task<GenericResponse<RefreshToken>> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var refreshTokenRecord = await unitOfWork.Repository<RefreshToken>().FirstOrDefaultAsync(x => x.RefreshTokenValue.ToLower() == request.Token.ToLower());
        if (refreshTokenRecord == null)
        {
            return new GenericResponse<RefreshToken>
            {
                IsSuccess = false,
                Message = "No record for this token request"
            };
        }

        bool isExpired = refreshTokenRecord.DateExpires >= DateTime.Now;
        if (isExpired)
        {
            return new GenericResponse<RefreshToken>
            {
                IsSuccess = false,
                Message = "Token is expired"
            };
        }

        return new GenericResponse<RefreshToken>
        {
            IsSuccess = true,
            Data = refreshTokenRecord,
            Message = "Token record returned successfully"
        };
    }
}
