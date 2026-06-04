import json
import requests


def auth_headers(token: str | None = None):
    headers = {}

    if token:
        headers["Authorization"] = f"Bearer {token}"

    return headers


def post_json(base_url: str, path: str, body: dict, token: str | None = None):
    response = requests.post(
        f"{base_url}{path}",
        json=body,
        headers=auth_headers(token),
        timeout=30
    )

    return handle_response(response, body)


def get_json(base_url: str, path: str, token: str | None = None):
    response = requests.get(
        f"{base_url}{path}",
        headers=auth_headers(token),
        timeout=30
    )

    return handle_response(response)


def login(base_url: str, email: str, password: str):
    return post_json(
        base_url,
        "/api/auth/login",
        {
            "email": email,
            "password": password
        }
    )


def handle_response(response, body=None):
    try:
        response.raise_for_status()
        return safe_json(response)

    except requests.HTTPError:
        print("\nERROR REQUEST")
        print("STATUS:", response.status_code)
        print("URL:", response.request.url)

        if body:
            print("\nREQUEST BODY:")
            print(json.dumps(body, indent=2))

        print("\nRESPONSE:")
        print(response.text)

        raise

    except Exception as e:
        print("\nUNEXPECTED ERROR")
        print("STATUS:", response.status_code)
        print("URL:", response.request.url)
        print("ERROR:", str(e))
        print("RAW:", response.text)
        raise

def safe_json(response):
    try:
        if response.content and len(response.content.strip()) > 0:
            return response.json()
        return None
    except ValueError:
        return None