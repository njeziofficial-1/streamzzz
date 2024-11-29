using MediatR;
using StremoCloud.Domain.Entities;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared.Response;

namespace StremoCloud.Application.Features.Queries;

public class GetUserQuery : IRequest<GenericResponse<List<UserResponse>>>
{
}
public class GetUserQueryHandler(IStremoUnitOfWork unitOfWork) : IRequestHandler<GetUserQuery, GenericResponse<List<UserResponse>>>
{
    public async Task<GenericResponse<List<UserResponse>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userCollectionResponse = unitOfWork.Repository<SignUp>().GetList()
            .Select(x => new UserResponse
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address
            }).ToList();

        bool isSuccess = userCollectionResponse.Any();
        return new GenericResponse<List<UserResponse>>
        {
            Data = userCollectionResponse,
            IsSuccess = isSuccess,
            Message = isSuccess ? "Users returned successfully" : "No response for this request at this time. Please try again later"
        };
    }
}
