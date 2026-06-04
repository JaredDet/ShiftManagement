from common import login, post_json

PASSWORD = "SeedOnly#2026"


def create_user(ctx, company_id, name, email, token):
    return post_json(
        ctx["base_url"],
        "/api/users",
        {
            "companyId": company_id,
            "name": name,
            "email": email,
            "password": PASSWORD
        },
        token
    )


def assign_role(ctx, user_id, role, token, branch_id=None):
    return post_json(
        ctx["base_url"],
        f"/api/users/{user_id}/roles",
        {
            "role": role,
            "branchId": branch_id
        },
        token
    )


# =========================
# HELPERS
# =========================

def get_admin_token(ctx, company_id):

    admin_user_id = (
        ctx["company1"]["adminUserId"]
        if company_id == ctx["company1"]["id"]
        else ctx["company2"]["adminUserId"]
    )

    token = ctx["tokens"].get(admin_user_id)

    if not token:
        raise Exception(
            f"Admin token not found for company {company_id}"
        )

    return token


# =========================
# SEED
# =========================

def seed_identity(ctx):

    print("\nCreando usuarios...")

    ctx.setdefault("tokens", {})

    ctx["users"] = []
    ctx["staff_users"] = []

    ctx["supervisors"] = []
    ctx["branch_managers"] = []

    if not ctx.get("branches"):
        raise Exception(
            "Branches must be seeded before identity"
        )

    tech_centro = ctx["branches"][0]
    tech_norte = ctx["branches"][1]
    retail_mall = ctx["branches"][2]

    users_data = [
        (
            ctx["company1"]["id"],
            "Laura Martínez",
            "laura.martinez@techsolutions.cl",
            "BranchManager",
            tech_centro["id"]
        ),
        (
            ctx["company1"]["id"],
            "Diego Fuentes",
            "diego.fuentes@techsolutions.cl",
            "Supervisor",
            tech_norte["id"]
        ),
        (
            ctx["company2"]["id"],
            "Camila Rojas",
            "camila.rojas@retailgroup.cl",
            "BranchManager",
            retail_mall["id"]
        ),
        (
            ctx["company2"]["id"],
            "Javier Soto",
            "javier.soto@retailgroup.cl",
            "Supervisor",
            retail_mall["id"]
        )
    ]

    # =========================
    # MANAGERS & SUPERVISORS
    # =========================

    for company_id, name, email, role, branch_id in users_data:

        admin_token = get_admin_token(
            ctx,
            company_id
        )

        user = create_user(
            ctx,
            company_id,
            name,
            email,
            admin_token
        )

        ctx["users"].append(user)

        assign_role(
            ctx,
            user["id"],
            role,
            admin_token,
            branch_id
        )

        role_record = {
            "id": user["id"],
            "email": user["email"],
            "companyId": company_id,
            "branchId": branch_id
        }

        if role == "Supervisor":
            ctx["supervisors"].append(role_record)

        elif role == "BranchManager":
            ctx["branch_managers"].append(role_record)

    # =========================
    # STAFF USERS
    # =========================

    for i in range(1, 16):

        company_id = (
            ctx["company2"]["id"]
            if i % 2 == 0
            else ctx["company1"]["id"]
        )

        admin_token = get_admin_token(
            ctx,
            company_id
        )

        user = create_user(
            ctx,
            company_id,
            f"Staff {i}",
            f"staff{i}@company.com",
            admin_token
        )

        ctx["users"].append(user)

    print(
        "Usuarios creados:",
        len(ctx["users"])
    )

    # =========================
    # STAFF ROLES
    # =========================

    branch_count = len(ctx["branches"])

    if branch_count == 0:
        raise Exception(
            "No branches available for staff assignment"
        )

    ctx["user_to_staff"] = {}

    staff_start_index = len(users_data)

    for i, user in enumerate(
        ctx["users"][staff_start_index:]
    ):

        branch = ctx["branches"][
            i % branch_count
        ]

        admin_token = get_admin_token(
            ctx,
            user["companyId"]
        )

        assign_role(
            ctx,
            user["id"],
            "Staff",
            admin_token,
            branch["id"]
        )

        staff_record = {
            "id": user["id"],
            "email": user["email"],
            "companyId": user["companyId"],
            "branchId": branch["id"]
        }

        ctx["staff_users"].append(
            staff_record
        )

        ctx["user_to_staff"][
            user["id"]
        ] = staff_record

    print(
        "Staff users:",
        len(ctx["staff_users"])
    )

    print(
        "Supervisors:",
        len(ctx["supervisors"])
    )

    print(
        "Branch managers:",
        len(ctx["branch_managers"])
    )

    # =========================
    # TOKENS
    # =========================

    print("\nGenerando tokens...")

    for user in ctx["users"]:

        token_response = login(
            ctx["base_url"],
            user["email"],
            PASSWORD
        )

        ctx["tokens"][
            user["id"]
        ] = token_response["accessToken"]

    print(
        "Tokens generados:",
        len(ctx["tokens"])
    )

    print(
        "Supervisor records:",
        ctx["supervisors"]
    )