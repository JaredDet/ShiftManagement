from common import post_json


def seed_organization(ctx: dict):

    print("\nCreando empresas...")

    company1 = post_json(
        ctx["base_url"],
        "/api/companies",
        {
            "name": "Tech Solutions"
        }
    )

    company2 = post_json(
        ctx["base_url"],
        "/api/companies",
        {
            "name": "Retail Group"
        }
    )

    ctx["company1"] = company1
    ctx["company2"] = company2

    print("Company1:", company1["id"])
    print("Company2:", company2["id"])

    print("\nCreando sucursales...")

    branches = [
        {
            "companyId": company1["id"],
            "name": "Sucursal Centro",
            "address": "Av. Principal 123"
        },
        {
            "companyId": company1["id"],
            "name": "Sucursal Norte",
            "address": "Calle Norte 456"
        },
        {
            "companyId": company2["id"],
            "name": "Sucursal Mall",
            "address": "Mall Plaza 789"
        }
    ]

    for branch_body in branches:

        branch = post_json(
            ctx["base_url"],
            "/api/branches",
            branch_body
        )

        ctx["branches"].append(branch)

    print("Branches creadas:", len(ctx["branches"]))