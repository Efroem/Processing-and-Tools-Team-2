# Test file for all GET methods in the code

# To run tests. run the following command
# $env:PYTHONPATH="<root directory>" ; pytest api/tests/test_clients.py
import pytest
import requests
import sys
import os
import json
sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../../')))
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) 


# print(data_provider.fetch_client_pool().get_clients())


@pytest.fixture
def _data():
    return [{'URL': 'http://localhost:5000/api/v1/'}]


def test_get_clients_integration(_data):
    url = _data[0]["URL"] + 'Clients'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 and len(response_data) >= 1


def test_get_client_by_id_integration(_data):
    url = _data[0]["URL"] + 'Clients/1'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    # Verify that the status code is 200 (OK)
    print(response_data)
    assert status_code == 200 and response_data["clientId"] == 1

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'

def test_post_clients_integration(_data):
    url = _data[0]["URL"] + 'Clients'
    # params = {'id': 12}
    body = {
        "name": "Test-Test",
        "address": "12345 Test Suite 420",
        "city": "South Anthonymouth",
        "zipCode": "12345",
        "province": "Test-Province",
        "country": "United States",
        "contactName": "Testy Testra",
        "contactPhone": "431-688-3019",
        "contactEmail": "testtest@example.net"
    }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, json=body)
    assert post_response.status_code == 200
    warehouse_id = post_response.json().get("clientId")
    
    get_response = requests.get(f"{url}/{warehouse_id}")

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    print(warehouse_id)
    print(response_data)
    assert status_code == 200 and response_data["name"] == body["name"] and response_data["address"] == body["address"]

    dummy = requests.delete(f"{url}/{warehouse_id}")


def test_put_clients_integration(_data):
    url = _data[0]["URL"] + 'Clients/1'
    # params = {'id': 12}
    body = {
        "name": "Test-Test",
        "address": "12345 Test Suite 420",
        "city": "South Anthonymouth",
        "zipCode": "12345",
        "province": "Test-Province",
        "country": "United States",
        "contactName": "Testy Testra",
        "contactPhone": "431-688-3019",
        "contactEmail": "test@example.net"
    }
    dummy_get = requests.get(url)
    dummyJson = dummy_get.json()
    # Send a PUT request to the API and check if it was successful
    put_response = requests.put(url, json=body)
    assert put_response.status_code == 200
    client_id = put_response.json().get("clientId")
    get_response = requests.get(url)

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK) and the body in this code and the response data are basically equal
    assert status_code == 200 and response_data["clientId"] == client_id and response_data["name"] == body["name"] and response_data["address"] == body["address"]
    dummy = requests.put(url, json=dummyJson)


def test_delete_clients_integration(_data):
    # Make a POST reqeust first to make a dummy warehouse
    url = _data[0]["URL"] + 'Clients'
    # params = {'id': 12}
    body = {
        "name": "Test-Test",
        "address": "12345 Test Suite 420",
        "city": "South Anthonymouth",
        "zipCode": "12345",
        "province": "Test-Province",
        "country": "United States",
        "contactName": "Testy Testra",
        "contactPhone": "431-688-3019",
        "contactEmail": "test@example.net"
    }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, json=body)
    assert post_response.status_code == 200
    client_id = post_response.json().get("clientId")
    
    url += f"/{client_id}"

    # Send a DELETE request to the API and check if it was successful
    delete_response = requests.delete(url)
    assert delete_response.status_code == 200

    get2_response = requests.get(url)


    # Get the status code and response data
    status_code = get2_response.status_code
    response_data = None 
    try:
        response_data = get2_response.json()
    except:
        pass

    # Verify that the status code is 200 (OK) and that the warehouse doesn't exist anymore
    assert status_code == 404 and response_data == None







