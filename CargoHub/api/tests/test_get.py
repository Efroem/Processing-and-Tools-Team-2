# Test file for all GET methods in the code
import sys
import os

sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))


from api.models import orders
from api.providers import data_provider


def test_get_clients():
    clients = data_provider.fetch_client_pool().get_clients()
    assert clients is not None



