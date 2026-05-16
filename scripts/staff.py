from common import post_json


def create_employee(
    ctx,
    user_id,
    company_id,
    branch_id,
    position_id
):

    return post_json(
        ctx["base_url"],
        "/api/employees",
        {
            "userId": user_id,
            "companyId": company_id,
            "mainBranchId": branch_id,
            "mainPositionId": position_id
        }
    )


def seed_staff(ctx):

    print("\nCreando módulo Staff...")

    developer_position = post_json(
        ctx["base_url"],
        "/api/positions",
        {
            "companyId": ctx["company1"]["id"],
            "name": "Backend Developer",
            "description": "Encargado del backend"
        }
    )

    seller_position = post_json(
        ctx["base_url"],
        "/api/positions",
        {
            "companyId": ctx["company2"]["id"],
            "name": "Sales Assistant",
            "description": "Ventas"
        }
    )

    ctx["developer_position_id"] = developer_position["id"]
    ctx["seller_position_id"] = seller_position["id"]

    for user in ctx["staff_users"]:

        if user["companyId"] == ctx["company1"]["id"]:
            position_id = ctx["developer_position_id"]
        else:
            position_id = ctx["seller_position_id"]

        employee = create_employee(
            ctx,
            user["id"],
            user["companyId"],
            user["branchId"],
            position_id
        )

        ctx["employees"].append(employee)

    print("Employees creados:", len(ctx["employees"]))