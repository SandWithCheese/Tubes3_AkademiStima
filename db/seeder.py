from db import Db

if __name__ == "__main__":
    Db.init("db/database.db", "db/schema.sql")
