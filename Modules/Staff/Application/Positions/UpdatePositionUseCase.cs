using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class UpdatePositionUseCase(
    PositionRepository repository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<PositionResponse>> Execute(
        Guid id,
        UpdatePositionRequest request
    )
    {
        var position = await repository.GetByIdAsync(id);

        if (position is null)
        {
            return Result<PositionResponse>.Failure(
                StaffErrors.PositionNotFound
            );
        }

        var normalizedName = request.Name.Trim().ToLowerInvariant();

        var exists = await repository.ExistsByNameAsync(
            position.CompanyId,
            normalizedName
        );

        if (
            exists &&
            !position.Name.Equals(
                request.Name,
                StringComparison.OrdinalIgnoreCase
            )
        )
        {
            return Result<PositionResponse>.Failure(
                StaffErrors.PositionAlreadyExists
            );
        }

        position.Update(
            request.Name,
            request.Description
        );

        await context.SaveChangesAsync();

        return Result<PositionResponse>.Success(
            PositionMapper.ToResponse(position)
        );
    }
}