import sqlite3
import os
import faker
import random


class Data:
    # Generate sidik jari tuples
    @staticmethod
    def generate_sidik_jari_tuples() -> list[tuple[str, str]]:
        # Initialize faker
        fake = faker.Faker()

        # Base path
        base_path = "test/"

        # List all files and remove README.md
        test_files = os.listdir("test")
        test_files.remove("README.md")

        # Generate sidik jari tuples
        sidik_jari = []
        check_number = None
        current_name = None
        for f in test_files:
            current_number = f.split("_")[0]
            if check_number == None and current_name == None:
                current_name = fake.name()
                check_number = current_number

            if check_number != current_number:
                current_name = fake.name()
                check_number = current_number

            sidik_jari.append((base_path + f, current_name))

        return sidik_jari

    # Generate biodata tuples
    # @staticmethod
    def generate_biodata_tuples(
        sidik_jari: list[tuple[str, str]]
    ) -> list[tuple[str, str]]:
        # Initialize faker
        fake = faker.Faker()

        # Generate biodata tuples
        biodata = []
        data = {
            "nik": None,
            "nama": None,
            "tempat_lahir": None,
            "tanggal_lahir": None,
            "jenis_kelamin": None,
            "golongan_darah": None,
            "alamat": None,
            "agama": None,
            "status_perkawinan": None,
            "pekerjaan": None,
            "kewarganegaraan": None,
        }

        for _, name in sidik_jari:
            if not all(data.values()):
                data["nik"] = str(fake.random_number(16))
                data["nama"] = name
                data["tempat_lahir"] = fake.city()
                data["tanggal_lahir"] = fake.date_of_birth()
                data["jenis_kelamin"] = random.choice(["Laki-Laki", "Perempuan"])
                data["golongan_darah"] = random.choice(["A", "B", "AB", "O"])
                data["alamat"] = fake.address()
                data["agama"] = random.choice(
                    ["Islam", "Kristen", "Katolik", "Hindu", "Budha", "Konghucu"]
                )
                data["status_perkawinan"] = random.choice(
                    ["Belum Menikah", "Menikah", "Cerai"]
                )
                data["pekerjaan"] = fake.job()
                data["kewarganegaraan"] = random.choice(["WNI", "WNA"])

            if data["nama"] != name:
                data["nik"] = str(fake.random_number(16))
                data["nama"] = name
                data["tempat_lahir"] = fake.city()
                data["tanggal_lahir"] = fake.date_of_birth().strftime("%Y-%m-%d")
                data["jenis_kelamin"] = random.choice(["Laki-Laki", "Perempuan"])
                data["golongan_darah"] = random.choice(["A", "B", "AB", "O"])
                data["alamat"] = fake.address()
                data["agama"] = random.choice(
                    ["Islam", "Kristen", "Katolik", "Hindu", "Budha", "Konghucu"]
                )
                data["status_perkawinan"] = random.choice(
                    ["Belum Menikah", "Menikah", "Cerai"]
                )
                data["pekerjaan"] = fake.job()
                data["kewarganegaraan"] = random.choice(["WNI", "WNA"])
            else:
                continue

            biodata.append(
                (
                    data["nik"],
                    Data.alay_generator(data["nama"]),
                    data["tempat_lahir"],
                    data["tanggal_lahir"],
                    data["jenis_kelamin"],
                    data["golongan_darah"],
                    data["alamat"],
                    data["agama"],
                    data["status_perkawinan"],
                    data["pekerjaan"],
                    data["kewarganegaraan"],
                )
            )

        return biodata

    @staticmethod
    def alay_generator(name: str) -> str:
        methods = [
            "orisinil",
            "orisinil",
            "orisinil",
            "besar-kecil",
            "angka",
            "singkat",
            "kombinasi",
        ]

        method = random.choice(methods)

        if method == "orisinil":
            return name
        elif method == "besar-kecil":
            return Data.besarkecil(name)
        elif method == "angka":
            return Data.angka(name)
        elif method == "singkat":
            return Data.singkat(name)
        elif method == "kombinasi":
            return Data.kombinasi(name)

    @staticmethod
    def besarkecil(name: str) -> str:
        temp = ""
        for i in range(len(name)):
            if random.randint(0, 1) == 0:
                temp += name[i].lower()
            else:
                temp += name[i].upper()
        return temp

    @staticmethod
    def angka(name: str) -> str:
        mapping = {
            "o": "0",
            "O": "0",
            "i": "1",
            "I": "1",
            "l": "1",
            "z": "2",
            "Z": "2",
            "E": "3",
            "A": "4",
            "s": "5",
            "S": "5",
            "G": "6",
            "T": "7",
            "B": "8",
            "g": "9",
        }

        temp = ""
        for i in range(len(name)):
            if mapping.get(name[i]) != None:
                temp += mapping[name[i]]
            else:
                temp += name[i]

        return temp

    @staticmethod
    def singkat(name: str) -> str:
        temp = ""

        arr_name = name.split(" ")
        for name in arr_name:
            for i in range(len(name)):
                if random.randint(1, 10) > 3:
                    temp += name[i]

            temp += " "

        return temp

    @staticmethod
    def kombinasi(name: str) -> str:
        return Data.singkat(Data.angka(Data.besarkecil(name)))


class Db:
    # Static variable to store connection
    conn: sqlite3.Connection = None
    cur: sqlite3.Cursor = None

    # Static method to initialize db
    @staticmethod
    def init(db_path: str, schema_path: str):
        Db.connect(db_path)
        Db.migrate(schema_path)
        Db.seed()

    # Static method to initialize connection
    @staticmethod
    def connect(db_path: str):
        try:
            Db.conn = sqlite3.connect(db_path, timeout=10)
            Db.cur = Db.conn.cursor()
            Db.cur.execute("PRAGMA foreign_keys = ON")  # Enable foreign key constraints
        except sqlite3.Error as e:
            print("Error connecting to db")
            print(e)
        except Exception as e:
            print(e)

    # Static method to migrate db schema
    @staticmethod
    def migrate(migrate_path: str):
        try:
            # Read schema.sql file
            file = open(migrate_path, "r")
            schema = file.read()
            # Execute schema
            Db.cur.executescript(schema)
            # Commit
            Db.conn.commit()
        except sqlite3.Error as e:
            print("Error migrating schema")
            print(e)
        except Exception as e:
            print(e)

    # Seeding
    @staticmethod
    def seed():
        # Seed data
        sidik_jari_tuples = Data.generate_sidik_jari_tuples()
        biodata_tuples = Data.generate_biodata_tuples(sidik_jari_tuples)

        try:
            # Begin transaction
            Db.cur.execute("BEGIN TRANSACTION")

            # Remove all data
            Db.cur.execute("DELETE FROM biodata")
            Db.cur.execute("DELETE FROM sidik_jari")

            # Insert data
            Db.cur.executemany(
                "INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
                biodata_tuples,
            )
            Db.cur.executemany(
                "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (?, ?)",
                sidik_jari_tuples,
            )

            # Commit transaction
            Db.conn.commit()

        except sqlite3.Error as e:
            # If error, rollback connnection
            Db.conn.rollback()
            print("Error seeding data")
            print(e)
        except Exception as e:
            print(e)

    # Close connection & reset connection
    def close():
        # Close connection
        Db.conn.close()
        Db.conn = None
        Db.cur = None
