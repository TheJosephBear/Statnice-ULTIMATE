import requests
import pandas as pd
import io
import os
import kagglehub
from kagglehub import KaggleDatasetAdapter
import urllib.parse
import urllib.request

class DatasetDownloader:

    @staticmethod
    def download_dataset(url: str) -> pd.DataFrame:
        """Downloads a CSV dataset from the given URL and returns it as a DataFrame."""
        response = requests.get(url)
        response.raise_for_status()
        return pd.read_csv(io.StringIO(response.text))
    
    @staticmethod
    def download_dataset_file_csv(default_path: str = "dataset.csv") -> pd.DataFrame:
        if not os.path.exists(default_path):
            raise FileNotFoundError(f"Default dataset not found at: {default_path}")

        return pd.read_csv(default_path)
    
    # Kaggle je nahovno protože se musíš registrovat :-)
    @staticmethod
    def download_dataset_kaggle(dataset_string = "mdsultanulislamovi/student-stress-monitoring-datasets") -> pd.DataFrame:
        """Downloads a CSV dataset from the given URL and returns it as a DataFrame."""
        # Set the path to the file you'd like to load
        file_path = "Student_Stress.csv"

        # Load the latest version
        df = kagglehub.load_dataset(
            KaggleDatasetAdapter.PANDAS,
            dataset_string,
            file_path
            )
        return df
    
    # Musí se generovat API key na: https://api.store/czechia-api/czso.cz/dokumentace/
    @staticmethod
    def download_csu_dataset(url: str = 'https://api.apitalks.store/czso.cz/obyvatelstvo-domy/557467264') -> pd.DataFrame:
        """Downloads a csu  dataset from the given URL and returns it as a DataFrame."""

        req = urllib.request.Request(url)
        req.add_header('x-api-key', '<your api key here>')
        response = urllib.request.urlopen(req)
        data = response.read()

        print(data)