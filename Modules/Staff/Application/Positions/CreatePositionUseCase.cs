using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Infrastructure;

namespace ShiftManagement.Api.Modules.Staff.Application.Positions;

public sealed class CreatePositionUseCase(
    PositionRepository repository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<PositionResponse>> Execute(CreatePositionRequest request)
    {
        var position = new Position(
            Guid.NewGuid(),
            request.CompanyId,
            request.Name,
            request.Description,
            DateTime.UtcNow
        );

        await repository.AddAsync(position);
        await context.SaveChangesAsync();

        return Result<PositionResponse>.Success(
            PositionMapper.ToResponse(position)
        );
    }
}