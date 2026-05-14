using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class ListPositionsUseCase(
    PositionRepository repository
)
{
    public async Task<Result<PositionListResponse>> Execute()
    {
        var positions = await repository.ListProjectedAsync();

        return Result<PositionListResponse>.Success(
            new PositionListResponse
            {
                Positions = positions
            }
        );
    }
}