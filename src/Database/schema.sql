-- SQLite schema for tubes3_stima24

-- Table structure for table `biodata`
DROP TABLE IF EXISTS biodata;
CREATE TABLE biodata (
  NIK VARCHAR(16) NOT NULL PRIMARY KEY,
  nama VARCHAR(100) DEFAULT NULL,
  tempat_lahir VARCHAR(50) DEFAULT NULL,
  tanggal_lahir DATE DEFAULT NULL,
  jenis_kelamin VARCHAR(10) CHECK(jenis_kelamin IN ('Laki-Laki', 'Perempuan')) DEFAULT NULL,
  golongan_darah VARCHAR(5) DEFAULT NULL,
  alamat VARCHAR(255) DEFAULT NULL,
  agama VARCHAR(50) DEFAULT NULL,
  status_perkawinan VARCHAR(20) CHECK(status_perkawinan IN ('Belum Menikah', 'Menikah', 'Cerai')) DEFAULT NULL,
  pekerjaan VARCHAR(100) DEFAULT NULL,
  kewarganegaraan VARCHAR(50) DEFAULT NULL
);

-- Table structure for table `sidik_jari`
DROP TABLE IF EXISTS sidik_jari;
CREATE TABLE sidik_jari (
  berkas_citra TEXT,
  nama VARCHAR(100) DEFAULT NULL
);
