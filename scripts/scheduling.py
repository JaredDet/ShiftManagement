from datetime import datetime, timedelta, timezone

from common import post_json


# =========================
# HELPERS
# =========================

def get_supervisor_token(ctx, company_id):

    supervisors = ctx.get("supervisors", [])

    for s in supervisors:
        if s["companyId"] == company_id:
            user_id = s["id"]
            token = ctx["tokens"].get(user_id)

            if token:
                return token

    raise Exception(f"No supervisor found for company {company_id}")


def get_branch_manager_token(ctx, company_id):

    manager = next(
        (
            u for u in ctx["branch_managers"]
            if u["companyId"] == company_id
        ),
        None
    )

    if not manager:
        raise Exception(f"No branch manager found for company {company_id}")

    return ctx["tokens"][manager["id"]]


# =========================
# ACTIONS
# =========================

def replace_collaborator(
    ctx,
    shift_id,
    current_id,
    new_id,
    token
):
    return post_json(
        ctx["base_url"],
        "/api/shifts/replace",
        {
            "shiftId": shift_id,
            "currentCollaboratorId": current_id,
            "newCollaboratorId": new_id
        },
        token
    )


def create_swap_request(
    ctx,
    requester_id,
    target_id,
    source_shift_id,
    target_shift_id,
    token
):
    return post_json(
        ctx["base_url"],
        "/api/shift-swaps",
        {
            "requesterId": requester_id,
            "targetCollaboratorId": target_id,
            "sourceShiftId": source_shift_id,
            "targetShiftId": target_shift_id
        },
        token
    )


def accept_swap_request(ctx, request_id, token):
    return post_json(
        ctx["base_url"],
        f"/api/shift-swaps/{request_id}/accept",
        {},
        token
    )


def approve_swap_request(ctx, request_id, token):
    return post_json(
        ctx["base_url"],
        f"/api/shift-swaps/{request_id}/approve",
        {},
        token
    )


# =========================
# SEED
# =========================

def seed_scheduling(ctx):

    print("\nCreando turnos...")

    ctx["shifts"] = []
    ctx["assignments"] = []
    ctx["replacements"] = []
    ctx["swap_requests"] = []
    ctx["approved_swaps"] = []

    branches = ctx["branches"]
    collaborators = ctx["collaborators"]

    base_date = datetime.now(timezone.utc)

    # =========================
    # SHIFTS
    # =========================

    for i in range(10):

        branch = branches[i % len(branches)]

        start = (
            base_date + timedelta(days=i)
        ).replace(
            hour=9,
            minute=0,
            second=0,
            microsecond=0
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
            },
            get_supervisor_token(ctx, branch["companyId"])
        )

        ctx["shifts"].append(shift)

    print("Shifts creados:", len(ctx["shifts"]))

    # =========================
    # ASSIGNMENTS
    # =========================

    print("\nAsignando colaboradores...")

    for i, shift in enumerate(ctx["shifts"]):

        collaborator = collaborators[i % len(collaborators)]

        assignment = post_json(
            ctx["base_url"],
            "/api/shifts/assign",
            {
                "shiftId": shift["id"],
                "collaboratorId": collaborator["id"]
            },
            get_branch_manager_token(ctx, collaborator["companyId"])
        )

        ctx["assignments"].append(assignment)

    print("Assignments creados:", len(ctx["assignments"]))

    # =========================
    # DIRECT REPLACEMENTS
    # =========================

    print("\nProbando reemplazos...")

    max_replacements = min(3, len(ctx["assignments"]) - 1)

    for i in range(max_replacements):

        a = ctx["assignments"][i]
        b = ctx["assignments"][i + 1]

        current_collaborator = next(
            c for c in collaborators
            if c["id"] == a["collaboratorId"]
        )

        replacement = replace_collaborator(
            ctx,
            a["shiftId"],
            a["collaboratorId"],
            b["collaboratorId"],
            get_supervisor_token(ctx, current_collaborator["companyId"])
        )

        ctx["replacements"].append(replacement)

    print("Replacements:", len(ctx["replacements"]))

    # =========================
    # SWAP FLOW
    # =========================

    print("\nProbando swap flow...")

    if len(ctx["assignments"]) >= 5:

        a = ctx["assignments"][3]
        b = ctx["assignments"][4]

        requester_id = a["collaboratorId"]
        target_id = b["collaboratorId"]

        requester = next(
            c for c in collaborators
            if c["id"] == requester_id
        )

        requester_user_id = (
            ctx["collaborator_to_user"][requester_id]
        )

        target_user_id = (
            ctx["collaborator_to_user"][target_id]
        )

        swap_request = create_swap_request(
            ctx,
            requester_id,
            target_id,
            a["shiftId"],
            b["shiftId"],
            ctx["tokens"][requester_user_id]
        )

        ctx["swap_requests"].append(
            swap_request
        )

        accept_swap_request(
            ctx,
            swap_request["id"],
            ctx["tokens"][target_user_id]
        )

        approved = approve_swap_request(
            ctx,
            swap_request["id"],
            get_supervisor_token(ctx, requester["companyId"])
        )

        ctx["approved_swaps"].append(approved)

    print("Swap requests:", len(ctx["swap_requests"]))
    print("Approved swaps:", len(ctx["approved_swaps"]))

    return ctx