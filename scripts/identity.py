from common import post_json


PASSWORD = "Str0ng!Pass123"


def create_user(ctx, company_id, name, email):

    return post_json(
        ctx["base_url"],
        "/api/users",
        {
            "companyId": company_id,
            "name": name,
            "email": email,
            "password": PASSWORD
        }
    )


def assign_role(ctx, user_id, role, branch_id=None):

    body = {
        "role": role,
        "branchId": branch_id
    }

    post_json(
        ctx["base_url"],
        f"/api/users/{user_id}/roles",
        body
    )


def seed_identity(ctx):

    print("\nCreando usuarios...")

    users_data = [
        (ctx["company1"]["id"], "Admin One", "admin1@tech.com"),
        (ctx["company1"]["id"], "Manager One", "manager1@tech.com"),
        (ctx["company1"]["id"], "Supervisor One", "supervisor1@tech.com"),
        (ctx["company2"]["id"], "Admin Two", "admin2@retail.com"),
        (ctx["company2"]["id"], "Manager Two", "manager2@retail.com"),
    ]

    for company_id, name, email in users_data:

        user = create_user(ctx, company_id, name, email)

        ctx["users"].append(user)

    for i in range(1, 16):

        company_id = (
            ctx["company2"]["id"]
            if i % 2 == 0
            else ctx["company1"]["id"]
        )

        user = create_user(
            ctx,
            company_id,
            f"Staff {i}",
            f"staff{i}@company.com"
        )

        ctx["users"].append(user)

    print("Usuarios creados:", len(ctx["users"]))

    assign_role(ctx, ctx["users"][0]["id"], "CompanyAdmin")
    assign_role(ctx, ctx["users"][1]["id"], "BranchManager")
    assign_role(ctx, ctx["users"][2]["id"], "Supervisor")
    assign_role(ctx, ctx["users"][3]["id"], "CompanyAdmin")
    assign_role(ctx, ctx["users"][4]["id"], "BranchManager")

    branch_count = len(ctx["branches"])

    for i, user in enumerate(ctx["users"][5:]):

        branch = ctx["branches"][i % branch_count]

        assign_role(
            ctx,
            user["id"],
            "Staff",
            branch["id"]
        )

        ctx["staff_users"].append({
            "id": user["id"],
            "email": user["email"],
            "companyId": user["companyId"],
            "branchId": branch["id"]
        })

    print("Staff users:", len(ctx["staff_users"]))