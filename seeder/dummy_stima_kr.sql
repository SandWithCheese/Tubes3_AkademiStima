-- MariaDB dump 10.19-11.4.1-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: dummy_stima
-- ------------------------------------------------------
-- Server version	11.4.1-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*M!100616 SET @OLD_NOTE_VERBOSITY=@@NOTE_VERBOSITY, NOTE_VERBOSITY=0 */;

--
-- Table structure for table `biodata`
--

DROP TABLE IF EXISTS `biodata`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `biodata` (
  `NIK` varchar(16) NOT NULL,
  `nama` varchar(100) DEFAULT NULL,
  `tempat_lahir` varchar(50) DEFAULT NULL,
  `tanggal_lahir` date DEFAULT NULL,
  `jenis_kelamin` enum('Laki-Laki','Perempuan') DEFAULT NULL,
  `golongan_darah` varchar(5) DEFAULT NULL,
  `alamat` varchar(255) DEFAULT NULL,
  `agama` varchar(50) DEFAULT NULL,
  `status_perkawinan` enum('Belum Menikah','Menikah','Cerai') DEFAULT NULL,
  `pekerjaan` varchar(100) DEFAULT NULL,
  `kewarganegaraan` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`NIK`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `biodata`
--

LOCK TABLES `biodata` WRITE;
/*!40000 ALTER TABLE `biodata` DISABLE KEYS */;
INSERT INTO `biodata` VALUES
('02031791560422','4lC jhn50n','New Jeffrey','1994-05-02','Perempuan','AB','90253 Anderson Branch, Michaeltown, TX 19624','Hindu','Cerai','Designer, exhibition/display','China'),
('03034588491919','Davd jn35','Johnsonstad','2001-07-09','Perempuan','B','095 White Vista, Hallborough, IA 57603','Hindu','Menikah','Water engineer','Indonesia'),
('13393536604117','Rbrt BRwN','North Marymouth','2003-02-13','Laki-Laki','A','647 Kristin Passage Apt. 807, Lake Marytown, TN 43517','Islam','Cerai','Product designer','Indonesia'),
('20490773409890','jHn d','Lisamouth','1974-02-09','Perempuan','B','PSC 5497, Box 7189, APO AA 57519','Kristen','Belum Menikah','Herbalist','India'),
('28358759367383','DNl Wl5N','Lake Anthony','1980-01-10','Laki-Laki','AB','140 Hudson Avenue Suite 891, East Matthew, KY 22734','Kristen','Cerai','Conservation officer, historic buildings','Rusia'),
('45879327281543','M1LY 64rc','Perrychester','1977-10-29','Perempuan','O','616 Jennifer Avenue Suite 870, Port Chadport, NJ 94780','Konghucu','Belum Menikah','Oceanographer','Rusia'),
('48106543869985','j4n smith','Melanieville','1955-08-05','Perempuan','O','74457 Abbott Green Apt. 530, Port Rebeccatown, CT 64941','Budha','Cerai','Administrator, education','Arab'),
('67774905372892','5PH mrtN2','Perezstad','1991-12-26','Laki-Laki','A','8851 Emily Shoal, Kristinaborough, MN 49563','Hindu','Belum Menikah','Homeopath','Inggris'),
('75192780638829','m 4Ndr5n','Sarahport','1971-02-20','Perempuan','O','33286 Edwards Spurs, Davismouth, SC 42137','Katolik','Menikah','Engineer, aeronautical','India'),
('96653896815355','m1chl WLl1m5','Lake Karen','1972-04-11','Laki-Laki','B','775 Cynthia Stream, Nicoleton, KS 72153','Hindu','Cerai','Risk manager','Jepang');
/*!40000 ALTER TABLE `biodata` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sidik_jari`
--

DROP TABLE IF EXISTS `sidik_jari`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sidik_jari` (
  `berkas_citra` text DEFAULT NULL,
  `nama` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sidik_jari`
--

LOCK TABLES `sidik_jari` WRITE;
/*!40000 ALTER TABLE `sidik_jari` DISABLE KEYS */;
INSERT INTO `sidik_jari` VALUES
('test/sidik_jari0.BMP','John Doe'),
('test/sidik_jari1.BMP','Jane Smith'),
('test/sidik_jari2.BMP','Alice Johnson'),
('test/sidik_jari3.BMP','Robert Brown'),
('test/sidik_jari4.BMP','Michael Williams'),
('test/sidik_jari5.BMP','David Jones'),
('test/sidik_jari6.BMP','Emily Garcia'),
('test/sidik_jari7.BMP','Sophia Martinez'),
('test/sidik_jari8.BMP','Daniel Wilson'),
('test/sidik_jari9.BMP','Mia Anderson');
/*!40000 ALTER TABLE `sidik_jari` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*M!100616 SET NOTE_VERBOSITY=@OLD_NOTE_VERBOSITY */;

-- Dump completed on 2024-06-09 16:23:56
