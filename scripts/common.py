import json
import requests


def post_json(base_url: str, path: str, body: dict):
    url = f"{base_url}{path}"

    try:
        response = requests.post(
            url,
            json=body,
            timeout=30
        )

        response.raise_for_status()

        return response.json()

    except requests.HTTPError:
        print("\nERROR POST")
        print("URL:", url)

        print("\nBODY:")
        print(json.dumps(body, indent=2))

        try:
            print("\nRESPONSE:")
            print(response.text)
        except Exception:
            pass

        raise