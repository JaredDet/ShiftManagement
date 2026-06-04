using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Session.Api.Contracts;
using ShiftManagement.Api.Modules.Session.Infrastructure;

namespace ShiftManagement.Api.Modules.Session.Application;

public sealed class GetSessionUseCase(
    IExecutionContext context,
    SessionReadRepository sessionReadRepository
)
{
    public async Task<Result<SessionResponse>> ExecuteAsync()
    {
        var userId = context.UserId;
        var companyId = context.CompanyId;

        var session = await sessionReadRepository
            .GetContextAsync(userId, companyId);

        if (session is null)
            return Result<SessionResponse>.Failure(SessionErrors.SessionNotFound);

        return Result<SessionResponse>.Success(session);
    }
}