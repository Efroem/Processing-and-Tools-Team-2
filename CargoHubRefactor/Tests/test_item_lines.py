# Test file for all GET methods in the code


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


def test_get_item_lines_integration(_data):
    url = _data[0]["URL"] + 'Item_Lines'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 and len(response_data) >= 1


def test_get_item_lines_by_id_integration(_data):
    url = _data[0]["URL"] + 'Item_Lines/1'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    # Verify that the status code is 200 (OK)
    print(response_data)
    assert status_code == 200 and response_data["lineId"] == 1


def test_post_item_lines_integration(_data):
    url = _data[0]["URL"] + 'Item_Lines'
    # params = {'id': 12}
    body = {
        "name": "Test-Test",
        "description": "Test-Test-Test",
        "itemGroup": 1
    }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, json=body)
    assert post_response.status_code == 200
    lineId = post_response.json().get("lineId")
    
    get_response = requests.get(f"{url}/{lineId}")

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    print(lineId)
    print(response_data)
    dummy = requests.delete(f"{url}/{lineId}")
    assert status_code == 200 and response_data["name"] == body["name"] and response_data["description"] == body["description"]


def test_put_item_lines_integration(_data):
    url = _data[0]["URL"] + 'Item_Lines/1'
    # params = {'id': 12}
    body = {
        "name": "Test-Test",
        "description": "Test-Test-Test",
        "itemGroup": 1
    }
    dummy_get = requests.get(url)
    assert dummy_get.status_code == 200
    dummyJson = dummy_get.json()

    # Send a PUT request to the API and check if it was successful
    put_response = requests.put(url, json=body)
    assert put_response.status_code == 200
    lineId = put_response.json().get("lineId")

    # Get the status code and response data
    get_response = requests.get(url)
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK) and the body in this code and the response data are basically equal
    assert status_code == 200 and response_data["lineId"] == lineId and response_data["name"] == body["name"] and response_data["description"] == body["description"]
    dummy = requests.put(url, json=dummyJson)

def test_delete_item_lines_integration(_data):
    # Make a POST reqeust first to make a dummy warehouse
    url = _data[0]["URL"] + 'Item_Lines'
    # params = {'id': 12}
    body = {
        "name": "dummy",
        "description": "dummy",
        "itemGroup": 1
    }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, json=body)
    assert post_response.status_code == 200
    lineId = post_response.json().get("lineId")
    
    url += f"/{lineId}"

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









