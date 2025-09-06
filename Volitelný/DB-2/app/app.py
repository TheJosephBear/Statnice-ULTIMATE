from flask import Flask, render_template
from repository.mongo_repository import MongoRepository
import time

app = Flask(__name__)

mongo_repo = MongoRepository(uri="mongodb://admin:secret@mongo:27017/", database_name="digitalTwin")

# Přidání testovacího záznamu do kolekce "Accounts"
def setup_accounts():
    while True:
        try:
            mongo_repo.connect_to_database()
            break
        except Exception as e:
            print("Waiting for MongoDB...", e)
            time.sleep(2)

    test_account = {"username": "pepa123", "email": "pepa@example.com", "balance": 1000}
    inserted_id = mongo_repo.create_record("Accounts", test_account)
    print(f"Inserted test account with ID: {inserted_id}")

# Zavoláme setup při startu Flasku
setup_accounts()

@app.route("/")
@app.route("/home")
def home():
    # Přečteme první záznam z kolekce Accounts pro ověření
    account = mongo_repo.read_record("Accounts", {"username": "pepa123"})
    print("Account read from Mongo:", account)  # vypíše do konzole
    return render_template("home.html")

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)
