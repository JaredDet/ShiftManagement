from common import post_json


# =========================
# HELPERS
# =========================

def get_admin_token(ctx, company_id):

    if company_id == ctx["company1"]["id"]:
        return ctx["tokens"][ctx["company1"]["adminUserId"]]

    if company_id == ctx["company2"]["id"]:
        return ctx["tokens"][ctx["company2"]["adminUserId"]]

    raise Exception(f"Admin token not found for company: {company_id}")


def create_collaborator(
    ctx,
    user_id,
    company_id,
    branch_id,
    position_id,
    token
):
    return post_json(
        ctx["base_url"],
        "/api/collaborators",
        {
            "userId": user_id,
            "companyId": company_id,
            "mainBranchId": branch_id,
            "mainPositionId": position_id
        },
        token
    )


# =========================
# SEED STAFF
# =========================

def seed_staff(ctx):

    print("\nCreando módulo Staff...")

    ctx["collaborators"] = []

    # userId -> collaborator
    ctx["user_to_collaborator"] = {}

    # collaboratorId -> userId
    ctx["collaborator_to_user"] = {}

    # =========================
    # POSITIONS
    # =========================

    developer_position = post_json(
        ctx["base_url"],
        "/api/positions",
        {
            "companyId": ctx["company1"]["id"],
            "name": "Backend Developer",
            "description": "Backend development and maintenance"
        },
        get_admin_token(ctx, ctx["company1"]["id"])
    )

    seller_position = post_json(
        ctx["base_url"],
        "/api/positions",
        {
            "companyId": ctx["company2"]["id"],
            "name": "Sales Assistant",
            "description": "Customer service and sales"
        },
        get_admin_token(ctx, ctx["company2"]["id"])
    )

    ctx["developer_position_id"] = developer_position["id"]
    ctx["seller_position_id"] = seller_position["id"]

    # =========================
    # COLLABORATORS
    # =========================

    for user in ctx["staff_users"]:

        position_id = (
            ctx["developer_position_id"]
            if user["companyId"] == ctx["company1"]["id"]
            else ctx["seller_position_id"]
        )

        admin_token = get_admin_token(
            ctx,
            user["companyId"]
        )

        collaborator = create_collaborator(
            ctx,
            user["id"],
            user["companyId"],
            user["branchId"],
            position_id,
            admin_token
        )

        ctx["collaborators"].append(collaborator)

        ctx["user_to_collaborator"][user["id"]] = collaborator

        ctx["collaborator_to_user"][collaborator["id"]] = user["id"]

    print(
        "Collaborators creados:",
        len(ctx["collaborators"])
    )