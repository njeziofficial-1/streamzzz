using FluentValidation;
using MediatR;
using StremoCloud.Domain.Entities;
using StremoCloud.Domain.Interface;
using StremoCloud.Infrastructure.Data.UnitOfWork;
using StremoCloud.Shared;
using StremoCloud.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Application.Features.Command.Create
{
    public class CreateOrderCommandHandler(IStremoUnitOfWork unitOfWork)
        : IRequestHandler<CreateOrderCommand, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Check if the order already exists, which to me is unnecessary
            var existOrder = await unitOfWork.Repository<Order>()
                .FirstOrDefaultAsync(x => x.DatePlaced == request.DatePlaced);

            string message;
            if (existOrder != null)
            {
                return new GenericResponse<string>
                {
                    Data = null!,
                    Message = "An order with similar details already exists.",
                    IsSuccess = false
                };
            }

            // Create a new order
            var newOrder = new Order
            {
                DatePlaced = request.DatePlaced,
                Amount = request.Amount,
                IsSuccessful = request.IsSuccessful
            };

            await unitOfWork.Repository<Order>().CreateAsync(newOrder);

            message = "Order created successfully.";
            return new GenericResponse<string>
            {
                Data = message,
                Message = message,
                IsSuccess = true
            };
        }

        public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
        {
            public CreateOrderCommandValidator()
            {
                RuleFor(p => p.Amount)
                    .NotNull()
                    .NotEmpty()
                    .GreaterThan(0)
                    .WithMessage("Amount must be greater than 0.");

                RuleFor(p => p.DatePlaced)
                .NotEmpty().WithMessage("DatePlaced must not be empty.")
                .Must(date => date != default).WithMessage("DatePlaced must be a valid date.");

            }
        }

    }
}
