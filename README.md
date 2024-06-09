# Tubes3_AkademiStima

## Deskripsi Tugas

Ini deskripsi tugas

## Cara Kompilasi

1. Inisialisasi database

    ```bash
    cd seeder
    dotnet restore
    dotnet build
    dotnet run
    ```

    Akan di-generate file `database.db` yang berisi database sqlite yang telah diisi dengan data faker yang dienkripsi beserta file `.env` yang berisi key dan iv dari enkripsi yang dilakukan. Pindahkan file `database.db` ke dalam folder src/Database dan file `.env` ke folder src.

2. Inisialisasi aplikasi

    ```bash
    cd src
    dotnet restore
    dotnet build
    dotnet run
    ```

    Command-command tersebut akan menginisialisasi packages yang digunakan pada aplikasi beserta melakukan proses build aplikasi dan menjalankannya.

3. Inisialisasi dataset

    Instruksi untuk dataset dapat dilihat lebih lanjut pada file README di folder test.

## Anggota

| NAMA ANGGOTA               | NIM      |
|----------------------------|----------|
| Ahmad Naufal Ramadan       | 13522005 |
| Muhammad Althariq Fairuz   | 13522027 |
| Muhammad Dava Fathurrahman | 13522114 |

## Acknowledgments

The design of this project was inspired by [THE CeCE IDEA TEAM](https://cece.uco.edu/idea/ProductCatalog/project.php?hash=3ef815416f775098fe977004015c6193).
