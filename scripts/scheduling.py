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


def create_swap_request(
    ctx,
    requester_id,
    target_collaborator_id,
    source_shift_id,
    target_shift_id
):

    return post_json(
        ctx["base_url"],
        "/api/shift-swaps",
        {
            "requesterId": requester_id,
            "targetCollaboratorId": target_collaborator_id,
            "sourceShiftId": source_shift_id,
            "targetShiftId": target_shift_id
        }
    )


def accept_swap_request(
    ctx,
    request_id
):

    return post_json(
        ctx["base_url"],
        f"/api/shift-swaps/{request_id}/accept",
        {}
    )


def approve_swap_request(
    ctx,
    request_id
):

    return post_json(
        ctx["base_url"],
        f"/api/shift-swaps/{request_id}/approve",
        {}
    )


def seed_scheduling(ctx):

    print("\nCreando turnos...")

    branches = ctx["branches"]
    employees = ctx["employees"]

    # =========================
    # SHIFTS
    # =========================
    for i in range(10):

        branch = branches[i % len(branches)]

        start = (
            datetime.now(timezone.utc)
            .replace(
                hour=9,
                minute=0,
                second=0,
                microsecond=0
            )
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

    # =========================
    # ASSIGNMENTS
    # =========================
    print("\nAsignando colaboradores...")

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

    # =========================
    # DIRECT REPLACEMENTS
    # =========================
    print("\nProbando reemplazos directos...")

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

    # =========================
    # SWAP REQUESTS
    # =========================
    print("\nProbando solicitudes de intercambio...")

    a = ctx["assignments"][3]
    b = ctx["assignments"][4]

    swap_request = create_swap_request(
        ctx,
        a["collaboratorId"],
        b["collaboratorId"],
        a["shiftId"],
        b["shiftId"]
    )

    ctx["swap_requests"].append(swap_request)

    print("Swap request creada:", swap_request["id"])

    accepted = accept_swap_request(
        ctx,
        swap_request["id"]
    )

    print("Swap request aceptada")

    approved = approve_swap_request(
        ctx,
        swap_request["id"]
    )

    print("Swap request aprobada")

    ctx["approved_swaps"].append(approved)

    return ctx