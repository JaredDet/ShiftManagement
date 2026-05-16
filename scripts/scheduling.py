from datetime import datetime, timedelta, timezone

from common import post_json


def replace_collaborator(
    ctx,
    shift_id,
    current_id,
    new_id
):

    return post_json(
        ctx["base_url"],
        "/api/shifts/replace",
        {
            "shiftId": shift_id,
            "currentCollaboratorId": current_id,
            "newCollaboratorId": new_id
        }
    )


def seed_scheduling(ctx):

    print("\nCreando turnos...")

    branches = ctx["branches"]
    employees = ctx["employees"]

    for i in range(10):

        branch = branches[i % len(branches)]

        start = (
            datetime.now(timezone.utc)
            .replace(hour=9, minute=0, second=0, microsecond=0)
            + timedelta(days=i)
        )

        end = start.replace(hour=18)

        shift = post_json(
            ctx["base_url"],
            "/api/shifts",
            {
                "branchId": branch["id"],
                "positionId": ctx["developer_position_id"],
                "startsAt": start.isoformat(),
                "endsAt": end.isoformat()
            }
        )

        ctx["shifts"].append(shift)

    print("Shifts creados:", len(ctx["shifts"]))

    for i, shift in enumerate(ctx["shifts"]):

        employee = employees[i % len(employees)]

        assignment = post_json(
            ctx["base_url"],
            "/api/shifts/assign",
            {
                "shiftId": shift["id"],
                "collaboratorId": employee["id"]
            }
        )

        ctx["assignments"].append(assignment)

    print("Assignments:", len(ctx["assignments"]))

    for i in range(3):

        a = ctx["assignments"][i]
        b = ctx["assignments"][i + 1]

        replacement = replace_collaborator(
            ctx,
            a["shiftId"],
            a["collaboratorId"],
            b["collaboratorId"]
        )

        ctx["replacements"].append(replacement)

    print("Replacements:", len(ctx["replacements"]))