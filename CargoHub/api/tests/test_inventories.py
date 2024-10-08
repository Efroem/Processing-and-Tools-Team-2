# Test file for all GET methods in the code

# To run tests. run the following command
# $env:PYTHONPATH="<root directory>" ; pytest api/tests/test_inventories.py

import sys
import os
import pytest
import requests

sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) 


from api.models import orders
from api.providers import data_provider

data_provider.init()



@pytest.fixture
def _data():
    return [{'URL': 'http://localhost:3000/api/v1/'}, {"API_KEY": "a1b2c3d4e5"}]


def test_get_inventories_no_key_integration(_data):
    url = _data[0]["URL"] + 'inventories'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    # response_data = response.json()

    # Verify that the status code is 401 (Unauthorized)
    assert status_code == 401

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'


def test_get_inventories_integration(_data):
    url = _data[0]["URL"] + 'inventories'
    # params = {'id': 12}
    header = _data[1]

    # Send a GET request to the API
    response = requests.get(url, headers=header)

    # Get the status code and response data
    status_code = response.status_code
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'

def test_get_inventory_by_id_no_key_integration(_data):
    url = _data[0]["URL"] + 'inventories/12'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    # response_data = response.json()
    # Verify that the status code is 401 (Unauthorized)
    assert status_code == 401 ## and response_data["id"] == 12

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'


def test_get_inventory_by_id_integration(_data):
    url = _data[0]["URL"] + 'inventories/12'
    # params = {'id': 12}
    header = _data[1]

    # Send a GET request to the API
    response = requests.get(url, headers=header)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 and response_data["id"] == 12

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'




def test_get_inventories():
    inventories = data_provider.fetch_inventory_pool().get_inventories()
    assert len(inventories) >= 0


def test_get_inventory_by_id():
    inventory = data_provider.fetch_inventory_pool().get_inventory(35)
    assert inventory["id"] == 35





