using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Staff.Infrastructure;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class DeactivatePositionUseCase(
    PositionRepository repository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid id)
    {
        var position = await repository.GetByIdAsync(id);

        if (position is null)
            return Result.Failure(StaffErrors.PositionNotFound);

        position.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}