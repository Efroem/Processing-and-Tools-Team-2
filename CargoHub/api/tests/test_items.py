# Test file for all GET methods in the code

# To run tests. run the following command
# $env:PYTHONPATH="<root directory>" ; pytest api/tests/test_items.py

import sys
import os

sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..'))) 


from api.models import orders
from api.providers import data_provider

data_provider.init()

# print(data_provider.fetch_client_pool().get_clients())


# def test_get_clients():
#     # data_provider.init()
#     clients = data_provider.fetch_client_pool().get_clients()
#     assert len(clients) >= 0



