from db import Db

if __name__ == "__main__":
    Db.init("src/Database/database.db", "src/Database/schema.sql")
