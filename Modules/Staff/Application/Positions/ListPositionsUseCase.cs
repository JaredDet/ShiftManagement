using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class ListPositionsUseCase(
    PositionRepository repository
)
{
    public async Task<Result<List<PositionResponse>>> Execute()
    {
        var positions = await repository.ListProjectedAsync();

        return Result<List<PositionResponse>>
        .Success(positions);
    }
}