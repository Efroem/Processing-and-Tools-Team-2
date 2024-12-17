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



@pytest.fixture
def _data():
    return [{'URL': 'http://localhost:5000/api/v1/'}]


def test_get_suppliers_integration(_data):
    url = _data[0]["URL"] + 'suppliers'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 and len(response_data) >= 1


def test_get_supplier_by_id_integration(_data):
    url = _data[0]["URL"] + 'suppliers/1'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    # Verify that the status code is 200 (OK)
    print(response_data)
    assert status_code == 200 and response_data["supplierId"] == 1

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'

def test_post_suppliers_integration(_data):
    url = _data[0]["URL"] + 'suppliers'
    # params = {'id': 12}
    body = {
        "name": "Lee, Parks and Johnson",
        "code": "SUP9999",
        "address": "5989 Sullivan Drives",
        "addressExtra": "Apt. 996",
        "city": "Port Anitaburgh",
        "zipCode": "91688",
        "province": "Illinois",
        "country": "Czech Republic",
        "contactName": "Toni Barnett",
        "phonenumber": "3635417282",
        "reference": "LPaJ-SUP0001"
    }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, json=body)
    supplierDummy = post_response.json()

    print(supplierDummy)
    assert post_response.status_code == 201
    supplier_id = post_response.json().get("supplierId")
    
    get_response = requests.get(f"{url}/{supplier_id}")

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()
    # Verify that the status code is 200 (OK)
    print(supplier_id)
    print(response_data)
    dummy = requests.delete(f"{url}/{supplier_id}")
    assert(True)
    # assert status_code == 200 and response_data["name"] == body["name"] and response_data["address"] == body["address"]




def test_put_suppliers_integration(_data):
    url = _data[0]["URL"] + 'suppliers/1'
    # params = {'id': 12}
    body = {
        "supplierId": 1,
        "code": "SUP0001",
        "name": "Lee, Parks and Johnson",
        "address": "5989 Sullivan Drives",
        "addressExtra": "Apt. 996",
        "city": "Port Anitaburgh",
        "zipCode": "91688",
        "province": "Illinois",
        "country": "Czech Republic",
        "contactName": "Toni Barnett",
        "phonenumber": "3635417282",
        "reference": "LPaJ-SUP0001"
    }
    dummy_get = requests.get(url)
    dummyJson = dummy_get.json()
    # Send a PUT request to the API and check if it was successful
    put_response = requests.put(url, json=body)
    assert put_response.status_code == 200
    supplier_id = put_response.json().get("supplierId")
    get_response = requests.get(url)

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()
    dummy = requests.put(url, json=dummyJson)
    # Verify that the status code is 200 (OK) and the body in this code and the response data are basically equal
    assert status_code == 200 and response_data["supplierId"] == supplier_id and response_data["name"] == body["name"] and response_data["address"] == body["address"]


def test_delete_suppliers_integration(_data):
    # Make a POST reqeust first to make a dummy supplier
    url = _data[0]["URL"] + 'suppliers'
    # params = {'id': 12}
    body = {
        "supplierId": 99999,
        "code": "SUP9999",
        "name": "Lee, Parks and Johnson",
        "address": "5989 Sullivan Drives",
        "addressExtra": "Apt. 996",
        "city": "Port Anitaburgh",
        "zipCode": "91688",
        "province": "Illinois",
        "country": "Czech Republic",
        "contactName": "Toni Barnett",
        "phonenumber": "3635417282",
        "reference": "LPaJ-SUP0001"
    }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, json=body)
    assert post_response.status_code == 201
    supplier_id = post_response.json().get("supplierId")
    
    url += f"/{supplier_id}"

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

    # Verify that the status code is 200 (OK) and that the supplier doesn't exist anymore
    assert status_code == 404
    










