from common import login, post_json

PASSWORD = "DevCompany2026!"

COMPANY1_EMAIL = "c.herrera@techsolutions.cl"
COMPANY2_EMAIL = "m.gonzalez@retailgroup.cl"


def bootstrap_company(
    ctx,
    company_name,
    admin_name,
    admin_email,
    password
):
    return post_json(
        ctx["base_url"],
        "/api/dev/bootstrap",
        {
            "companyName": company_name,
            "adminName": admin_name,
            "adminEmail": admin_email,
            "password": password
        }
    )


def seed_bootstrap(ctx):

    print("\nBootstrapping companies...")

    ctx["companies"] = {}
    ctx.setdefault("tokens", {})

    company1 = bootstrap_company(
        ctx,
        "Tech Solutions",
        "Carlos Herrera",
        COMPANY1_EMAIL,
        PASSWORD
    )

    company2 = bootstrap_company(
        ctx,
        "Retail Group",
        "María González",
        COMPANY2_EMAIL,
        PASSWORD
    )

    # =========================
    # VALIDATION
    # =========================

    if not company1 or "companyId" not in company1:
        raise Exception("Company1 bootstrap failed")

    if not company2 or "companyId" not in company2:
        raise Exception("Company2 bootstrap failed")

    # =========================
    # CONTEXT NORMALIZATION
    # =========================

    ctx["company1"] = {
        "id": company1["companyId"],
        "adminUserId": company1["adminUserId"],
        "adminEmail": COMPANY1_EMAIL
    }

    ctx["company2"] = {
        "id": company2["companyId"],
        "adminUserId": company2["adminUserId"],
        "adminEmail": COMPANY2_EMAIL
    }

    ctx["companies"][company1["companyId"]] = company1
    ctx["companies"][company2["companyId"]] = company2

    print("Company1:", company1["companyId"])
    print("Company2:", company2["companyId"])

    # =========================
    # TOKENS
    # =========================

    company1_login = login(
        ctx["base_url"],
        ctx["company1"]["adminEmail"],
        PASSWORD
    )

    company2_login = login(
        ctx["base_url"],
        ctx["company2"]["adminEmail"],
        PASSWORD
    )

    company1_token = company1_login["accessToken"]
    company2_token = company2_login["accessToken"]

    ctx["tokens"][company1["adminUserId"]] = company1_token
    ctx["tokens"][company2["adminUserId"]] = company2_token

    print("Admin tokens generated")