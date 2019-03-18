-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Client :  localhost
-- Généré le :  Lun 11 Mars 2019 à 14:50
-- Version du serveur :  5.7.25-0ubuntu0.16.04.2
-- Version de PHP :  7.0.33-0ubuntu0.16.04.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `serioustest`
--
CREATE DATABASE IF NOT EXISTS `serioustest` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `serioustest`;

-- --------------------------------------------------------

--
-- Structure de la table `aptitude`
--

DROP TABLE IF EXISTS `aptitude`;
CREATE TABLE IF NOT EXISTS `aptitude` (
  `IDAptitude` int(20) NOT NULL AUTO_INCREMENT,
  `AptitudeName` varchar(50) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `LevelRequired` int(20) NOT NULL,
  PRIMARY KEY (`IDAptitude`),
  KEY `id_skill_index` (`IDSkill`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `aptitude`
--

TRUNCATE TABLE `aptitude`;
-- --------------------------------------------------------

--
-- Structure de la table `artefact`
--

DROP TABLE IF EXISTS `artefact`;
CREATE TABLE IF NOT EXISTS `artefact` (
  `IDArtefact` int(11) NOT NULL AUTO_INCREMENT,
  `ArtefactName` varchar(20) NOT NULL,
  `IDBonus` int(11) NOT NULL,
  PRIMARY KEY (`IDArtefact`),
  KEY `fk_id_bonus_artefact` (`IDBonus`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `artefact`
--

TRUNCATE TABLE `artefact`;
--
-- Contenu de la table `artefact`
--

INSERT INTO `artefact` (`IDArtefact`, `ArtefactName`, `IDBonus`) VALUES
(1, 'Bling-Bling', 1),
(2, 'Magasin de poche', 8),
(3, 'Cyber-Implant', 4),
(4, 'Coffre Mystère', 7),
(5, 'Rubixcube Magique', 6),
(6, 'Téléphone Nokia', 5);

-- --------------------------------------------------------

--
-- Structure de la table `artefact_pc`
--

DROP TABLE IF EXISTS `artefact_pc`;
CREATE TABLE IF NOT EXISTS `artefact_pc` (
  `IDArtefact` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  PRIMARY KEY (`IDArtefact`,`IDPCharacter`),
  KEY `fk_id_pc_artefact` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `artefact_pc`
--

TRUNCATE TABLE `artefact_pc`;
--
-- Contenu de la table `artefact_pc`
--

INSERT INTO `artefact_pc` (`IDArtefact`, `IDPCharacter`) VALUES
(6, 1);

-- --------------------------------------------------------

--
-- Structure de la table `artefact_used`
--

DROP TABLE IF EXISTS `artefact_used`;
CREATE TABLE IF NOT EXISTS `artefact_used` (
  `IDArtefact` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  PRIMARY KEY (`IDArtefact`,`IDPCharacter`),
  KEY `fk_id_pc_artefact_used` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `artefact_used`
--

TRUNCATE TABLE `artefact_used`;
-- --------------------------------------------------------

--
-- Structure de la table `association_city_district`
--

DROP TABLE IF EXISTS `association_city_district`;
CREATE TABLE IF NOT EXISTS `association_city_district` (
  `IDCity` int(20) NOT NULL,
  `IDDistrict` int(20) NOT NULL,
  KEY `id_district_index` (`IDDistrict`) USING BTREE,
  KEY `fk_id_city` (`IDCity`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `association_city_district`
--

TRUNCATE TABLE `association_city_district`;
--
-- Contenu de la table `association_city_district`
--

INSERT INTO `association_city_district` (`IDCity`, `IDDistrict`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `association_company_district`
--

DROP TABLE IF EXISTS `association_company_district`;
CREATE TABLE IF NOT EXISTS `association_company_district` (
  `IDCompany` int(11) NOT NULL,
  `IDDistrict` int(11) NOT NULL,
  PRIMARY KEY (`IDCompany`,`IDDistrict`),
  KEY `fk_id_district_company` (`IDDistrict`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_company_district`
--

TRUNCATE TABLE `association_company_district`;
--
-- Contenu de la table `association_company_district`
--

INSERT INTO `association_company_district` (`IDCompany`, `IDDistrict`) VALUES
(1, 1),
(5, 1),
(7, 1),
(10, 1),
(12, 1),
(1, 2),
(5, 2),
(10, 2),
(11, 2),
(12, 2),
(1, 3),
(2, 3),
(6, 3),
(9, 3),
(10, 3),
(11, 3),
(1, 4),
(2, 4),
(7, 4),
(11, 4),
(12, 4),
(1, 5),
(2, 5),
(6, 5),
(7, 5),
(8, 5),
(1, 6),
(5, 6),
(8, 6),
(9, 6),
(1, 8),
(5, 8),
(6, 8),
(8, 8),
(9, 8);

-- --------------------------------------------------------

--
-- Structure de la table `association_diploms`
--

DROP TABLE IF EXISTS `association_diploms`;
CREATE TABLE IF NOT EXISTS `association_diploms` (
  `IDDiplom` int(11) NOT NULL,
  `IDDiplomRequiered` int(11) NOT NULL,
  KEY `id_diplom_index` (`IDDiplomRequiered`,`IDDiplom`) USING BTREE,
  KEY `fk_id_diplom` (`IDDiplom`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_diploms`
--

TRUNCATE TABLE `association_diploms`;
--
-- Contenu de la table `association_diploms`
--

INSERT INTO `association_diploms` (`IDDiplom`, `IDDiplomRequiered`) VALUES
(3, 2);

-- --------------------------------------------------------

--
-- Structure de la table `association_district_entertainment`
--

DROP TABLE IF EXISTS `association_district_entertainment`;
CREATE TABLE IF NOT EXISTS `association_district_entertainment` (
  `IDDistrict` int(11) NOT NULL,
  `IDEntertainment` int(11) NOT NULL,
  PRIMARY KEY (`IDDistrict`,`IDEntertainment`),
  KEY `fk_id_entertainment_district` (`IDEntertainment`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_district_entertainment`
--

TRUNCATE TABLE `association_district_entertainment`;
--
-- Contenu de la table `association_district_entertainment`
--

INSERT INTO `association_district_entertainment` (`IDDistrict`, `IDEntertainment`) VALUES
(1, 1),
(2, 2);

-- --------------------------------------------------------

--
-- Structure de la table `association_district_npc`
--

DROP TABLE IF EXISTS `association_district_npc`;
CREATE TABLE IF NOT EXISTS `association_district_npc` (
  `IDDistrict` int(20) NOT NULL,
  `IDNPCharacter` int(20) NOT NULL,
  KEY `id_district_index` (`IDDistrict`),
  KEY `fk_id_npc` (`IDNPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `association_district_npc`
--

TRUNCATE TABLE `association_district_npc`;
--
-- Contenu de la table `association_district_npc`
--

INSERT INTO `association_district_npc` (`IDDistrict`, `IDNPCharacter`) VALUES
(1, 1),
(2, 3);

-- --------------------------------------------------------

--
-- Structure de la table `association_district_place`
--

DROP TABLE IF EXISTS `association_district_place`;
CREATE TABLE IF NOT EXISTS `association_district_place` (
  `IDDistrict` int(11) NOT NULL,
  `IDPlace` int(11) NOT NULL,
  PRIMARY KEY (`IDDistrict`,`IDPlace`),
  KEY `fk_id_place_district` (`IDPlace`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_district_place`
--

TRUNCATE TABLE `association_district_place`;
-- --------------------------------------------------------

--
-- Structure de la table `association_ip_pc`
--

DROP TABLE IF EXISTS `association_ip_pc`;
CREATE TABLE IF NOT EXISTS `association_ip_pc` (
  `ip` varchar(256) NOT NULL,
  `playerId` int(11) NOT NULL,
  `isConnected` tinyint(1) NOT NULL,
  UNIQUE KEY `ip` (`ip`),
  KEY `fk_id_character` (`playerId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_ip_pc`
--

TRUNCATE TABLE `association_ip_pc`;
--
-- Contenu de la table `association_ip_pc`
--

INSERT INTO `association_ip_pc` (`ip`, `playerId`, `isConnected`) VALUES
('139.124.242.37', 23, 1),
('172.24.10.231', 23, 0),
('192.168.200.2', 42, 1),
('192.168.56.1', 23, 1);

-- --------------------------------------------------------

--
-- Structure de la table `association_job_character`
--

DROP TABLE IF EXISTS `association_job_character`;
CREATE TABLE IF NOT EXISTS `association_job_character` (
  `IDJob` int(20) NOT NULL,
  `IDPCharacter` int(20) NOT NULL,
  KEY `id_job_index` (`IDJob`),
  KEY `id_character_index` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `association_job_character`
--

TRUNCATE TABLE `association_job_character`;
-- --------------------------------------------------------

--
-- Structure de la table `association_job_skill`
--

DROP TABLE IF EXISTS `association_job_skill`;
CREATE TABLE IF NOT EXISTS `association_job_skill` (
  `IDJob` int(20) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `IDRank` int(20) NOT NULL,
  KEY `id_job_index` (`IDJob`),
  KEY `id_skill_index` (`IDSkill`),
  KEY `id_rank_index` (`IDRank`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `association_job_skill`
--

TRUNCATE TABLE `association_job_skill`;
-- --------------------------------------------------------

--
-- Structure de la table `association_place_exam`
--

DROP TABLE IF EXISTS `association_place_exam`;
CREATE TABLE IF NOT EXISTS `association_place_exam` (
  `IDPlace` int(11) NOT NULL,
  `IDExam` int(11) NOT NULL,
  PRIMARY KEY (`IDPlace`,`IDExam`),
  KEY `fk_id_exam_place` (`IDExam`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_place_exam`
--

TRUNCATE TABLE `association_place_exam`;
--
-- Contenu de la table `association_place_exam`
--

INSERT INTO `association_place_exam` (`IDPlace`, `IDExam`) VALUES
(2, 1),
(2, 2),
(2, 3);

-- --------------------------------------------------------

--
-- Structure de la table `association_ressource_pc`
--

DROP TABLE IF EXISTS `association_ressource_pc`;
CREATE TABLE IF NOT EXISTS `association_ressource_pc` (
  `IDRessource` int(20) NOT NULL,
  `IDPCharacter` int(20) NOT NULL,
  `Value` int(50) NOT NULL,
  UNIQUE KEY `idRessourcePc` (`IDRessource`,`IDPCharacter`),
  KEY `id_ressource_index` (`IDRessource`),
  KEY `id_character_index` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `association_ressource_pc`
--

TRUNCATE TABLE `association_ressource_pc`;
--
-- Contenu de la table `association_ressource_pc`
--

INSERT INTO `association_ressource_pc` (`IDRessource`, `IDPCharacter`, `Value`) VALUES
(1, 1, 0),
(1, 3, 0),
(1, 4, 0),
(1, 23, 1000),
(1, 27, 0),
(1, 33, 0),
(1, 35, 9000),
(1, 36, 0),
(1, 41, 0),
(2, 1, 0),
(2, 3, 0),
(2, 4, 0),
(2, 23, 0),
(2, 27, 0),
(2, 33, 0),
(2, 36, 0),
(2, 41, 0),
(3, 1, 100),
(3, 3, 15),
(3, 4, 0),
(3, 23, 100),
(3, 27, 6),
(3, 33, 9),
(3, 36, 0),
(3, 41, 0),
(4, 1, 100),
(4, 3, 15),
(4, 4, 0),
(4, 23, 100),
(4, 27, 6),
(4, 33, 9),
(4, 36, 0),
(4, 41, 0);

-- --------------------------------------------------------

--
-- Structure de la table `association_shop_item`
--

DROP TABLE IF EXISTS `association_shop_item`;
CREATE TABLE IF NOT EXISTS `association_shop_item` (
  `IDShop` int(11) NOT NULL,
  `IDItem` int(11) NOT NULL,
  KEY `id_shop_index` (`IDShop`),
  KEY `id_item_index` (`IDShop`),
  KEY `fk_item_shop` (`IDItem`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `association_shop_item`
--

TRUNCATE TABLE `association_shop_item`;
-- --------------------------------------------------------

--
-- Structure de la table `bonus`
--

DROP TABLE IF EXISTS `bonus`;
CREATE TABLE IF NOT EXISTS `bonus` (
  `IDBonus` int(20) NOT NULL AUTO_INCREMENT,
  `BonusName` varchar(20) NOT NULL,
  PRIMARY KEY (`IDBonus`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `bonus`
--

TRUNCATE TABLE `bonus`;
--
-- Contenu de la table `bonus`
--

INSERT INTO `bonus` (`IDBonus`, `BonusName`) VALUES
(1, 'Orcus'),
(2, 'Compétence'),
(3, 'Temps'),
(4, 'IA'),
(5, 'Social'),
(6, 'Divertissement'),
(7, 'Objet'),
(8, 'Boutique');

-- --------------------------------------------------------

--
-- Structure de la table `city`
--

DROP TABLE IF EXISTS `city`;
CREATE TABLE IF NOT EXISTS `city` (
  `IDCity` int(20) NOT NULL AUTO_INCREMENT,
  `NameCity` varchar(50) NOT NULL,
  PRIMARY KEY (`IDCity`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `city`
--

TRUNCATE TABLE `city`;
--
-- Contenu de la table `city`
--

INSERT INTO `city` (`IDCity`, `NameCity`) VALUES
(1, 'Daedelus');

-- --------------------------------------------------------

--
-- Structure de la table `company`
--

DROP TABLE IF EXISTS `company`;
CREATE TABLE IF NOT EXISTS `company` (
  `IDCompany` int(20) NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(50) NOT NULL,
  `Size` int(11) NOT NULL,
  PRIMARY KEY (`IDCompany`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `company`
--

TRUNCATE TABLE `company`;
--
-- Contenu de la table `company`
--

INSERT INTO `company` (`IDCompany`, `CompanyName`, `Size`) VALUES
(1, 'Jupiter', 4),
(2, 'Saturn', 1),
(5, 'Janus', 1),
(6, 'Liber', 2),
(7, 'Ceres', 2),
(8, 'Vulcan', 2),
(9, 'Mercury', 2),
(10, 'Junno', 2),
(11, 'Appollo', 2),
(12, 'Minerva', 2);

-- --------------------------------------------------------

--
-- Structure de la table `company_specialization`
--

DROP TABLE IF EXISTS `company_specialization`;
CREATE TABLE IF NOT EXISTS `company_specialization` (
  `IDDistrict` int(11) NOT NULL,
  `IDCompany` int(11) NOT NULL,
  PRIMARY KEY (`IDDistrict`,`IDCompany`),
  KEY `fk_id_company_spec_district` (`IDCompany`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `company_specialization`
--

TRUNCATE TABLE `company_specialization`;
--
-- Contenu de la table `company_specialization`
--

INSERT INTO `company_specialization` (`IDDistrict`, `IDCompany`) VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(5, 1),
(6, 1),
(8, 1),
(5, 6),
(1, 7),
(8, 8),
(6, 9),
(3, 10),
(2, 11),
(4, 12);

-- --------------------------------------------------------

--
-- Structure de la table `diplom`
--

DROP TABLE IF EXISTS `diplom`;
CREATE TABLE IF NOT EXISTS `diplom` (
  `IDDiplom` int(11) NOT NULL AUTO_INCREMENT,
  `DiplomName` varchar(256) NOT NULL,
  PRIMARY KEY (`IDDiplom`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `diplom`
--

TRUNCATE TABLE `diplom`;
--
-- Contenu de la table `diplom`
--

INSERT INTO `diplom` (`IDDiplom`, `DiplomName`) VALUES
(1, 'DUT Option Gestion des Ressources Humaines (GRH)'),
(2, 'DUT Chimie');

-- --------------------------------------------------------

--
-- Structure de la table `diplom_pc`
--

DROP TABLE IF EXISTS `diplom_pc`;
CREATE TABLE IF NOT EXISTS `diplom_pc` (
  `IDDiplom` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  PRIMARY KEY (`IDDiplom`,`IDPCharacter`),
  KEY `fk_id_pc_diplom` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `diplom_pc`
--

TRUNCATE TABLE `diplom_pc`;
--
-- Contenu de la table `diplom_pc`
--

INSERT INTO `diplom_pc` (`IDDiplom`, `IDPCharacter`) VALUES
(1, 1),
(2, 1),
(3, 1);

-- --------------------------------------------------------

--
-- Structure de la table `district`
--

DROP TABLE IF EXISTS `district`;
CREATE TABLE IF NOT EXISTS `district` (
  `IDDistrict` int(11) NOT NULL AUTO_INCREMENT,
  `DistrictName` varchar(30) NOT NULL,
  PRIMARY KEY (`IDDistrict`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `district`
--

TRUNCATE TABLE `district`;
--
-- Contenu de la table `district`
--

INSERT INTO `district` (`IDDistrict`, `DistrictName`) VALUES
(0, 'Daedelus'),
(1, 'Administration'),
(2, 'Creatif'),
(3, 'AffaireDroit'),
(4, 'InformationCommunication'),
(5, 'Campagne'),
(6, 'Commerce'),
(8, 'Industrie');

-- --------------------------------------------------------

--
-- Structure de la table `duration`
--

DROP TABLE IF EXISTS `duration`;
CREATE TABLE IF NOT EXISTS `duration` (
  `IDDuration` int(11) NOT NULL AUTO_INCREMENT,
  `DurationName` varchar(11) NOT NULL,
  `DurationValue` int(11) NOT NULL,
  PRIMARY KEY (`IDDuration`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `duration`
--

TRUNCATE TABLE `duration`;
--
-- Contenu de la table `duration`
--

INSERT INTO `duration` (`IDDuration`, `DurationName`, `DurationValue`) VALUES
(1, 'I', 1),
(2, 'II', 2),
(3, 'III', 3),
(4, 'IV', 4),
(5, 'V', 5),
(6, 'VI', 6),
(7, 'VII', 7),
(8, 'VIII', 8),
(9, 'IX', 9),
(10, 'X', 10);

-- --------------------------------------------------------

--
-- Structure de la table `entertainment`
--

DROP TABLE IF EXISTS `entertainment`;
CREATE TABLE IF NOT EXISTS `entertainment` (
  `IDEntertainment` int(11) NOT NULL AUTO_INCREMENT,
  `EntertainmentName` varchar(20) NOT NULL,
  `IDRank` int(11) NOT NULL,
  PRIMARY KEY (`IDEntertainment`),
  KEY `fk_id_rank_entertainment` (`IDRank`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `entertainment`
--

TRUNCATE TABLE `entertainment`;
--
-- Contenu de la table `entertainment`
--

INSERT INTO `entertainment` (`IDEntertainment`, `EntertainmentName`, `IDRank`) VALUES
(1, 'Jouer à un jeu', 1),
(2, 'Sortie entre ami', 2),
(3, 'Voyage', 3),
(4, 'Séjour', 4);

-- --------------------------------------------------------

--
-- Structure de la table `entertainment_done`
--

DROP TABLE IF EXISTS `entertainment_done`;
CREATE TABLE IF NOT EXISTS `entertainment_done` (
  `IDEntertainment` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  PRIMARY KEY (`IDEntertainment`,`IDPCharacter`),
  KEY `fk_id_pc_entertainment_done` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `entertainment_done`
--

TRUNCATE TABLE `entertainment_done`;
-- --------------------------------------------------------

--
-- Structure de la table `exam`
--

DROP TABLE IF EXISTS `exam`;
CREATE TABLE IF NOT EXISTS `exam` (
  `IDExam` int(20) NOT NULL AUTO_INCREMENT,
  `ExamName` varchar(50) NOT NULL,
  `IDDiplom` int(20) NOT NULL,
  `IDRank` int(20) NOT NULL,
  `IDSkillSlot1` int(11) DEFAULT NULL,
  `IDSkillSlot2` int(11) DEFAULT NULL,
  `IDSkillSlot3` int(11) DEFAULT NULL,
  `IDSkillSlot4` int(11) DEFAULT NULL,
  `IDSkillSlot5` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDExam`),
  KEY `fk_id_skill1` (`IDSkillSlot1`),
  KEY `fk_id_skill2` (`IDSkillSlot2`),
  KEY `fk_id_skill3` (`IDSkillSlot3`),
  KEY `fk_id_skill4` (`IDSkillSlot4`),
  KEY `fk_id_skill5` (`IDSkillSlot5`),
  KEY `fk_id_diplom_exam` (`IDDiplom`),
  KEY `fk_id_rank_exam` (`IDRank`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `exam`
--

TRUNCATE TABLE `exam`;
--
-- Contenu de la table `exam`
--

INSERT INTO `exam` (`IDExam`, `ExamName`, `IDDiplom`, `IDRank`, `IDSkillSlot1`, `IDSkillSlot2`, `IDSkillSlot3`, `IDSkillSlot4`, `IDSkillSlot5`) VALUES
(1, 'DUT Informatique', 2, 2, 5, 4, 2, 1, 3),
(2, 'Exam Licence WEB', 3, 1, 5, 2, 3, 1, 4),
(3, 'Bac S', 1, 1, 5, 2, 3, 1, 4);

-- --------------------------------------------------------

--
-- Structure de la table `friend`
--

DROP TABLE IF EXISTS `friend`;
CREATE TABLE IF NOT EXISTS `friend` (
  `IDPCharacter` int(11) NOT NULL,
  `IDFriend` int(11) NOT NULL,
  PRIMARY KEY (`IDPCharacter`,`IDFriend`),
  KEY `fk_id_pc_friend` (`IDFriend`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `friend`
--

TRUNCATE TABLE `friend`;
--
-- Contenu de la table `friend`
--

INSERT INTO `friend` (`IDPCharacter`, `IDFriend`) VALUES
(35, 1),
(36, 1),
(1, 3),
(35, 3),
(1, 4),
(1, 23),
(35, 32),
(35, 37);

-- --------------------------------------------------------

--
-- Structure de la table `gain`
--

DROP TABLE IF EXISTS `gain`;
CREATE TABLE IF NOT EXISTS `gain` (
  `IDGain` int(11) NOT NULL AUTO_INCREMENT,
  `GainName` varchar(20) NOT NULL,
  PRIMARY KEY (`IDGain`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `gain`
--

TRUNCATE TABLE `gain`;
--
-- Contenu de la table `gain`
--

INSERT INTO `gain` (`IDGain`, `GainName`) VALUES
(1, 'Orcus'),
(2, 'Divertissement'),
(3, 'IA'),
(4, 'Social'),
(5, 'Objet'),
(6, 'Compétence'),
(7, 'Aptitude'),
(8, 'Diplome'),
(9, 'Artefact');

-- --------------------------------------------------------

--
-- Structure de la table `item`
--

DROP TABLE IF EXISTS `item`;
CREATE TABLE IF NOT EXISTS `item` (
  `IDItem` int(20) NOT NULL AUTO_INCREMENT,
  `ItemName` varchar(50) NOT NULL,
  `IDBonus` int(20) NOT NULL,
  `IDRank` int(11) NOT NULL,
  `BonusGain` int(20) NOT NULL,
  PRIMARY KEY (`IDItem`),
  KEY `id_bonus_index` (`IDBonus`) USING BTREE,
  KEY `fk_id_rank_item` (`IDRank`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `item`
--

TRUNCATE TABLE `item`;
--
-- Contenu de la table `item`
--

INSERT INTO `item` (`IDItem`, `ItemName`, `IDBonus`, `IDRank`, `BonusGain`) VALUES
(1, 'Bourse', 1, 1, 2),
(2, 'Encyclopédie', 2, 1, 2),
(3, 'Montre à gousset', 3, 1, 1),
(4, 'Carte de crédit', 1, 2, 4),
(5, 'Vidéo explicative', 2, 2, 4),
(6, 'Montre', 3, 2, 2),
(7, 'Porte-monnaie électronique', 1, 3, 10),
(8, 'Conférence en ligne', 2, 3, 10),
(9, 'SmartWatch', 3, 3, 5),
(10, 'Emprunt sans remboursement', 1, 4, 20),
(11, 'Spécialisation sur 6 mois', 2, 4, 20),
(12, 'OmniTech', 3, 4, 20);

-- --------------------------------------------------------

--
-- Structure de la table `item_bought`
--

DROP TABLE IF EXISTS `item_bought`;
CREATE TABLE IF NOT EXISTS `item_bought` (
  `IDItem` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  PRIMARY KEY (`IDItem`,`IDPCharacter`),
  KEY `fk_id_pc_item_bought` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `item_bought`
--

TRUNCATE TABLE `item_bought`;
-- --------------------------------------------------------

--
-- Structure de la table `item_pc`
--

DROP TABLE IF EXISTS `item_pc`;
CREATE TABLE IF NOT EXISTS `item_pc` (
  `IDItem` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  PRIMARY KEY (`IDItem`,`IDPCharacter`),
  KEY `fk_id_pc_item` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `item_pc`
--

TRUNCATE TABLE `item_pc`;
--
-- Contenu de la table `item_pc`
--

INSERT INTO `item_pc` (`IDItem`, `IDPCharacter`, `Quantity`) VALUES
(1, 1, 0),
(1, 3, 10),
(1, 4, 0),
(1, 23, 0),
(1, 27, 0),
(1, 33, 0),
(1, 36, 0),
(1, 41, 0),
(2, 1, 0),
(2, 3, 0),
(2, 4, 0),
(2, 23, 0),
(2, 27, 0),
(2, 33, 0),
(2, 36, 0),
(2, 41, 0),
(3, 1, 0),
(3, 3, 0),
(3, 4, 0),
(3, 23, 0),
(3, 27, 0),
(3, 33, 0),
(3, 36, 0),
(3, 41, 0),
(4, 1, 0),
(4, 3, 0),
(4, 4, 0),
(4, 23, 0),
(4, 27, 0),
(4, 33, 0),
(4, 36, 0),
(4, 41, 0),
(5, 1, 0),
(5, 3, 0),
(5, 4, 0),
(5, 23, 0),
(5, 27, 0),
(5, 33, 0),
(5, 36, 0),
(5, 41, 0),
(6, 1, 0),
(6, 3, 0),
(6, 4, 0),
(6, 23, 0),
(6, 27, 0),
(6, 33, 0),
(6, 36, 0),
(6, 41, 0),
(7, 1, 0),
(7, 3, 0),
(7, 4, 0),
(7, 23, 0),
(7, 27, 0),
(7, 33, 0),
(7, 36, 0),
(7, 41, 0),
(8, 1, 0),
(8, 3, 0),
(8, 4, 0),
(8, 23, 0),
(8, 27, 0),
(8, 33, 0),
(8, 36, 0),
(8, 41, 0),
(9, 1, 0),
(9, 3, 0),
(9, 4, 0),
(9, 23, 0),
(9, 27, 0),
(9, 33, 0),
(9, 36, 0),
(9, 41, 0),
(10, 1, 0),
(10, 3, 0),
(10, 4, 0),
(10, 23, 0),
(10, 27, 0),
(10, 33, 0),
(10, 36, 0),
(10, 41, 0),
(11, 1, 0),
(11, 3, 0),
(11, 4, 0),
(11, 23, 0),
(11, 27, 0),
(11, 33, 0),
(11, 36, 0),
(11, 41, 0),
(12, 1, 0),
(12, 3, 0),
(12, 4, 0),
(12, 23, 0),
(12, 27, 0),
(12, 33, 0),
(12, 36, 0),
(12, 41, 0);

-- --------------------------------------------------------

--
-- Structure de la table `job`
--

DROP TABLE IF EXISTS `job`;
CREATE TABLE IF NOT EXISTS `job` (
  `IDJob` int(11) NOT NULL AUTO_INCREMENT,
  `JobName` varchar(256) NOT NULL,
  PRIMARY KEY (`IDJob`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `job`
--

TRUNCATE TABLE `job`;
--
-- Contenu de la table `job`
--

INSERT INTO `job` (`IDJob`, `JobName`) VALUES
(1, 'Responsable de la formation'),
(2, 'Chargé de communication interne'),
(3, 'Responsable rémunérations et avantages sociaux'),
(4, 'Responsable diversité et Responsabilité Sociale et Ethique'),
(5, 'Responsable du recrutement'),
(6, 'Gestionnaire de carrières'),
(7, 'Auditeur social'),
(8, 'Directeur des Ressources Humaines');

-- --------------------------------------------------------

--
-- Structure de la table `loss`
--

DROP TABLE IF EXISTS `loss`;
CREATE TABLE IF NOT EXISTS `loss` (
  `IDLoss` int(11) NOT NULL AUTO_INCREMENT,
  `LossName` varchar(20) NOT NULL,
  PRIMARY KEY (`IDLoss`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `loss`
--

TRUNCATE TABLE `loss`;
--
-- Contenu de la table `loss`
--

INSERT INTO `loss` (`IDLoss`, `LossName`) VALUES
(1, 'Orcus'),
(2, 'Divertissement'),
(3, 'Rang'),
(4, 'Social'),
(5, 'Objet'),
(6, 'Aptitude'),
(7, 'Diplome'),
(8, 'IA');

-- --------------------------------------------------------

--
-- Structure de la table `mission`
--

DROP TABLE IF EXISTS `mission`;
CREATE TABLE IF NOT EXISTS `mission` (
  `IDMission` int(20) NOT NULL AUTO_INCREMENT,
  `MissionName` varchar(50) NOT NULL,
  `IDRank` int(20) NOT NULL,
  `IDSkill1` int(20) DEFAULT NULL,
  `IDSkill2` int(20) DEFAULT NULL,
  `IDSkill3` int(20) DEFAULT NULL,
  `IDSkill4` int(20) DEFAULT NULL,
  `IDSkill5` int(11) DEFAULT NULL,
  `AssociatedJob` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`IDMission`),
  KEY `id_rank_index` (`IDRank`),
  KEY `fk_skill4` (`IDSkill4`),
  KEY `fk_skill5` (`IDSkill5`),
  KEY `id_skill_index` (`IDSkill1`,`IDSkill2`,`IDSkill3`,`IDSkill4`,`IDSkill5`) USING BTREE,
  KEY `fk_skill3` (`IDSkill3`) USING BTREE,
  KEY `fk_skill2` (`IDSkill2`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `mission`
--

TRUNCATE TABLE `mission`;
--
-- Contenu de la table `mission`
--

INSERT INTO `mission` (`IDMission`, `MissionName`, `IDRank`, `IDSkill1`, `IDSkill2`, `IDSkill3`, `IDSkill4`, `IDSkill5`, `AssociatedJob`) VALUES
(1, 'Elaboration des bulletins de salaire', 2, 1, NULL, NULL, NULL, NULL, NULL),
(2, 'Calcul et analyse des ratios ', 3, 1, NULL, NULL, NULL, NULL, NULL),
(3, 'Analyse des écarts', 4, 1, NULL, NULL, NULL, NULL, NULL),
(4, 'Identification des fonctions recherchées', 5, 1, NULL, NULL, NULL, NULL, NULL),
(5, 'Définition des besoins ', 6, 1, NULL, NULL, NULL, NULL, NULL),
(6, 'Gestion de la formation continue', 2, 4, 6, NULL, NULL, NULL, 'Responsable de la formation'),
(7, 'Gestion de la communication interne', 2, 2, 5, NULL, NULL, NULL, 'Chargé de communication interne'),
(8, 'Gestion des rémunérations', 2, 3, 2, NULL, NULL, NULL, 'Responsable rémunérations et avantages sociaux'),
(9, 'Gestion des politiques de diversité', 2, 2, 5, NULL, NULL, NULL, 'Responsable diversité et Responsabilité Sociale et Ethique'),
(10, 'Gestion du recrutement', 2, 3, 4, NULL, NULL, NULL, 'Responsable du recrutement'),
(11, 'Gestion des carrières des salariés', 3, 2, 3, 4, 5, NULL, 'Gestionnaire de carrières'),
(12, 'Conduite d’un audit social et organisationnel', 3, 3, 4, 5, 6, NULL, 'Auditeur social'),
(13, 'Définir la stratégie des ressources humaines', 4, 2, 3, 4, 5, 6, 'Directeur des Ressources Humaines'),
(14, 'Analyse d\'une eau', 7, 1, NULL, NULL, NULL, NULL, NULL),
(15, 'Synthèse d\'un polymère', 8, 1, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Structure de la table `npc_present`
--

DROP TABLE IF EXISTS `npc_present`;
CREATE TABLE IF NOT EXISTS `npc_present` (
  `IDNPCharacter` int(11) NOT NULL,
  `IDMission` int(11) NOT NULL,
  PRIMARY KEY (`IDMission`),
  KEY `fk_id_npc_present` (`IDNPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `npc_present`
--

TRUNCATE TABLE `npc_present`;
-- --------------------------------------------------------

--
-- Structure de la table `npc_social_mission`
--

DROP TABLE IF EXISTS `npc_social_mission`;
CREATE TABLE IF NOT EXISTS `npc_social_mission` (
  `IDNPCharacter` int(20) NOT NULL,
  `IDSMission` int(20) NOT NULL,
  KEY `id_npcharacter_index` (`IDNPCharacter`),
  KEY `id_smission_index` (`IDSMission`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `npc_social_mission`
--

TRUNCATE TABLE `npc_social_mission`;
-- --------------------------------------------------------

--
-- Structure de la table `np_character`
--

DROP TABLE IF EXISTS `np_character`;
CREATE TABLE IF NOT EXISTS `np_character` (
  `IDNPCharacter` int(20) NOT NULL AUTO_INCREMENT,
  `NPCName` varchar(50) NOT NULL,
  `IDArtefact` int(11) NOT NULL,
  PRIMARY KEY (`IDNPCharacter`),
  KEY `fk_id_artefact_npc` (`IDArtefact`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `np_character`
--

TRUNCATE TABLE `np_character`;
--
-- Contenu de la table `np_character`
--

INSERT INTO `np_character` (`IDNPCharacter`, `NPCName`, `IDArtefact`) VALUES
(1, 'Crésus', 1),
(2, 'Thalia', 5),
(3, 'Polymnie', 6),
(4, 'Athènes', 3),
(5, 'Pandore', 4),
(6, 'Hermès', 2);

-- --------------------------------------------------------

--
-- Structure de la table `place`
--

DROP TABLE IF EXISTS `place`;
CREATE TABLE IF NOT EXISTS `place` (
  `IDPlace` int(11) NOT NULL AUTO_INCREMENT,
  `PlaceName` varchar(40) NOT NULL,
  PRIMARY KEY (`IDPlace`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `place`
--

TRUNCATE TABLE `place`;
--
-- Contenu de la table `place`
--

INSERT INTO `place` (`IDPlace`, `PlaceName`) VALUES
(1, 'Usine'),
(2, 'Grande Surface'),
(3, 'Hôpital '),
(4, 'Mairie'),
(5, 'Bureau avec PC'),
(6, 'Centrale énergétique'),
(7, 'Décheterie'),
(8, 'Laiterie'),
(9, 'Chimie/biologie/procédés'),
(10, 'Port');

-- --------------------------------------------------------

--
-- Structure de la table `present_missions`
--

DROP TABLE IF EXISTS `present_missions`;
CREATE TABLE IF NOT EXISTS `present_missions` (
  `IDDistrict` int(11) NOT NULL,
  `IDMission` int(11) NOT NULL,
  `IDCompany` int(11) DEFAULT NULL,
  `Date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IDDistrict`,`IDMission`),
  KEY `fk_id_mission_district` (`IDMission`),
  KEY `fk_company_mission` (`IDCompany`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `present_missions`
--

TRUNCATE TABLE `present_missions`;
-- --------------------------------------------------------

--
-- Structure de la table `present_missions_done`
--

DROP TABLE IF EXISTS `present_missions_done`;
CREATE TABLE IF NOT EXISTS `present_missions_done` (
  `IDMission` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  KEY `fk_id_mission_present` (`IDMission`),
  KEY `fk_id_mission_pc` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `present_missions_done`
--

TRUNCATE TABLE `present_missions_done`;
-- --------------------------------------------------------

--
-- Structure de la table `p_character`
--

DROP TABLE IF EXISTS `p_character`;
CREATE TABLE IF NOT EXISTS `p_character` (
  `IDPCharacter` int(11) NOT NULL AUTO_INCREMENT,
  `PCName` varchar(50) NOT NULL,
  `mail` varchar(256) NOT NULL,
  `Password` varchar(256) NOT NULL,
  `LastConnection` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `isFirstConnection` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`IDPCharacter`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `p_character`
--

TRUNCATE TABLE `p_character`;
--
-- Contenu de la table `p_character`
--

INSERT INTO `p_character` (`IDPCharacter`, `PCName`, `mail`, `Password`, `LastConnection`, `isFirstConnection`) VALUES
(1, 'nathan', 'nathan@gmail.com', '$2y$10$eeKZKGjxwrxQ.z9gbsu/Pe/1xv4MP/Ga8x1UI7IxfyPBzVZqFefwK', '2019-02-12 09:44:20', 0),
(3, 'lilith', 'lilith@gmail.com', '$2y$10$mC4GbHLU7LPf4rhH2p10ve1yLuFOfYE4eV6GdvnLKSnbPpT1zXXd6', '2018-11-28 09:13:09', 0),
(4, 'jean', 'jeanval@gmail.com', '$2y$10$aZGx9DNBbAcXyP7A7D95ReaowmDcfVrL.1G.I38tREasWn3fwVVk2', '2018-11-28 09:16:30', 0),
(23, 'david', 'davidlaroch@hotmail.fr', '$2y$10$87cW5WZ7xu6sa4ZRZJqUfeUelpJt7oQ2m1ZvIPp3uVV54WusmgcMu', '2019-02-13 10:05:29', 0),
(27, 'didier', 'didier@gmail.com', '$2y$10$I53Valdjj4W3G337XaOcS.gqErCMKbwZo/z51BxEaKX5IdKQ5O3Lu', '2018-11-28 09:15:07', 0),
(32, 'doudou', 'doudou@gmail.com', '$2y$10$1bsnsqmOkv9ZQXwm.2UUn.AiCRS.T65Ua/RGxLpPs5epWyKc4kCw2', '2018-11-28 10:04:16', 0),
(33, 'jerome', 'jerome@gmail.com', '$2y$10$XDdT84KukzbyQ1XID71pjenDg3K5U/5lKTlkIXM0xg0D/ceR97xvq', '2018-11-30 07:14:36', 1),
(34, 'jojo', 'jojo@gmail.com', '$2y$10$caa9U0fU7Q.rxi2iKEyy8.uezXh6bg/wQmhvVQNTsqzhZ6XzOtBQi', '2018-11-29 07:20:42', 1),
(35, 'jey', 'jeremy.giardina13@gmail.com', '$2y$10$.CUFz4bgjHhriK7o.ZQjs.gJ9ybJByn5e.xbs/XXYhuHkjVGq19Fu', '2018-11-30 08:39:13', 0),
(36, 'vero', 'vero@gmail.com', '$2y$10$gCbaqi7PS.Mu/VlPPo1/Z.WyKS/DXWJE2yT3A3tGpW69f.sboLV4y', '2018-11-30 17:35:40', 0),
(37, 'coucou', 'coucoulp@yopmail.com', '$2y$10$vMSQnY8t2z.QBESd4v4Cj.pC8HD03sQHYSiv2kEvmOJzz6AjbMfCi', '2019-01-03 09:49:58', 1),
(38, 'joe', 'joe@hotmail.fr', '$2y$10$NxZPdNws5PpLilgWMg8lDeZlRJVzA3kDZt0cMDvHJlSHIy5JXebsC', '2019-01-08 15:27:01', 0),
(41, 'davidlaroche', 'davidlaroche@hotmail.fr', '$2y$10$fmzvK8lOOlp3IDRYQh6ILOh0eMS7MySOcSdgY8LppDpqgS8mswT.S', '2019-01-11 10:55:15', 0),
(42, 'toto', 'toto>gmail.com', '$2y$10$RDMQgAvZs.Y5a2njnrP07OxZ3BetxmFYwSzIaaTnyL/l/W0BzzXe2', '2019-02-13 07:25:55', 0);

-- --------------------------------------------------------

--
-- Structure de la table `rank`
--

DROP TABLE IF EXISTS `rank`;
CREATE TABLE IF NOT EXISTS `rank` (
  `IDRank` int(20) NOT NULL AUTO_INCREMENT,
  `RankName` varchar(10) NOT NULL,
  PRIMARY KEY (`IDRank`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `rank`
--

TRUNCATE TABLE `rank`;
--
-- Contenu de la table `rank`
--

INSERT INTO `rank` (`IDRank`, `RankName`) VALUES
(1, 'C'),
(2, 'B'),
(3, 'A'),
(4, 'S'),
(5, 'C+'),
(6, 'B+'),
(7, 'A+'),
(8, 'S+');

-- --------------------------------------------------------

--
-- Structure de la table `ressource`
--

DROP TABLE IF EXISTS `ressource`;
CREATE TABLE IF NOT EXISTS `ressource` (
  `IDRessource` int(11) NOT NULL AUTO_INCREMENT,
  `RessourceName` varchar(50) NOT NULL,
  PRIMARY KEY (`IDRessource`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `ressource`
--

TRUNCATE TABLE `ressource`;
--
-- Contenu de la table `ressource`
--

INSERT INTO `ressource` (`IDRessource`, `RessourceName`) VALUES
(1, 'Orcus'),
(2, 'IA'),
(3, 'Social'),
(4, 'Divertissement');

-- --------------------------------------------------------

--
-- Structure de la table `shop`
--

DROP TABLE IF EXISTS `shop`;
CREATE TABLE IF NOT EXISTS `shop` (
  `IDShop` int(20) NOT NULL AUTO_INCREMENT,
  `ShopName` varchar(50) NOT NULL,
  PRIMARY KEY (`IDShop`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `shop`
--

TRUNCATE TABLE `shop`;
--
-- Contenu de la table `shop`
--

INSERT INTO `shop` (`IDShop`, `ShopName`) VALUES
(1, 'Magasin');

-- --------------------------------------------------------

--
-- Structure de la table `skill`
--

DROP TABLE IF EXISTS `skill`;
CREATE TABLE IF NOT EXISTS `skill` (
  `IDSkill` int(20) NOT NULL AUTO_INCREMENT,
  `SkillName` varchar(100) NOT NULL,
  `SkillDescription` varchar(256) NOT NULL,
  PRIMARY KEY (`IDSkill`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `skill`
--

TRUNCATE TABLE `skill`;
--
-- Contenu de la table `skill`
--

INSERT INTO `skill` (`IDSkill`, `SkillName`, `SkillDescription`) VALUES
(1, 'En attente', 'Skill nul'),
(2, 'Législation sociale', 'Maîtriser et mettre en application la législation sociale'),
(3, 'Rémunération', 'Préparer et contrôler paie et tableaux de bord sociaux'),
(4, 'Gestion préviosionnelle des emplois et des compétences (GPEC)', 'Gestion anticipative et préventive des ressources humaines'),
(5, 'Recrutement', 'Recruter et licencier'),
(6, 'Management', 'Définir une politique managériale et sociale performante'),
(7, 'Analyse Chimique', 'Procéder à des analyses chimiques quantitatives et qualitatives.'),
(8, 'Synthèse', 'Réaliser suivant un protocole la synthèse chimique d\'un composé.'),
(9, 'Qualité', 'Engager une démarche qualité.'),
(10, 'Communication', 'Se positionner dans son milieu professionnel.'),
(11, 'HSE', 'Travailler dans le respect des consignes d\'hygiène, sécurité et environnement.');

-- --------------------------------------------------------

--
-- Structure de la table `skill_job`
--

DROP TABLE IF EXISTS `skill_job`;
CREATE TABLE IF NOT EXISTS `skill_job` (
  `IDJob` int(20) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `LevelRequiered` int(20) NOT NULL,
  KEY `id_job_index` (`IDJob`),
  KEY `id_skill_index` (`IDSkill`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `skill_job`
--

TRUNCATE TABLE `skill_job`;
-- --------------------------------------------------------

--
-- Structure de la table `skill_npc`
--

DROP TABLE IF EXISTS `skill_npc`;
CREATE TABLE IF NOT EXISTS `skill_npc` (
  `IDNPCharacter` int(20) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `SkillLevel` int(20) NOT NULL,
  KEY `id_npcharacter_index` (`IDNPCharacter`),
  KEY `id_skill_index` (`IDSkill`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `skill_npc`
--

TRUNCATE TABLE `skill_npc`;
-- --------------------------------------------------------

--
-- Structure de la table `skill_pc`
--

DROP TABLE IF EXISTS `skill_pc`;
CREATE TABLE IF NOT EXISTS `skill_pc` (
  `IDSkill` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  `SkillLevel` int(11) NOT NULL,
  PRIMARY KEY (`IDSkill`,`IDPCharacter`),
  KEY `fk_id_character_skill` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `skill_pc`
--

TRUNCATE TABLE `skill_pc`;
--
-- Contenu de la table `skill_pc`
--

INSERT INTO `skill_pc` (`IDSkill`, `IDPCharacter`, `SkillLevel`) VALUES
(1, 1, 0),
(1, 3, 0),
(1, 4, 0),
(1, 23, 0),
(1, 27, 0),
(1, 33, 0),
(1, 36, 0),
(1, 41, 0),
(2, 1, 0),
(2, 3, 0),
(2, 4, 0),
(2, 23, 0),
(2, 27, 0),
(2, 33, 0),
(2, 36, 0),
(2, 41, 0),
(3, 1, 0),
(3, 3, 0),
(3, 4, 0),
(3, 23, 0),
(3, 27, 0),
(3, 33, 0),
(3, 36, 0),
(3, 41, 0),
(4, 1, 0),
(4, 3, 0),
(4, 4, 0),
(4, 23, 0),
(4, 27, 0),
(4, 33, 0),
(4, 36, 0),
(4, 41, 0),
(5, 1, 0),
(5, 3, 0),
(5, 4, 0),
(5, 23, 0),
(5, 27, 0),
(5, 33, 0),
(5, 36, 0),
(5, 41, 0);

-- --------------------------------------------------------

--
-- Structure de la table `social_mission`
--

DROP TABLE IF EXISTS `social_mission`;
CREATE TABLE IF NOT EXISTS `social_mission` (
  `IDSMission` int(20) NOT NULL AUTO_INCREMENT,
  `IDRank` int(20) NOT NULL,
  `IDItem` int(20) NOT NULL,
  `IDAptitude` int(11) NOT NULL,
  PRIMARY KEY (`IDSMission`),
  KEY `id_rank_index` (`IDRank`),
  KEY `id_item_index` (`IDItem`),
  KEY `id_aptitude_index` (`IDAptitude`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `social_mission`
--

TRUNCATE TABLE `social_mission`;
-- --------------------------------------------------------

--
-- Structure de la table `social_network`
--

DROP TABLE IF EXISTS `social_network`;
CREATE TABLE IF NOT EXISTS `social_network` (
  `IDSNetwork` int(20) NOT NULL AUTO_INCREMENT,
  `SNetworkName` varchar(50) NOT NULL,
  PRIMARY KEY (`IDSNetwork`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Vider la table avant d'insérer `social_network`
--

TRUNCATE TABLE `social_network`;
-- --------------------------------------------------------

--
-- Structure de la table `trophy`
--

DROP TABLE IF EXISTS `trophy`;
CREATE TABLE IF NOT EXISTS `trophy` (
  `IDTrophy` int(11) NOT NULL AUTO_INCREMENT,
  `IDRank` int(11) NOT NULL,
  `Description` text NOT NULL,
  PRIMARY KEY (`IDTrophy`),
  KEY `fk_id_rank_trophy` (`IDRank`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `trophy`
--

TRUNCATE TABLE `trophy`;
--
-- Contenu de la table `trophy`
--

INSERT INTO `trophy` (`IDTrophy`, `IDRank`, `Description`) VALUES
(1, 3, 'Réussir 100 missions de rang C'),
(2, 1, 'Obtenir votre premier objet');

-- --------------------------------------------------------

--
-- Structure de la table `trophy_pc`
--

DROP TABLE IF EXISTS `trophy_pc`;
CREATE TABLE IF NOT EXISTS `trophy_pc` (
  `IDTrophy` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  PRIMARY KEY (`IDTrophy`,`IDPCharacter`),
  KEY `fk_id_pc_trophy` (`IDPCharacter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Vider la table avant d'insérer `trophy_pc`
--

TRUNCATE TABLE `trophy_pc`;
--
-- Contenu de la table `trophy_pc`
--

INSERT INTO `trophy_pc` (`IDTrophy`, `IDPCharacter`) VALUES
(1, 1),
(2, 1);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `aptitude`
--
ALTER TABLE `aptitude`
  ADD CONSTRAINT `fk_id_skill` FOREIGN KEY (`IDSkill`) REFERENCES `skill` (`IDSkill`);

--
-- Contraintes pour la table `artefact`
--
ALTER TABLE `artefact`
  ADD CONSTRAINT `fk_id_bonus_artefact` FOREIGN KEY (`IDBonus`) REFERENCES `bonus` (`IDBonus`);

--
-- Contraintes pour la table `artefact_pc`
--
ALTER TABLE `artefact_pc`
  ADD CONSTRAINT `fk_id_artefact_pc` FOREIGN KEY (`IDArtefact`) REFERENCES `artefact` (`IDArtefact`),
  ADD CONSTRAINT `fk_id_pc_artefact` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `artefact_used`
--
ALTER TABLE `artefact_used`
  ADD CONSTRAINT `fk_id_artefact_used_pc` FOREIGN KEY (`IDArtefact`) REFERENCES `artefact` (`IDArtefact`),
  ADD CONSTRAINT `fk_id_pc_artefact_used` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `association_city_district`
--
ALTER TABLE `association_city_district`
  ADD CONSTRAINT `fk_id_city` FOREIGN KEY (`IDCity`) REFERENCES `city` (`IDCity`),
  ADD CONSTRAINT `fk_id_district` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`);

--
-- Contraintes pour la table `association_company_district`
--
ALTER TABLE `association_company_district`
  ADD CONSTRAINT `fk_id_company_district` FOREIGN KEY (`IDCompany`) REFERENCES `company` (`IDCompany`),
  ADD CONSTRAINT `fk_id_district_company` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`);

--
-- Contraintes pour la table `association_diploms`
--
ALTER TABLE `association_diploms`
  ADD CONSTRAINT `fk_id_diplom` FOREIGN KEY (`IDDiplom`) REFERENCES `diplom` (`IDDiplom`),
  ADD CONSTRAINT `fk_id_diplom_required` FOREIGN KEY (`IDDiplomRequiered`) REFERENCES `diplom` (`IDDiplom`);

--
-- Contraintes pour la table `association_district_entertainment`
--
ALTER TABLE `association_district_entertainment`
  ADD CONSTRAINT `fk_id_district_entertainment` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`),
  ADD CONSTRAINT `fk_id_entertainment_district` FOREIGN KEY (`IDEntertainment`) REFERENCES `entertainment` (`IDEntertainment`);

--
-- Contraintes pour la table `association_district_npc`
--
ALTER TABLE `association_district_npc`
  ADD CONSTRAINT `fk_id_district_4` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`),
  ADD CONSTRAINT `fk_id_npc` FOREIGN KEY (`IDNPCharacter`) REFERENCES `np_character` (`IDNPCharacter`);

--
-- Contraintes pour la table `association_district_place`
--
ALTER TABLE `association_district_place`
  ADD CONSTRAINT `fk_id_district_place` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`),
  ADD CONSTRAINT `fk_id_place_district` FOREIGN KEY (`IDPlace`) REFERENCES `place` (`IDPlace`);

--
-- Contraintes pour la table `association_ip_pc`
--
ALTER TABLE `association_ip_pc`
  ADD CONSTRAINT `fk_id_character` FOREIGN KEY (`playerId`) REFERENCES `p_character` (`IDPCharacter`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `association_job_character`
--
ALTER TABLE `association_job_character`
  ADD CONSTRAINT `fk_id_job` FOREIGN KEY (`IDJob`) REFERENCES `job` (`IDJob`),
  ADD CONSTRAINT `fk_id_pc` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `association_job_skill`
--
ALTER TABLE `association_job_skill`
  ADD CONSTRAINT `fk_id_job_2` FOREIGN KEY (`IDJob`) REFERENCES `job` (`IDJob`),
  ADD CONSTRAINT `fk_id_rank` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`),
  ADD CONSTRAINT `fk_id_skill_3` FOREIGN KEY (`IDSkill`) REFERENCES `skill` (`IDSkill`);

--
-- Contraintes pour la table `association_place_exam`
--
ALTER TABLE `association_place_exam`
  ADD CONSTRAINT `fk_id_exam_place` FOREIGN KEY (`IDExam`) REFERENCES `exam` (`IDExam`),
  ADD CONSTRAINT `fk_id_place_exam` FOREIGN KEY (`IDPlace`) REFERENCES `place` (`IDPlace`);

--
-- Contraintes pour la table `association_ressource_pc`
--
ALTER TABLE `association_ressource_pc`
  ADD CONSTRAINT `fk_id_character_2` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_id_ressource` FOREIGN KEY (`IDRessource`) REFERENCES `ressource` (`IDRessource`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `association_shop_item`
--
ALTER TABLE `association_shop_item`
  ADD CONSTRAINT `fk_item_shop` FOREIGN KEY (`IDItem`) REFERENCES `item` (`IDItem`),
  ADD CONSTRAINT `fk_shop_item` FOREIGN KEY (`IDShop`) REFERENCES `shop` (`IDShop`);

--
-- Contraintes pour la table `company_specialization`
--
ALTER TABLE `company_specialization`
  ADD CONSTRAINT `fk_id_company_spec_district` FOREIGN KEY (`IDCompany`) REFERENCES `company` (`IDCompany`),
  ADD CONSTRAINT `fk_id_district_company_spec` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`);

--
-- Contraintes pour la table `diplom_pc`
--
ALTER TABLE `diplom_pc`
  ADD CONSTRAINT `fk_id_diplom_pc` FOREIGN KEY (`IDDiplom`) REFERENCES `diplom` (`IDDiplom`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_id_pc_diplom` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `entertainment`
--
ALTER TABLE `entertainment`
  ADD CONSTRAINT `fk_id_rank_entertainment` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`);

--
-- Contraintes pour la table `entertainment_done`
--
ALTER TABLE `entertainment_done`
  ADD CONSTRAINT `fk_id_entertainement_pc_done` FOREIGN KEY (`IDEntertainment`) REFERENCES `entertainment` (`IDEntertainment`),
  ADD CONSTRAINT `fk_id_pc_entertainment_done` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `exam`
--
ALTER TABLE `exam`
  ADD CONSTRAINT `fk_id_diplom_exam` FOREIGN KEY (`IDDiplom`) REFERENCES `diplom` (`IDDiplom`),
  ADD CONSTRAINT `fk_id_rank_exam` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`),
  ADD CONSTRAINT `fk_id_skill1` FOREIGN KEY (`IDSkillSlot1`) REFERENCES `skill` (`IDSkill`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_id_skill2` FOREIGN KEY (`IDSkillSlot2`) REFERENCES `skill` (`IDSkill`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_id_skill3` FOREIGN KEY (`IDSkillSlot3`) REFERENCES `skill` (`IDSkill`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_id_skill4` FOREIGN KEY (`IDSkillSlot4`) REFERENCES `skill` (`IDSkill`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_id_skill5` FOREIGN KEY (`IDSkillSlot5`) REFERENCES `skill` (`IDSkill`) ON DELETE SET NULL;

--
-- Contraintes pour la table `friend`
--
ALTER TABLE `friend`
  ADD CONSTRAINT `fk_id_friend_pc` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`),
  ADD CONSTRAINT `fk_id_pc_friend` FOREIGN KEY (`IDFriend`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `item`
--
ALTER TABLE `item`
  ADD CONSTRAINT `fk_id_bonus` FOREIGN KEY (`IDBonus`) REFERENCES `bonus` (`IDBonus`),
  ADD CONSTRAINT `fk_id_rank_item` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`);

--
-- Contraintes pour la table `item_bought`
--
ALTER TABLE `item_bought`
  ADD CONSTRAINT `fk_id_item_bought` FOREIGN KEY (`IDItem`) REFERENCES `item` (`IDItem`),
  ADD CONSTRAINT `fk_id_pc_item_bought` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `item_pc`
--
ALTER TABLE `item_pc`
  ADD CONSTRAINT `fk_id_item_pc` FOREIGN KEY (`IDItem`) REFERENCES `item` (`IDItem`),
  ADD CONSTRAINT `fk_id_pc_item` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`);

--
-- Contraintes pour la table `mission`
--
ALTER TABLE `mission`
  ADD CONSTRAINT `fk_rank` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_skill1` FOREIGN KEY (`IDSkill1`) REFERENCES `skill` (`IDSkill`) ON DELETE NO ACTION,
  ADD CONSTRAINT `fk_skill2` FOREIGN KEY (`IDSkill2`) REFERENCES `skill` (`IDSkill`) ON DELETE NO ACTION,
  ADD CONSTRAINT `fk_skill3` FOREIGN KEY (`IDSkill3`) REFERENCES `skill` (`IDSkill`),
  ADD CONSTRAINT `fk_skill4` FOREIGN KEY (`IDSkill4`) REFERENCES `skill` (`IDSkill`),
  ADD CONSTRAINT `fk_skill5` FOREIGN KEY (`IDSkill5`) REFERENCES `skill` (`IDSkill`);

--
-- Contraintes pour la table `npc_present`
--
ALTER TABLE `npc_present`
  ADD CONSTRAINT `fk_id_npc_mission` FOREIGN KEY (`IDMission`) REFERENCES `mission` (`IDMission`),
  ADD CONSTRAINT `fk_id_npc_present` FOREIGN KEY (`IDNPCharacter`) REFERENCES `np_character` (`IDNPCharacter`);

--
-- Contraintes pour la table `npc_social_mission`
--
ALTER TABLE `npc_social_mission`
  ADD CONSTRAINT `fk_id_npc_smission` FOREIGN KEY (`IDNPCharacter`) REFERENCES `np_character` (`IDNPCharacter`),
  ADD CONSTRAINT `fk_id_smission_npc` FOREIGN KEY (`IDSMission`) REFERENCES `social_mission` (`IDSMission`);

--
-- Contraintes pour la table `np_character`
--
ALTER TABLE `np_character`
  ADD CONSTRAINT `fk_id_artefact_npc` FOREIGN KEY (`IDArtefact`) REFERENCES `artefact` (`IDArtefact`);

--
-- Contraintes pour la table `present_missions`
--
ALTER TABLE `present_missions`
  ADD CONSTRAINT `fk_company_mission` FOREIGN KEY (`IDCompany`) REFERENCES `company` (`IDCompany`),
  ADD CONSTRAINT `fk_id_district_mission` FOREIGN KEY (`IDDistrict`) REFERENCES `district` (`IDDistrict`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_id_mission_district` FOREIGN KEY (`IDMission`) REFERENCES `mission` (`IDMission`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `present_missions_done`
--
ALTER TABLE `present_missions_done`
  ADD CONSTRAINT `fk_id_mission_pc` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`),
  ADD CONSTRAINT `fk_id_mission_present` FOREIGN KEY (`IDMission`) REFERENCES `present_missions` (`IDMission`);

--
-- Contraintes pour la table `skill_job`
--
ALTER TABLE `skill_job`
  ADD CONSTRAINT `fk_id_job_skill` FOREIGN KEY (`IDJob`) REFERENCES `job` (`IDJob`),
  ADD CONSTRAINT `fk_id_skill_job` FOREIGN KEY (`IDSkill`) REFERENCES `skill` (`IDSkill`);

--
-- Contraintes pour la table `skill_npc`
--
ALTER TABLE `skill_npc`
  ADD CONSTRAINT `fk_id_npc_skill` FOREIGN KEY (`IDNPCharacter`) REFERENCES `np_character` (`IDNPCharacter`),
  ADD CONSTRAINT `fk_id_skill_npc` FOREIGN KEY (`IDSkill`) REFERENCES `skill` (`IDSkill`);

--
-- Contraintes pour la table `skill_pc`
--
ALTER TABLE `skill_pc`
  ADD CONSTRAINT `fk_id_character_skill` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`),
  ADD CONSTRAINT `fk_id_skill_pc` FOREIGN KEY (`IDSkill`) REFERENCES `skill` (`IDSkill`) ON DELETE CASCADE;

--
-- Contraintes pour la table `social_mission`
--
ALTER TABLE `social_mission`
  ADD CONSTRAINT `fk_aptitude_mission` FOREIGN KEY (`IDAptitude`) REFERENCES `aptitude` (`IDAptitude`),
  ADD CONSTRAINT `fk_item_mission` FOREIGN KEY (`IDItem`) REFERENCES `item` (`IDItem`),
  ADD CONSTRAINT `fk_rank_mission` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`);

--
-- Contraintes pour la table `trophy`
--
ALTER TABLE `trophy`
  ADD CONSTRAINT `fk_id_rank_trophy` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`);

--
-- Contraintes pour la table `trophy_pc`
--
ALTER TABLE `trophy_pc`
  ADD CONSTRAINT `fk_id_pc_trophy` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`),
  ADD CONSTRAINT `fk_id_trophy_pc` FOREIGN KEY (`IDTrophy`) REFERENCES `trophy` (`IDTrophy`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
