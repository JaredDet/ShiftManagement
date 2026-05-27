using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed record AddCommentToClaimRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(2000)]
    public string Content { get; init; } = string.Empty;
}