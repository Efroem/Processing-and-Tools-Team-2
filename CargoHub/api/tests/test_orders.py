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


def test_orders_response_no_key_integration(_data):
    url = _data[0]["URL"] + 'orders'
    # params = {'id': 12}

    # Send a GET request to the API
    response = requests.get(url)

    # Get the status code and response data
    status_code = response.status_code
    # response_data = response.json()

    # Verify that the status code is 401 (Unauthorized)
    assert status_code == 401


def test_get_orders_integration(_data):
    url = _data[0]["URL"] + 'orders'
    # params = {'id': 12}
    header = _data[1]

    # Send a GET request to the API
    response = requests.get(url, headers=header)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 and len(response_data) >= 1

def test_get_orders_by_id_integration(_data):
    url = _data[0]["URL"] + 'orders/2'
    # params = {'id': 12}
    header = _data[1]

    # Send a GET request to the API
    response = requests.get(url, headers=header)

    # Get the status code and response data
    status_code = response.status_code
    response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 and response_data["id"] == 2

    # Verify the response data
    # assert response_data['id'] == 123
    # assert response_data['name'] == 'John Smith'

def test_post_orders_integration(_data):
    url = _data[0]["URL"] + 'orders'
    # params = {'id': 12}
    header = _data[1]
    body =    {
        "id": 99999, 
        "source_id": 9, 
        "order_date": "1999-07-05T19:31:10Z", 
        "request_date":"1999-07-09T19:31:10Z", 
        "reference": "ORD00002", 
        "reference_extra": "Vergelijken raak geluid beetje altijd.", 
        "order_status": "Delivered", 
        "notes": "We hobby thee compleet wiel fijn.", 
        "shipping_notes": "Nood provincie hier.", 
        "picking_notes": "Borstelen dit verf suiker.", 
        "warehouse_id": 20, 
        "ship_to": null, 
        "bill_to": null, 
        "shipment_id": 2, 
        "total_amount": 8484.98, 
        "total_discount": 214.52, 
        "total_tax": 665.09, 
        "total_surcharge": 42.12, 
        "created_at": "1999-07-05T19:31:10Z", 
        "updated_at": "1999-07-07T15:31:10Z", 
        "items": [{"item_id": "P003790", "amount": 10}, {"item_id": "P007369", "amount": 15}, {"item_id": "P007311", "amount": 21}, {"item_id": "P004140", "amount": 8}, {"item_id": "P004413", "amount": 46}, {"item_id": "P004717", "amount": 38}, {"item_id": "P001919", "amount": 13}, {"item_id": "P010075", "amount": 5}, {"item_id": "P006603", "amount": 48}, {"item_id": "P004504", "amount": 30}, {"item_id": "P009594", "amount": 35}, {"item_id": "P008851", "amount": 25}, {"item_id": "P002129", "amount": 46}, {"item_id": "P002320", "amount": 4}, {"item_id": "P008341", "amount": 23}]
        }

    # Send a POST request to the API and check if it was successful
    post_response = requests.post(url, headers=header, json=body)
    assert post_response.status_code == 201

    get_response = requests.get(url + "/999999", headers=header)

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK)
    assert status_code == 200 
    dummy = requests.delete(url + "/999999", headers=header)


def test_put_orders_integration(_data):
    url = _data[0]["URL"] + 'orders/2'
    header = _data[1]
    body =    {
        "id": 1, 
        "source_id": 9, 
        "order_date": "1999-07-05T19:31:10Z", 
        "request_date":"1999-07-09T19:31:10Z", 
        "reference": "ORD00002", 
        "reference_extra": "Vergelijken raak geluid beetje altijd.", 
        "order_status": "Delivered", 
        "notes": "We hobby thee compleet wiel fijn.", 
        "shipping_notes": "Nood provincie hier.", 
        "picking_notes": "Borstelen dit verf suiker.", 
        "warehouse_id": 20, 
        "ship_to": null, 
        "bill_to": null, 
        "shipment_id": 2, 
        "total_amount": 8484.98, 
        "total_discount": 214.52, 
        "total_tax": 665.09, 
        "total_surcharge": 42.12, 
        "created_at": "1999-07-05T19:31:10Z", 
        "updated_at": "1999-07-07T15:31:10Z", 
        "items": [{"item_id": "P003790", "amount": 10}, {"item_id": "P007369", "amount": 15}, {"item_id": "P007311", "amount": 21}, {"item_id": "P004140", "amount": 8}, {"item_id": "P004413", "amount": 46}, {"item_id": "P004717", "amount": 38}, {"item_id": "P001919", "amount": 13}, {"item_id": "P010075", "amount": 5}, {"item_id": "P006603", "amount": 48}, {"item_id": "P004504", "amount": 30}, {"item_id": "P009594", "amount": 35}, {"item_id": "P008851", "amount": 25}, {"item_id": "P002129", "amount": 46}, {"item_id": "P002320", "amount": 4}, {"item_id": "P008341", "amount": 23}]
    }

    # Send a PUT request to the API and check if it was successful
    put_response = requests.put(url, headers=header, json=body)
    assert put_response.status_code == 200

    get_response = requests.get(url, headers=header)

    # Get the status code and response data
    status_code = get_response.status_code
    response_data = get_response.json()
    # response_data = response.json()

    # Verify that the status code is 200 (OK) and the body in this code and the response data are basically equal
    assert status_code == 200 and response_data["id"] == body["id"] and response_data["notes"] == body["notes"] and response_data["total_tax"] == body["total_tax"]


def test_delete_orders_integration(_data):
    url = _data[0]["URL"] + 'orders/35'
    header = _data[1]

    get1_response = requests.get(url, headers=header)

    # Send a DELETE request to the API and check if it was successful
    delete_response = requests.delete(url, headers=header)
    assert delete_response.status_code == 200

    get2_response = requests.get(url, headers=header)

    # Get the status code and response data
    status_code = get2_response.status_code
    response_data = get2_response.json()

    # Verify that the status code is 200 (OK) and that the client doesn't exist anymore
    assert status_code == 200 and response_data == None
    
    # Repost the deleted inventory for later use
    post_response = requests.post(url, headers=header, json=get1_response.json())








