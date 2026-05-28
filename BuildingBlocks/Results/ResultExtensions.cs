using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.BuildingBlocks.Results;

public static class ResultExtensions
{
    public static IActionResult Match<T>(
        this Result<T> result,
        Func<T, IActionResult> onSuccess)
    {
        if (!result.IsSuccess)
        {
            return result.Error!.ToActionResult();
        }

        return onSuccess(result.Value!);
    }

    public static IActionResult Match(
        this Result result,
        Func<IActionResult> onSuccess)
    {
        if (!result.IsSuccess)
        {
            return result.Error!.ToActionResult();
        }

        return onSuccess();
    }
}