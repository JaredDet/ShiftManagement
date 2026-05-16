import os

from dotenv import load_dotenv

from organization import seed_organization
from identity import seed_identity
from staff import seed_staff
from scheduling import seed_scheduling


load_dotenv()

ctx = {
    "base_url": f"http://localhost:{os.getenv('APP_PORT')}",

    "users": [],
    "staff_users": [],
    "employees": [],

    "branches": [],
    "shifts": [],
    "assignments": [],
    "replacements": []
}

print("\nINICIANDO SEED PIPELINE")

seed_organization(ctx)
seed_identity(ctx)
seed_staff(ctx)
seed_scheduling(ctx)

print("\nSEED COMPLETADO")