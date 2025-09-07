from flask import Flask, render_template
from repository.mongo_repository import MongoRepository
from repository.student_stress_repository import StudentStressRepository
from dataset_downloader import DatasetDownloader
import time
import os
import io
import base64
import matplotlib.pyplot as plt
import pandas as pd

app = Flask(__name__)

mongo_repo = StudentStressRepository(uri="mongodb://admin:secret@mongo:27017/")

# Přidání testovacího záznamu do kolekce "Accounts"
def setup_accounts():
    while True:
        try:
            mongo_repo.connect_to_database()
            break
        except Exception as e:
            print("Waiting for MongoDB...", e, flush=True)
            time.sleep(2)

    test_account = {"username": "pepa123", "email": "pepa@example.com", "balance": 1000}
    inserted_id = mongo_repo.create_record("Accounts", test_account)
    print(f"Inserted test account with ID: {inserted_id}")



def load_data():
    # Wait for mongo
    while True:
        try:
            mongo_repo.connect_to_database()
            break
        except Exception as e:
            print("Waiting for MongoDB...", e, flush=True)
            time.sleep(2)

    # Get dataset
    dataset_path = os.path.join("/app", "Stress_Dataset.csv")
    df_stress = DatasetDownloader.download_dataset_file_csv(dataset_path)
    
    dataset_path = os.path.join("/app", "StressLevelDataset.csv")
    df_stress_level = DatasetDownloader.download_dataset_file_csv(dataset_path)

    # Normalize column names first
    df_stress.columns = [col.strip().lower().replace(" ", "_") for col in df_stress.columns]
    df_stress_level.columns = [col.strip().lower().replace(" ", "_") for col in df_stress_level.columns]
    # Create synthetic ID
    df_stress['student_id'] = df_stress.index
    df_stress_level['student_id'] = df_stress_level.index  # assuming rows correspond
    # Merge datasets on student_id
    df_merged = pd.merge(df_stress, df_stress_level, on='student_id', how='inner')

    # Save to database
    mongo_repo.clean_and_save(df_merged)

    # Show database content in console
    print(mongo_repo.read_all_records("students_stress"), flush=True)
    for record in mongo_repo.read_all_records("students_stress").find().limit(10):
        print(record, flush=True)

def visualize_data():
    # Load data from Mongo
    collection = mongo_repo.read_all_records("students_stress")
    records = list(collection.find({}, {"_id": 0}))  # fetch all docs as list of dicts

    if not records:
        return {}  # no data yet
    
    df = pd.DataFrame(records)

    # Simple visualizations
    # 1Gender distribution
    gender_counts = df['gender'].value_counts()
    fig1, ax1 = plt.subplots()
    gender_counts.plot(kind='bar', ax=ax1, color=['skyblue', 'salmon'])
    ax1.set_title("Gender Distribution")
    ax1.set_ylabel("Count")
    ax1.set_xlabel("Gender")

    # Convert figure to base64
    buf1 = io.BytesIO()
    fig1.savefig(buf1, format='png', bbox_inches='tight')
    buf1.seek(0)
    gender_img = base64.b64encode(buf1.getvalue()).decode('utf-8')
    plt.close(fig1)

    # Age histogram
    fig2, ax2 = plt.subplots()
    df['age'].hist(bins=10, ax=ax2, color='lightgreen')
    ax2.set_title("Age Distribution")
    ax2.set_xlabel("Age")
    ax2.set_ylabel("Count")

    buf2 = io.BytesIO()
    fig2.savefig(buf2, format='png', bbox_inches='tight')
    buf2.seek(0)
    age_img = base64.b64encode(buf2.getvalue()).decode('utf-8')
    plt.close(fig2)

    # Return base64 strings to embed in template
    return {'gender_chart': gender_img, 'age_chart': age_img}



@app.route("/")
@app.route("/home")
def home():
    # Přečteme první záznam z kolekce Accounts pro ověření
#    account = mongo_repo.read_record("Accounts", {"username": "pepa123"})
#    print("Account read from Mongo:", account)  # vypíše do konzole
    print("loading data?", flush=True)
    load_data()
    charts = visualize_data()
    return render_template("home.html", charts=charts)

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)
