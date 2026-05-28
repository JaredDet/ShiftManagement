using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

public sealed class CreateShiftUseCase(
    ShiftRepository shiftRepository,
    BranchRepository branchRepository,
    PositionRepository positionRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftResponse>> ExecuteAsync(CreateShiftRequest request)
    {
        var branch = await branchRepository.GetByIdAsync(request.BranchId);
        if (branch is null)
            return Result<ShiftResponse>.Failure(OrganizationErrors.BranchNotFound);

        var position = await positionRepository.GetByIdAsync(request.PositionId);

        if (position is null)
            return Result<ShiftResponse>.Failure(StaffErrors.PositionNotFound);

        var shift = Shift.Create(
            request.BranchId,
            request.PositionId,
            request.StartsAt,
            request.EndsAt
        );

        await shiftRepository.AddAsync(shift);
        await context.SaveChangesAsync();

        return Result<ShiftResponse>.Success(
            ShiftMapper.ToResponse(
                shift,
                branch.Name,
                position.Name
            )
        );
    }
}