from common import post_json


def seed_organization(ctx):

    print("\nCreando sucursales...")

    ctx.setdefault("branches", [])

    branches = [
        {
            "companyId": ctx["company1"]["id"],
            "name": "Sucursal Centro",
            "address": "Av. Principal 123"
        },
        {
            "companyId": ctx["company1"]["id"],
            "name": "Sucursal Norte",
            "address": "Calle Norte 456"
        },
        {
            "companyId": ctx["company2"]["id"],
            "name": "Sucursal Mall",
            "address": "Mall Plaza 789"
        }
    ]

    for branch_body in branches:

        company_id = branch_body["companyId"]

        admin_user_id = (
            ctx["company1"]["adminUserId"]
            if company_id == ctx["company1"]["id"]
            else ctx["company2"]["adminUserId"]
        )

        admin_token = ctx["tokens"][admin_user_id]

        branch = post_json(
            ctx["base_url"],
            "/api/branches",
            branch_body,
            admin_token
        )

        if not branch or "id" not in branch:
            raise Exception(
                f"Branch creation failed: {branch_body}"
            )

        ctx["branches"].append(branch)

    print("Branches creadas:", len(ctx["branches"]))