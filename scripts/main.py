import os
from dotenv import load_dotenv

from bootstrap import seed_bootstrap
from organization import seed_organization
from identity import seed_identity
from staff import seed_staff
from scheduling import seed_scheduling
from claim import seed_claims

load_dotenv()


def create_ctx():
    return {
        "base_url": f"http://localhost:{os.getenv('APP_PORT')}",

        "companies": {},
        "branches": [],

        "users": [],
        "staff_users": [],
        "collaborators": [],

        "tokens": {},

        "claims": [],
        "shifts": [],
        "assignments": [],
        "replacements": [],
        "swap_requests": [],
        "approved_swaps": []
    }


def run_seed():

    ctx = create_ctx()

    print("\n=========================")
    print("BOOTSTRAP")
    print("=========================")

    seed_bootstrap(ctx)

    print("\n=========================")
    print("ORGANIZATION")
    print("=========================")

    seed_organization(ctx)

    print("\n=========================")
    print("IDENTITY")
    print("=========================")

    seed_identity(ctx)

    print("\n=========================")
    print("STAFF")
    print("=========================")

    seed_staff(ctx)

    print("\n=========================")
    print("SCHEDULING")
    print("=========================")

    seed_scheduling(ctx)

    print("\n=========================")
    print("CLAIMS")
    print("=========================")

    seed_claims(ctx)

    print("\n=========================")
    print("SEED COMPLETADO")
    print("=========================")


if __name__ == "__main__":
    run_seed()