using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class GetPositionUseCase(
    PositionRepository repository
)
{
    public async Task<Result<PositionResponse>> Execute(Guid id)
    {
        var position = await repository.GetByIdProjectedAsync(id);

        if (position is null)
            return Result<PositionResponse>.Failure(StaffErrors.PositionNotFound);

        return Result<PositionResponse>.Success(position);
    }
}