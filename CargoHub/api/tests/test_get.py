# Test file for all GET methods in the code
import sys
import os

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../')))


from api.models import orders
from api.providers import data_provider


def test_get_clients():
    assert True



