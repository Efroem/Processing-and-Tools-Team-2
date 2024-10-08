# Test file for all GET methods in the code

# To run tests. run the following command
# $env:PYTHONPATH="<root directory>" ; pytest api/tests/test_clients.py
import pytest
import requests
import sys
import os
import json

sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) 

from api.models import clients
from api.models import orders
from api.providers import data_provider

data_provider.init()

# print(data_provider.fetch_client_pool().get_clients())


@pytest.fixture
def _data():
    return [{'URL': 'http://localhost:3000/api/v1/'}, {"API_KEY": "a1b2c3d4e5"}]


def test_get_clients_no_key_integration(_data):
    url = _data[0]["URL"] + 'clients'
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


def test_get_clients_integration(_data):
    url = _data[0]["URL"] + 'clients'
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

def test_get_clients_by_id_no_key_integration(_data):
    url = _data[0]["URL"] + 'clients/12'
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


def test_get_client_by_id_integration(_data):
    url = _data[0]["URL"] + 'clients/12'
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




def test_get_clients():
    clients = data_provider.fetch_client_pool().get_clients()
    assert len(clients) >= 0


def test_get_client_by_id():
    client = data_provider.fetch_client_pool().get_client(35)
    assert client["id"] == 35







