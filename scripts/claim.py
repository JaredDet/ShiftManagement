from common import post_json


# =========================
# HELPERS
# =========================

def get_company_admin_token(ctx):

    company_id = ctx["company1"]["id"]

    admin_id = ctx["company1"]["adminUserId"]

    return ctx["tokens"][admin_id]


def get_staff_assignee(ctx):

    if not ctx.get("staff_users"):
        raise Exception("No staff users available")

    return ctx["staff_users"][0]


# =========================
# ACTIONS
# =========================

def create_claim(
    ctx,
    token,
    collaborator_id,
    company_id
):
    return post_json(
        ctx["base_url"],
        "/api/claims",
        {
            "title": "Problema con turno",
            "description":
                "El turno no fue asignado correctamente",
            "reason": "Schedule",
            "priority": "Medium",
            "collaboratorId": collaborator_id,
            "companyId": company_id
        },
        token
    )


def assign_claim(
    ctx,
    token,
    claim_id,
    user_id
):
    return post_json(
        ctx["base_url"],
        f"/api/claims/{claim_id}/assign",
        {
            "assignedToUserId": user_id
        },
        token
    )


# =========================
# SEED
# =========================

def seed_claims(ctx):

    print("\nCreando módulo Claims...")

    if not ctx.get("collaborators"):
        raise Exception(
            "Collaborators must be seeded before claims"
        )

    if not ctx.get("tokens"):
        raise Exception(
            "Tokens must be seeded before claims"
        )

    if not ctx.get("staff_users"):
        raise Exception(
            "Staff users must be seeded before claims"
        )

    if not ctx.get("collaborator_to_user"):
        raise Exception(
            "collaborator_to_user mapping not found"
        )

    ctx["claims"] = []

    collaborators = ctx["collaborators"][:2]

    for i, collaborator in enumerate(collaborators):

        user_id = ctx["collaborator_to_user"][
            collaborator["id"]
        ]

        creator_token = ctx["tokens"][
            user_id
        ]

        claim = create_claim(
            ctx,
            creator_token,
            collaborator["id"],
            collaborator["companyId"]
        )

        if not claim or "id" not in claim:
            raise Exception(
                "Claim creation failed for "
                f"collaborator {collaborator['id']}"
            )

        ctx["claims"].append(claim)

        if i == 0:

            assignee = get_staff_assignee(ctx)

            assign_claim(
                ctx,
                get_company_admin_token(ctx),
                claim["id"],
                assignee["id"]
            )

    print(
        "Claims creados:",
        len(ctx["claims"])
    )

    return ctx