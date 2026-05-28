using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class CreatePositionUseCase(
    PositionRepository repository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<PositionResponse>> Execute(CreatePositionRequest request)
    {
        var exists = await repository.ExistsByNameAsync(
            request.CompanyId,
            request.Name.Trim().ToLowerInvariant()
        );

        if (exists)
            return Result<PositionResponse>.Failure(
                StaffErrors.PositionAlreadyExists
            );

        var position = Position.Create(
            request.CompanyId,
            request.Name,
            request.Description
        );

        await repository.AddAsync(position);
        await context.SaveChangesAsync();

        return Result<PositionResponse>.Success(
            PositionMapper.ToResponse(position)
        );
    }
}