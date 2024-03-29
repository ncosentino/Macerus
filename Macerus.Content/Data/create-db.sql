CREATE DATABASE  IF NOT EXISTS `macerus` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `macerus`;
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: macerus
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `affix_types`
--

DROP TABLE IF EXISTS `affix_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `affix_types` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `affix_types`
--

LOCK TABLES `affix_types` WRITE;
/*!40000 ALTER TABLE `affix_types` DISABLE KEYS */;
INSERT INTO `affix_types` VALUES (1,'normal'),(2,'magic'),(3,'rare'),(4,'imbued'),(5,'unique'),(6,'legendary'),(7,'relic');
/*!40000 ALTER TABLE `affix_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `enchantment_definitions`
--

DROP TABLE IF EXISTS `enchantment_definitions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enchantment_definitions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `serialized` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `enchantment_definitions`
--

LOCK TABLES `enchantment_definitions` WRITE;
/*!40000 ALTER TABLE `enchantment_definitions` DISABLE KEYS */;
/*!40000 ALTER TABLE `enchantment_definitions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magic_affixes`
--

DROP TABLE IF EXISTS `magic_affixes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magic_affixes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `value` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magic_affixes`
--

LOCK TABLES `magic_affixes` WRITE;
/*!40000 ALTER TABLE `magic_affixes` DISABLE KEYS */;
INSERT INTO `magic_affixes` VALUES (1,'Lively'),(2,'Hearty'),(3,'Magic'),(4,'of Life'),(5,'of Heartiness'),(6,'of Magic');
/*!40000 ALTER TABLE `magic_affixes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rare_name_affixes`
--

DROP TABLE IF EXISTS `rare_name_affixes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rare_name_affixes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `prefix` tinyint NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=311 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rare_name_affixes`
--

LOCK TABLES `rare_name_affixes` WRITE;
/*!40000 ALTER TABLE `rare_name_affixes` DISABLE KEYS */;
INSERT INTO `rare_name_affixes` VALUES (1,'Armageddon',1),(2,'Beast',1),(3,'Bitter',1),(4,'Blackhorn',1),(5,'Blood',1),(6,'Bone',1),(7,'Bramble',1),(8,'Brimstone',1),(9,'Carrion',1),(10,'Chaos',1),(11,'Corpse',1),(12,'Corruption',1),(13,'Cruel',1),(14,'Dire',1),(15,'Death',1),(16,'Demon',1),(17,'Doom',1),(18,'Dread',1),(19,'Eagle',1),(20,'Entropy',1),(21,'Fiend',1),(22,'Gale',1),(23,'Ghoul',1),(24,'Glyph',1),(25,'Grim',1),(26,'Hailstone',1),(27,'Havoc',1),(28,'Imp',1),(29,'Loath',1),(30,'Order',1),(31,'Pain',1),(32,'Plague',1),(33,'Raven',1),(34,'Rule',1),(35,'Rune',1),(36,'Shadow',1),(37,'Skull',1),(38,'Stone',1),(39,'Storm',1),(40,'Soul',1),(41,'Spirit',1),(42,'Viper',1),(43,'Wraith',1),(44,'Aegis',0),(45,'Badge',0),(46,'Band',0),(47,'Bar',0),(48,'Barb',0),(49,'Beads',0),(50,'Bite',0),(51,'Blazer',0),(52,'Blow',0),(53,'Bludgeon',0),(54,'Bolt',0),(55,'Branch',0),(56,'Brand',0),(57,'Breaker',0),(58,'Brogues',0),(59,'Brow',0),(60,'Buckle',0),(61,'Carapace',0),(62,'Casque',0),(63,'Circle',0),(64,'Circlet',0),(65,'Chain',0),(66,'Clasp',0),(67,'Claw',0),(68,'Cleaver',0),(69,'Cloak',0),(70,'Clutches',0),(71,'Coat',0),(72,'Coil',0),(73,'Collar',0),(74,'Cord',0),(75,'Cowl',0),(76,'Crack',0),(77,'Crest',0),(78,'Crusher',0),(79,'Cry',0),(80,'Dart',0),(81,'Edge',0),(82,'Emblem',0),(83,'Eye',0),(84,'Fang',0),(85,'Flange',0),(86,'Fletch',0),(87,'Flight',0),(88,'Finger',0),(89,'Fist',0),(90,'Fringe',0),(91,'Gnarl',0),(92,'Gnash',0),(93,'Goad',0),(94,'Gorget',0),(95,'Grasp',0),(96,'Greaves',0),(97,'Grinder',0),(98,'Grip',0),(99,'Guard',0),(100,'Gutter',0),(101,'Gyre',0),(102,'Hand',0),(103,'Harness',0),(104,'Harp',0),(105,'Hide',0),(106,'Heart',0),(107,'Hew',0),(108,'Hold',0),(109,'Hood',0),(110,'Horn',0),(111,'Impaler',0),(112,'Jack',0),(113,'Knell',0),(114,'Knot',0),(115,'Knuckle',0),(116,'Lance',0),(117,'Lash',0),(118,'Lock',0),(119,'Loom',0),(120,'Loop',0),(121,'Mallet',0),(122,'Mangler',0),(123,'Mantle',0),(124,'Mar',0),(125,'Mark',0),(126,'Mask',0),(127,'Master',0),(128,'Nails',0),(129,'Needle',0),(130,'Nock',0),(131,'Noose',0),(132,'Pale',0),(133,'Pelt',0),(134,'Picket',0),(135,'Prod',0),(136,'Quarrel',0),(137,'Quill',0),(138,'Razor',0),(140,'Reaver',0),(141,'Rend',0),(142,'Rock',0),(143,'Saw',0),(144,'Scalpel',0),(145,'Scarab',0),(146,'Scourge',0),(147,'Scratch',0),(148,'Scythe',0),(149,'Sever',0),(150,'Shank',0),(151,'Shell',0),(152,'Cap',0),(153,'Shield',0),(154,'Shroud',0),(155,'Skewer',0),(156,'Slayer',0),(157,'Smasher',0),(158,'Song',0),(160,'Spawn',0),(161,'Spike',0),(162,'Spiral',0),(163,'Splitter',0),(164,'Spur',0),(165,'Stake',0),(166,'Stalker',0),(167,'Star',0),(168,'Stinger',0),(169,'Strap',0),(170,'Suit',0),(171,'Sunder',0),(172,'Talisman',0),(173,'Thirst',0),(174,'Tooth',0),(175,'Torc',0),(176,'Touch',0),(177,'Tower',0),(178,'Track',0),(179,'Trample',0),(180,'Tread',0),(181,'Turn',0),(182,'Veil',0),(183,'Visage',0),(184,'Visor',0),(185,'Wand',0),(186,'Ward',0),(187,'Weaver',0),(188,'Winding',0),(189,'Wing',0),(190,'Whorl',0),(191,'Wood',0),(192,'Wrack',0),(193,'Wrap',0),(194,'Wretched',1),(195,'Alpha',1),(196,'Mystery',1),(197,'Primal',1),(198,'Eternity',1),(199,'Shadow',0),(200,'Phantom',1),(201,'Ghost',1),(202,'Spirit',0),(203,'Terror',1),(204,'Nether',1),(205,'Mirage',1),(206,'Mirage',0),(207,'Storm',0),(208,'Soul',0),(209,'Shard',0),(210,'Keeper',0),(211,'Insanity',1),(212,'Rage',1),(213,'Rage',0),(214,'Kiss',0),(215,'Night',1),(216,'Day',1),(217,'Reaper',1),(218,'Reaper',0),(219,'Arcane',1),(220,'Truth',1),(221,'Fall',0),(222,'Light',0),(223,'Ravager',0),(224,'Secret',0),(225,'Secret',1),(226,'Star',1),(227,'Infinity',1),(228,'Moon',1),(229,'Sun',1),(230,'Cage',0),(231,'Torment',0),(232,'Torment',1),(233,'Malice',1),(234,'Malice',0),(235,'Omen',0),(236,'Curse',0),(237,'Curse',1),(238,'Hate',1),(239,'Sorrow',1),(240,'Sorrow',0),(241,'Glory',0),(242,'Glory',1),(243,'Oath',1),(244,'Oath',0),(245,'Dusk',1),(246,'Dawn',1),(247,'Prophecy',1),(248,'Prophecy',0),(249,'Ash',1),(250,'Greed',1),(251,'Eater',0),(253,'Faith',0),(254,'Faith',1),(255,'Foul',1),(256,'Echo',1),(257,'Echo',0),(258,'Horror',1),(259,'Whisper',1),(260,'Whisper',0),(261,'Vessel',0),(262,'Torrent',0),(263,'Flesh',1),(264,'Twist',0),(265,'Sky',1),(266,'Promise',0),(267,'Bauble',0),(268,'Aspect',0),(269,'Warp',0),(270,'Warp',1),(271,'Scorch',1),(272,'Midnight',1),(273,'Tranquil',1),(274,'Thunder',1),(275,'Lust',1),(276,'Lust',0),(277,'Nemesis',0),(278,'Nemesis',1),(279,'Fury',1),(280,'Fury',0),(281,'Bond',0),(282,'Fool',1),(283,'Spire',0),(284,'Twilight',1),(285,'Stone',0),(286,'Mist',1),(287,'Bringer',0),(288,'Ender',0),(289,'Riddle',0),(290,'Riddle',1),(291,'Betrayer',0),(292,'Requiem',0),(293,'Dirge',0),(294,'Chant',0),(295,'Widow',0),(296,'Widow',1),(297,'Jaw',0),(298,'Devourer',0),(299,'Desire',0),(300,'Desire',1),(301,'Comet',1),(302,'Meteor',1),(303,'Amnesia',1),(304,'Nightmare',1),(305,'Eternal',1),(306,'Abyss',1),(307,'Abyss',0),(308,'Purity',1),(309,'Woe',1),(310,'Scream',0);
/*!40000 ALTER TABLE `rare_name_affixes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resource_cultures`
--

DROP TABLE IF EXISTS `resource_cultures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `resource_cultures` (
  `id` int NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `idresource_cultures_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resource_cultures`
--

LOCK TABLES `resource_cultures` WRITE;
/*!40000 ALTER TABLE `resource_cultures` DISABLE KEYS */;
/*!40000 ALTER TABLE `resource_cultures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resources_strings`
--

DROP TABLE IF EXISTS `resources_strings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `resources_strings` (
  `id` varchar(256) NOT NULL,
  `culture_id` int NOT NULL,
  `value` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resources_strings`
--

LOCK TABLES `resources_strings` WRITE;
/*!40000 ALTER TABLE `resources_strings` DISABLE KEYS */;
/*!40000 ALTER TABLE `resources_strings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stat_definition_bounds`
--

DROP TABLE IF EXISTS `stat_definition_bounds`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stat_definition_bounds` (
  `id` int NOT NULL AUTO_INCREMENT,
  `stat_definition_id` int NOT NULL,
  `minimum_expression` text,
  `maximum_expression` text,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stat_definition_bounds`
--

LOCK TABLES `stat_definition_bounds` WRITE;
/*!40000 ALTER TABLE `stat_definition_bounds` DISABLE KEYS */;
INSERT INTO `stat_definition_bounds` VALUES (1,1,'0',NULL),(2,2,'0','LIFE_MAXIMUM'),(4,3,'0',NULL),(5,4,'0','MANA_MAXIMUM');
/*!40000 ALTER TABLE `stat_definition_bounds` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stat_definitions`
--

DROP TABLE IF EXISTS `stat_definitions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stat_definitions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `term` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stat_definitions`
--

LOCK TABLES `stat_definitions` WRITE;
/*!40000 ALTER TABLE `stat_definitions` DISABLE KEYS */;
INSERT INTO `stat_definitions` VALUES (1,'LIFE_MAXIMUM'),(2,'LIFE_CURRENT'),(3,'MANA_MAXIMUM'),(4,'MANA_CURRENT'),(5,'LIGHT_RADIUS_RADIUS'),(6,'LIGHT_RADIUS_INTENSITY'),(7,'LIGHT_RADIUS_RED'),(8,'LIGHT_RADIUS_GREEN'),(9,'LIGHT_RADIUS_BLUE');
/*!40000 ALTER TABLE `stat_definitions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stat_definitions_to_resources_strings`
--

DROP TABLE IF EXISTS `stat_definitions_to_resources_strings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stat_definitions_to_resources_strings` (
  `id` int NOT NULL AUTO_INCREMENT,
  `stat_definition_id` int NOT NULL,
  `resources_strings_id` varchar(256) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `stat_definition_id_UNIQUE` (`stat_definition_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stat_definitions_to_resources_strings`
--

LOCK TABLES `stat_definitions_to_resources_strings` WRITE;
/*!40000 ALTER TABLE `stat_definitions_to_resources_strings` DISABLE KEYS */;
/*!40000 ALTER TABLE `stat_definitions_to_resources_strings` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-08-04 22:44:41
