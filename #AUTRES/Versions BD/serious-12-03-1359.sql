-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Client :  localhost
-- Généré le :  Mar 12 Mars 2019 à 13:59
-- Version du serveur :  5.7.25-0ubuntu0.16.04.2
-- Version de PHP :  7.0.33-0ubuntu0.16.04.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `serious`
--
CREATE DATABASE IF NOT EXISTS `serious` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `serious`;

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
-- Contenu de la table `association_district_place`
--

INSERT INTO `association_district_place` (`IDDistrict`, `IDPlace`) VALUES
(8, 1),
(8, 2),
(8, 3),
(8, 4),
(6, 5),
(6, 6),
(1, 7),
(1, 8),
(1, 9),
(3, 10),
(3, 11),
(4, 12),
(4, 13),
(4, 14),
(4, 15),
(2, 16),
(2, 17),
(2, 18),
(5, 19),
(5, 20),
(5, 21),
(5, 22),
(7, 23),
(7, 24),
(7, 25),
(7, 26),
(7, 27);

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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `diplom`
--

INSERT INTO `diplom` (`IDDiplom`, `DiplomName`) VALUES
(1, 'DUT Option Gestion des Ressources Humaines (GRH)'),
(2, 'DUT Chimie'),
(3, 'DUT Gestion Urbaine'),
(4, 'DUT GACO'),
(5, 'DUT GB'),
(6, 'DUT GCGP'),
(7, 'DUT Option Gestion Compatble et Financière (GCF)'),
(8, 'DUT GEII'),
(9, 'DUT Informatique'),
(10, 'DUT Métiers du livre'),
(11, 'DUT MMI'),
(12, 'DUT MP'),
(13, 'DUT R&T'),
(14, 'DUT Hygiène Sécurité Environnement'),
(16, 'LP MTACB'),
(17, 'LP CAPC');

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
(7, 'Campus'),
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
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `exam`
--

INSERT INTO `exam` (`IDExam`, `ExamName`, `IDDiplom`, `IDRank`, `IDSkillSlot1`, `IDSkillSlot2`, `IDSkillSlot3`, `IDSkillSlot4`, `IDSkillSlot5`) VALUES
(1, 'DUT Option Gestion des Ressources Humaines (GRH)', 1, 4, 2, 3, 4, 5, 6),
(2, 'DUT Chimie', 2, 4, 7, 8, 9, 10, 11),
(3, 'DUT Gestion Urbaine', 3, 4, 12, 13, 14, 15, 16),
(4, 'DUT GACO', 4, 4, 17, 18, 19, 20, 21),
(5, 'DUT GB', 5, 4, 22, 23, 24, 25, 26),
(6, 'DUT GCGP', 6, 4, 27, 28, 29, 30, 31),
(7, 'DUT Option Gestion Compatble et Financière (GCF)', 7, 4, 32, 33, 34, 35, 36),
(8, 'DUT GEII', 8, 4, 37, 38, 39, 40, 41),
(9, 'DUT Informatique', 9, 4, 42, 43, 44, 45, 46),
(10, 'DUT Métiers du livre', 10, 4, 47, 48, 49, 50, 51),
(11, 'DUT MMI', 11, 4, 52, 53, 54, 55, 56),
(12, 'DUT MP', 12, 4, 57, 58, 59, 60, 61),
(13, 'DUT R&T', 13, 4, 62, 63, 64, 65, 66),
(14, 'DUT Hygiène Sécurité Environnement', 14, 4, 67, 68, 69, 70, 71),
(15, 'LP CAPC', 17, 4, 82, 83, 84, 85, 86),
(16, 'LP MTACB', 16, 4, 77, 78, 79, 80, 81);

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
  `MissionName` varchar(256) NOT NULL,
  `IDRank` int(20) NOT NULL,
  `IDSkill1` int(20) DEFAULT NULL,
  `IDSkill2` int(20) DEFAULT NULL,
  `IDSkill3` int(20) DEFAULT NULL,
  `IDSkill4` int(20) DEFAULT NULL,
  `IDSkill5` int(11) DEFAULT NULL,
  `AssociatedJob` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IDMission`),
  KEY `id_rank_index` (`IDRank`),
  KEY `fk_skill4` (`IDSkill4`),
  KEY `fk_skill5` (`IDSkill5`),
  KEY `id_skill_index` (`IDSkill1`,`IDSkill2`,`IDSkill3`,`IDSkill4`,`IDSkill5`) USING BTREE,
  KEY `fk_skill3` (`IDSkill3`) USING BTREE,
  KEY `fk_skill2` (`IDSkill2`)
) ENGINE=InnoDB AUTO_INCREMENT=229 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `mission`
--

INSERT INTO `mission` (`IDMission`, `MissionName`, `IDRank`, `IDSkill1`, `IDSkill2`, `IDSkill3`, `IDSkill4`, `IDSkill5`, `AssociatedJob`) VALUES
(1, 'Elaboration des bulletins de salaire', 1, 2, NULL, NULL, NULL, NULL, NULL),
(2, 'Calcul et analyse des ratios ', 1, 3, NULL, NULL, NULL, NULL, NULL),
(3, 'Analyse des écarts', 1, 4, NULL, NULL, NULL, NULL, NULL),
(4, 'Identification des fonctions recherchées', 1, 5, NULL, NULL, NULL, NULL, NULL),
(5, 'Définition des besoins ', 1, 6, NULL, NULL, NULL, NULL, NULL),
(6, 'Gestion de la formation continue', 2, 4, 6, NULL, NULL, NULL, 'Responsable de la formation'),
(7, 'Gestion de la communication interne', 2, 2, 5, NULL, NULL, NULL, 'Chargé de communication interne'),
(8, 'Gestion des rémunérations', 2, 3, 2, NULL, NULL, NULL, 'Responsable rémunérations et avantages sociaux'),
(9, 'Gestion des politiques de diversité', 2, 2, 5, NULL, NULL, NULL, 'Responsable diversité et Responsabilité Sociale et Ethique'),
(10, 'Gestion du recrutement', 2, 3, 4, NULL, NULL, NULL, 'Responsable du recrutement'),
(11, 'Gestion des carrières des salariés', 3, 2, 3, 4, 5, NULL, 'Gestionnaire de carrières'),
(12, 'Conduite d’un audit social et organisationnel', 3, 3, 4, 5, 6, NULL, 'Auditeur social'),
(13, 'Définir la stratégie des ressources humaines', 4, 2, 3, 4, 5, 6, 'Directeur des Ressources Humaines'),
(14, 'Assister le responsable de l\'entreprise', 1, 17, NULL, NULL, NULL, NULL, NULL),
(15, 'Assurer la comptabilité de l\'entreprise', 1, 18, NULL, NULL, NULL, NULL, NULL),
(16, 'Vendre les produits', 1, 19, NULL, NULL, NULL, NULL, NULL),
(17, 'Organiser des évènements', 1, 20, NULL, NULL, NULL, NULL, NULL),
(18, 'Gérer la paye des employés', 1, 21, NULL, NULL, NULL, NULL, NULL),
(19, 'Evaluer la performance financière d\'une entreprise', 2, 17, 18, NULL, NULL, NULL, 'Analyste financier'),
(20, 'Prendre en charge la stratégie marketing de l\'entreprise', 2, 17, 19, NULL, NULL, NULL, 'Directeur commercial'),
(21, 'Définir la stratégie de communication', 2, 17, 20, NULL, NULL, NULL, 'Chargé de communication'),
(22, 'Gestion des perssonnels', 2, 17, 21, NULL, NULL, NULL, 'Directeur des ressources humaines'),
(23, 'Définir la communication produit', 2, 19, 20, NULL, NULL, NULL, 'Responsable publicité'),
(24, 'Assister le responsable de l\'entreprise', 3, 18, 19, 20, 21, NULL, 'Directeur adjoint'),
(25, 'Administrer l\'entreprise', 3, 18, 19, 20, 21, NULL, 'Secrétaire général'),
(26, 'Direction d\'une entreprise', 4, 17, 18, 19, 20, 21, 'Chef d\'entreprise'),
(27, 'Analyse d\'une eau', 1, 7, NULL, NULL, NULL, NULL, NULL),
(28, 'Synthèse d\'un polymère', 1, 8, NULL, NULL, NULL, NULL, NULL),
(29, 'Délivrance d\'une certification', 1, 9, NULL, NULL, NULL, NULL, NULL),
(30, 'Organisation d\'une réunion de service', 1, 10, NULL, NULL, NULL, NULL, NULL),
(31, 'Réalisation d\'un plan de gestion des déchets', 1, 11, NULL, NULL, NULL, NULL, NULL),
(32, 'Synthèse et caractérisation d\'une molécule', 2, 7, 8, NULL, NULL, NULL, 'Technicien en synthèse chimique'),
(33, 'Assurance qualité des mesures et la conformité des résultats', 2, 7, 9, NULL, NULL, NULL, 'Technicien en contrôle qualité'),
(34, 'Rédaction d\'un cahier de laboratoire', 2, 7, 10, NULL, NULL, NULL, 'Technicien de laboratoire'),
(35, 'Adaptation des protocoles en fonction de la réglementation', 2, 8, 11, NULL, NULL, NULL, 'Technicien recherche et développement'),
(36, 'Suivi réglementaire', 2, 9, 11, NULL, NULL, NULL, 'Responsable qualité'),
(37, 'Développement de méthodes d\'analyse', 3, 7, 8, 9, 10, NULL, 'Responsable R&D'),
(38, 'Gestion d\'équipe de techniciens', 3, 8, 9, 10, 11, NULL, 'Chef du pôle analyse'),
(39, 'Gestion d\'un laboratoire', 4, 7, 8, 9, 10, 11, 'Chef de laboratoire'),
(40, 'Analyser l\'organisation d\'un espace urbain', 1, 12, NULL, NULL, NULL, NULL, NULL),
(41, 'Trouver une nouvelle vocation à un espace urbain', 1, 13, NULL, NULL, NULL, NULL, NULL),
(42, 'Identifier les acteurs et les politiques', 1, 14, NULL, NULL, NULL, NULL, NULL),
(43, 'Mettre en place des réunions de quartier', 1, 15, NULL, NULL, NULL, NULL, NULL),
(44, 'Créer une enquête de satisfaction ', 1, 16, NULL, NULL, NULL, NULL, NULL),
(45, 'Mener un projet de réhabilitation ', 2, 12, 13, NULL, NULL, NULL, 'Agent de développement territorial'),
(46, 'Accompagner un projet culturelle', 2, 13, 14, NULL, NULL, NULL, 'Coordinateur culturel'),
(47, 'Organiser la concertation d\'un public en amont d\'un projet de développement urbain', 2, 14, 15, NULL, NULL, NULL, 'A RENSEIGNER'),
(48, 'Participer à la mise en place d\'un projet DD et à son évaluation', 2, 14, 16, NULL, NULL, NULL, 'Coordinateur des politiques environnementales'),
(49, 'Organiser la pietonnisation d\'un quartier de CV', 3, 13, 14, 15, 16, NULL, 'Coordinateur de projets urbains'),
(50, 'Mettre en place un jardin partagé dans un quartier sensible', 3, 13, 14, 15, 16, NULL, 'Coordinateur de projets urbains'),
(51, 'Piloter un projet de DD', 4, 12, 13, 14, 15, 16, 'Chef de projet'),
(52, 'Calculer la fertilisation des sols', 1, 22, NULL, NULL, NULL, NULL, NULL),
(53, 'Calculer les rations alimentaires des animaux', 1, 23, NULL, NULL, NULL, NULL, NULL),
(54, 'Contrôler la charte qualité', 1, 24, NULL, NULL, NULL, NULL, NULL),
(55, 'Analyser les paramètres du sol', 1, 25, NULL, NULL, NULL, NULL, NULL),
(56, 'Conseiller le producteur à modifier ses pratiques', 1, 26, NULL, NULL, NULL, NULL, NULL),
(57, 'Produire en autorecyclage', 2, 22, 23, NULL, NULL, NULL, 'Producteur polyculture élevage'),
(58, 'Analyser les données sol et conseiller le producteur', 2, 22, 25, NULL, NULL, NULL, 'Conseiller technique agricole'),
(59, 'Analyser les données d\'élevage et conseiller le producteur', 2, 23, 24, NULL, NULL, NULL, 'Conseiller technique agricole'),
(60, 'Contrôler la qualité par rapport au cahier des charges', 2, 24, 25, NULL, NULL, NULL, 'Technicien qualité'),
(61, 'Gestion d\'exploitation', 3, 23, 24, 25, 26, NULL, 'Technicien polyculture élevage '),
(62, 'Gestion d\'exploitation végétale', 3, 23, 24, 25, 26, NULL, 'Technicien conseil agronome'),
(63, 'Gestion d\'exploitation raisonnée', 4, 22, 23, 24, 25, 26, 'Ingénieur agronome'),
(64, 'Création de schémas d\'installation industrielle', 1, 27, NULL, NULL, NULL, NULL, NULL),
(65, 'Vérification du bon fonctionnement d\'une unité de purification industrielle', 1, 28, NULL, NULL, NULL, NULL, NULL),
(66, 'Analyses physico-chimiques du produit fini', 1, 29, NULL, NULL, NULL, NULL, NULL),
(67, 'Analyse de risques liés aux installations industrielles', 1, 30, NULL, NULL, NULL, NULL, NULL),
(68, 'Management d\'équipes', 1, 31, NULL, NULL, NULL, NULL, NULL),
(69, 'Suivi opérationnel de l\'installation de production', 2, 27, 28, NULL, NULL, NULL, 'Opérateur'),
(70, 'Analyse du produit fini', 2, 27, 29, NULL, NULL, NULL, 'Préleveur d\'échantillons'),
(71, 'Mise en sécurité des installations de production', 2, 27, 30, NULL, NULL, NULL, 'Animateur sécurité'),
(72, 'Amélioration d\'un procédé industriel de purification', 2, 28, 31, NULL, NULL, NULL, 'Technicien bureau d\'études'),
(73, 'Mise aux normes des installations industrielles', 2, 29, 31, NULL, NULL, NULL, 'Technicien qualité'),
(74, 'Suivi à distance du bon fonctionnement de l\'installation de production.', 3, 27, 28, 29, 30, NULL, 'Tableauteur'),
(75, 'Mise aux normes environnenmentales des installations', 3, 28, 29, 30, 31, NULL, 'Technicien environnement'),
(76, 'Management d\'une équipe de production industrielle', 4, 27, 28, 29, 30, 31, 'Chef de quart'),
(77, 'Mise en place d\'un compte de résultat', 1, 32, NULL, NULL, NULL, NULL, NULL),
(78, 'Calcul et Analyse des ratios ', 1, 33, NULL, NULL, NULL, NULL, NULL),
(79, 'Analyse des écarts', 1, 34, NULL, NULL, NULL, NULL, NULL),
(80, 'Calcul des coûts et des marges d\'une société', 1, 35, NULL, NULL, NULL, NULL, NULL),
(81, 'Calcul d\'un seuil de rentabilité', 1, 36, NULL, NULL, NULL, NULL, NULL),
(82, 'Analyse des soldes intermédiaires de gestion', 2, 32, 33, NULL, NULL, NULL, 'Assistant Expert comptable'),
(83, 'Calcul des écarts budgétaires', 2, 32, 34, NULL, NULL, NULL, 'Assistant Contrôleur de gestion'),
(84, 'Calcul de la rentabilité des produits, prestations, clients', 2, 32, 35, NULL, NULL, NULL, 'Manager d\'affaire'),
(85, 'Mise en place d\'un Business Plan ', 2, 32, 36, NULL, NULL, NULL, 'Chargé de gestion'),
(86, 'Elaboration d\'un plan de financement ', 2, 33, 34, NULL, NULL, NULL, 'Assistant Expert comptable'),
(87, 'Elaboration d\'un prévisionnel budgétaire d\'une société', 3, 32, 33, 34, 35, NULL, 'Contrôleur de gestion'),
(88, 'Elaboration d\'un Business Plan d\'une société ', 3, 33, 34, 35, 36, NULL, 'Directeur financier'),
(89, 'Audit financier d\'une entreprise', 4, 32, 33, 34, 35, 36, 'Commissaire aux comptes'),
(90, 'Insatallation d\'un équipement électronique', 1, 37, NULL, NULL, NULL, NULL, NULL),
(91, 'Diagnostic de panne', 1, 38, NULL, NULL, NULL, NULL, NULL),
(92, 'Dimensionnement d\'une installation', 1, 39, NULL, NULL, NULL, NULL, NULL),
(93, 'Sécuriser une installation', 1, 40, NULL, NULL, NULL, NULL, NULL),
(94, 'Automatiser une chaîne industrielle', 1, 41, NULL, NULL, NULL, NULL, NULL),
(95, 'Diagnostiquer et réparer une panne', 2, 37, 38, NULL, NULL, NULL, 'Technicien supérieur maintenance des systèmes'),
(96, 'Dimensionnement et entretien d\'une installation de production électrique', 2, 37, 39, NULL, NULL, NULL, 'Electrotechnicien'),
(97, 'Automatiser un système', 2, 37, 41, NULL, NULL, NULL, 'Automaticien'),
(98, 'Travailler sur des armoires électriques', 2, 38, 40, NULL, NULL, NULL, 'Electricien'),
(99, 'Sécuriser une installation avant diagnostic', 2, 39, 40, NULL, NULL, NULL, 'Technicien d\'installation et de maintenance'),
(100, 'Maintenance d\'une insallation électrique', 3, 37, 38, 39, 40, NULL, 'Technicien supérieur d\'installation et de maintenance'),
(101, 'Conduction de ligne de produtcion ', 3, 38, 39, 40, 41, NULL, 'Responsable de production de matériel électrique et électronique'),
(102, 'Validation des systèmes avioniques', 4, 37, 38, 39, 40, 41, 'Ingénieur aéronotique'),
(103, 'Programmation d\'un algorithme', 1, 42, NULL, NULL, NULL, NULL, NULL),
(104, 'Mise en place d\'un CMS', 1, 43, NULL, NULL, NULL, NULL, NULL),
(105, 'Envoi de requêtes SQL', 1, 44, NULL, NULL, NULL, NULL, NULL),
(106, 'Analyse logicielle', 1, 45, NULL, NULL, NULL, NULL, NULL),
(107, 'Création d\'un script shell', 1, 46, NULL, NULL, NULL, NULL, NULL),
(108, 'Développement front end', 2, 42, 43, NULL, NULL, NULL, 'Développeur front end'),
(109, 'Développement back end', 2, 42, 44, NULL, NULL, NULL, 'Développeur back end'),
(110, 'Conception applicative', 2, 42, 45, NULL, NULL, NULL, 'Architecte logiciel'),
(111, 'Création d\'un site Web', 2, 43, 44, NULL, NULL, NULL, 'Webmestre '),
(112, 'Gestion d\'un système d\'information', 2, 44, 45, NULL, NULL, NULL, 'Spécialiste bases de données'),
(113, 'Développement full stack', 3, 42, 43, 44, 45, NULL, 'Développeur full stack'),
(114, 'Mise en place d\'un parc informatique', 3, 43, 44, 45, 46, NULL, 'Administrateur systèmes, réseaux et bases de données'),
(115, 'Développement d\'une plate-forme logicielle', 4, 42, 43, 44, 45, 46, 'DevOps'),
(116, 'Lecture-correction d\'un manuscrit', 1, 47, NULL, NULL, NULL, NULL, NULL),
(117, 'Mise en page d\'un ouvrage', 1, 48, NULL, NULL, NULL, NULL, NULL),
(118, 'Conseil d\'un choix de livres', 1, 49, NULL, NULL, NULL, NULL, NULL),
(119, 'Etude des pratiques culturelles', 1, 50, NULL, NULL, NULL, NULL, NULL),
(120, 'Organisation d\'un événement culturel', 1, 51, NULL, NULL, NULL, NULL, NULL),
(121, 'Création d\'une maquette d\'un ouvrage', 2, 47, 48, NULL, NULL, NULL, 'Maquettiste'),
(122, 'Réalisation de plans de communication', 2, 47, 50, NULL, NULL, NULL, 'Attaché de presse'),
(123, 'Coordination de la fabrication d\'un livre', 2, 47, 51, NULL, NULL, NULL, 'Secrétaire d\'édition'),
(124, 'Merchandising en produits culturels', 2, 49, 50, NULL, NULL, NULL, 'Libraire'),
(125, 'Gestion d\'une bibliothèque', 3, 48, 49, 50, 51, NULL, 'Conservateur des bibliothèques'),
(126, 'Gestion d\'une librairie', 3, 48, 49, 50, 51, NULL, 'Responsable de librairie'),
(127, 'Gestion d\'une maison d\'édition', 4, 47, 48, 49, 50, 51, 'Editeur'),
(128, 'Tourner et monter un film ', 1, 52, NULL, NULL, NULL, NULL, NULL),
(129, 'Créer un site web institutionnel ', 1, 53, NULL, NULL, NULL, NULL, NULL),
(130, 'Gérer les réseaux sociaux de la mairie ', 1, 54, NULL, NULL, NULL, NULL, NULL),
(131, 'Concevoir une page d\'accueil ergonomique ', 1, 55, NULL, NULL, NULL, NULL, NULL),
(132, 'Gérer l\'application "photos" d\'un commerçant', 1, 56, NULL, NULL, NULL, NULL, NULL),
(133, 'Créer des GIF pour une application de discussion instantanée ', 2, 52, 53, NULL, NULL, NULL, 'Graphiste'),
(134, 'Créer une galerie photo/vidéo dans un site web déjà existant ', 2, 52, 56, NULL, NULL, NULL, 'Intégrateur web / technicien audio-visuel'),
(135, 'Concevoir et implanter le site web d\'un festival de musique', 2, 53, 55, NULL, NULL, NULL, 'Intégrateur web / web-designer'),
(136, 'Créer et rédiger une newsletter ', 2, 54, 55, NULL, NULL, NULL, 'Community manager'),
(137, 'Structurer un site web ergonomique et interactif', 3, 53, 54, 55, 56, NULL, 'Web-designer'),
(138, 'Concevoir une communication numérique', 3, 53, 54, 55, 56, NULL, 'Chargé de communication numérique'),
(139, 'Créer la stratégie de communication numérique intégrale d\'une entreprise ', 4, 52, 53, 54, 55, 56, 'Futur Responsable de communication numérique'),
(140, 'Gestion d’un parc d’instrument', 1, 57, NULL, NULL, NULL, NULL, NULL),
(141, 'Analyse et mise en œuvre d’un bus d’instrumentation', 1, 58, NULL, NULL, NULL, NULL, NULL),
(142, 'Comparer les polymères biodégradables', 1, 59, NULL, NULL, NULL, NULL, NULL),
(143, 'Démodulation d’une onde radio AM', 1, 60, NULL, NULL, NULL, NULL, NULL),
(144, 'Optimisation d’une pompe à chaleur', 1, 61, NULL, NULL, NULL, NULL, NULL),
(145, 'Test en traction d\'éprouvettes en acier', 2, 57, 59, NULL, NULL, NULL, 'Technicien métallurgiste'),
(146, 'Réponse en fréquence d’un filtre passe-bande', 2, 57, 60, NULL, NULL, NULL, 'Technicien ondes-radio'),
(147, 'Programmation d’une pompe à chaleur', 2, 58, 61, NULL, NULL, NULL, 'Chauffagiste'),
(148, 'Isolation d’un batiment', 2, 59, 61, NULL, NULL, NULL, 'Technicien génie civil'),
(149, 'Dimensionnement panneaux solaires', 3, 57, 58, 59, 60, NULL, 'Opérateur fabrication de panneaux photovoltaïques'),
(150, 'Labellisation Maison passive', 3, 58, 59, 60, 61, NULL, 'Expert domotique'),
(151, 'Réalisation d’une salle à hygrométrie contrôlée pour un musée', 4, 57, 58, 59, 60, 61, 'Coordinateur de chantier'),
(152, 'Câblage', 1, 62, NULL, NULL, NULL, NULL, NULL),
(153, 'Installation d\'un système', 1, 63, NULL, NULL, NULL, NULL, NULL),
(154, 'Programmation ', 1, 64, NULL, NULL, NULL, NULL, NULL),
(155, 'Configuration d\'un PABX', 1, 65, NULL, NULL, NULL, NULL, NULL),
(156, 'Réfléctométrie ', 1, 66, NULL, NULL, NULL, NULL, NULL),
(157, 'Mettre en place un réseau informatique et les services associés', 2, 62, 63, NULL, NULL, NULL, 'Administrateur réseaux et systèmes '),
(158, 'Dévellopement d\'une application client serveur', 2, 62, 64, NULL, NULL, NULL, 'Développeur d\'applications'),
(159, 'Mettre en place un réseau téléphonique et les services associés', 2, 62, 65, NULL, NULL, NULL, ''),
(160, 'Webmaster', 2, 63, 64, NULL, NULL, NULL, 'Webmaster'),
(161, 'Mise en place d\'un système de communication pour une entreprise', 3, 62, 63, 64, 65, NULL, 'Téchnicien réseaux et télécom'),
(162, 'Mise en place d\'un système de communication pour une entreprise', 3, 63, 64, 65, 66, NULL, 'Téchnicien réseaux et télécom'),
(163, 'Mise en place d\'un système de communication pour une entreprise', 3, 63, 64, 65, 66, NULL, 'Téchnicien réseaux et télécom'),
(164, 'Mise en place d\'un système de communication pour une entreprise', 3, 63, 64, 65, 66, NULL, 'Téchnicien réseaux et télécom'),
(165, 'Mise en place d\'un système de communication pour une entreprise', 3, 63, 64, 65, 66, NULL, 'Téchnicien en télécom'),
(166, 'Mise en place d\'un parc informatique', 4, 62, 63, 64, 65, 66, 'Technicien réseaux'),
(167, 'Elaborer le document unique d\'évaluation des risques', 1, 67, NULL, NULL, NULL, NULL, NULL),
(168, 'Mesurer les facteurs d\'ambiance (Temperature, bruit, éclairage, rayonnement)', 1, 68, NULL, NULL, NULL, NULL, NULL),
(169, 'Concevoir et mettre à jour les documents réglementaires', 1, 69, NULL, NULL, NULL, NULL, NULL),
(170, 'Identifier et mobiliser les différents acteurs de la santé au travail, de la protection de l\'environnement et des populations', 1, 70, NULL, NULL, NULL, NULL, NULL),
(171, 'Maîtriser les moyens techniques, humains et organisationnels des secours au sein d\'un organisme', 1, 71, NULL, NULL, NULL, NULL, NULL),
(172, 'Faire l\'étude d\'un poste de travail', 2, 67, 68, NULL, NULL, NULL, 'Préventeur HSE'),
(173, 'Auditer un sytème de management de la sécurité', 2, 67, 69, NULL, NULL, NULL, 'Auditeur sécurité'),
(174, 'Animer la mission de santé, sécurité au sein d\'une administration ou d\'une entreprise ', 2, 67, 70, NULL, NULL, NULL, 'Animateur en santé,sécurité du travail'),
(175, 'Proposer des solutions de protection pour les travailleurs exposés aux risques NRBC', 2, 68, 69, NULL, NULL, NULL, 'Technicien en risques Nucléaire, Radiologique, Bactériologique, Chimique (NRBC)'),
(176, 'Mettre en place les moyens techniques, humains et organisationnels des secours', 2, 69, 71, NULL, NULL, NULL, 'Officier Sapeur Pompier'),
(177, 'Organiser et faire respecter les règles de sécutité sur les chantiers BTP', 3, 67, 68, 69, 70, NULL, 'Coordonnatrice Sécurité, Protection de la Santé du BTP'),
(178, 'Coordonner les opérations en situation de crise', 3, 68, 69, 70, 71, NULL, 'Officier Sapeur Pompier'),
(179, 'Pilotage du sevice de prévention des risques professionnels', 4, 67, 68, 69, 70, 71, 'responsable du service prévention '),
(193, 'Analyse  d\'un échantillon d\'eau', 1, 77, NULL, NULL, NULL, NULL, NULL),
(194, 'Analyse d\'un prélèvement alimentaire', 1, 78, NULL, NULL, NULL, NULL, NULL),
(195, 'Gestion de la mise en œuvre d\'un projet', 1, 79, NULL, NULL, NULL, NULL, NULL),
(196, 'Direction d\'une réunion de service', 1, 80, NULL, NULL, NULL, NULL, NULL),
(197, 'Assurance de la conformité d\'un résultat', 1, 81, NULL, NULL, NULL, NULL, NULL),
(198, 'Réalisation des analyses chimiques et biologiques', 2, 77, 78, NULL, NULL, NULL, 'Technicien analyste'),
(199, 'Développement de méthodes d\'analyses', 2, 77, 79, NULL, NULL, NULL, 'Assistant recherche et développement'),
(200, 'Réalisation d\'analyses biologiques', 2, 78, 79, NULL, NULL, NULL, 'Technicien principal de laboratoire'),
(201, 'Suivi des taux d\'expositions des salariés aux germes pathogènes', 2, 78, 81, NULL, NULL, NULL, 'Technicien hygiène industrielle'),
(202, 'Gestion d\'équipes d\'analystes', 3, 78, 79, 80, 81, NULL, 'Chef du service analyses'),
(203, 'Développement de projets', 3, 78, 79, 80, 81, NULL, 'Assistant Ingénieur d\'études'),
(204, 'Gestion du quotidien d\'un labo', 4, 77, 78, 79, 80, 81, 'Chef de laboratoire'),
(217, 'Suivi du fonctionnement d\'une production industrielle', 1, 82, NULL, NULL, NULL, NULL, NULL),
(218, 'Suivi des produits formés au cours de la production', 1, 83, NULL, NULL, NULL, NULL, NULL),
(219, 'Vérification de la conformité des produits finis', 1, 84, NULL, NULL, NULL, NULL, NULL),
(220, 'Analyse des risques chimiques liés aux installations industrielles', 1, 85, NULL, NULL, NULL, NULL, NULL),
(221, 'Identification de problèmes', 1, 86, NULL, NULL, NULL, NULL, NULL),
(222, 'Suivi du bon fonctionnement de l\'installation de production', 2, 82, 85, NULL, NULL, NULL, 'Tableauteur'),
(223, 'Analyse du produit fini et rédaction de rapports', 2, 83, 84, NULL, NULL, NULL, 'Technicien de laboratoire'),
(224, 'Amélioration d\'une unité de production industrielle', 2, 83, 86, NULL, NULL, NULL, 'Technicien bureau d\'études'),
(225, 'Mise aux normes des installations industrielles', 2, 84, 86, NULL, NULL, NULL, 'Technicien Qualité'),
(226, 'Management d\'une équipe de production', 3, 82, 83, 84, 85, NULL, 'Chef de quart'),
(227, 'Gestion de projets', 3, 83, 84, 85, 86, NULL, 'Assistant technique de fabrication'),
(228, 'Aide à la mise en place d\'unité de production', 4, 82, 83, 84, 85, 86, 'Assistant ingénieur');

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
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4;

--
-- Contenu de la table `place`
--

INSERT INTO `place` (`IDPlace`, `PlaceName`) VALUES
(1, 'Usine'),
(2, 'Port'),
(3, 'Centrale énergétique'),
(4, 'Aéroport'),
(5, 'Grande surface'),
(6, 'Pharmacie'),
(7, 'Mairie'),
(8, 'Déchèterie'),
(9, 'Hôpital'),
(10, 'Banque'),
(11, 'Bureau d\'études'),
(12, 'Bureau avec PC'),
(13, 'Salle de réunion'),
(14, 'Bureau de publicité'),
(15, 'Agence web'),
(16, 'Studio de jeu vidéo'),
(17, 'Studio d\'enregistrement'),
(18, 'Bureau de conception créative'),
(19, 'Laiterie'),
(20, 'Commerce de proximité'),
(21, 'Barrage'),
(22, 'Ferme'),
(23, 'Gestion/Commerce/Marketing'),
(24, 'Electronique/Informatique/Mécanique'),
(25, 'Information/Communication/Multimédia'),
(26, 'Sécurité/Aménagement/Energie'),
(27, 'Chimie/Biologie/Procédés');

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
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=latin1;

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
(11, 'HSE', 'Travailler dans le respect des consignes d\'hygiène, sécurité et environnement.'),
(12, 'Diagnostic territorial', 'Elaborer un diagnostic territorial'),
(13, 'Développement territorial', 'Préconiser des actions de développement territorial'),
(14, 'Développement local', 'Accompagner un projet de développement local'),
(15, 'Publics', 'Sensibiliser et impliquer les publics'),
(16, 'Développement durable', 'Evaluer les projets et les politiques de développement durable'),
(17, 'Gestion d\'une petite et moyenne entreprise', 'Créer, reprendre ou gérer un entreprise'),
(18, 'Analyse financière ', 'Effectuer des opérations comptables et comprendre les indicateurs'),
(19, 'Commercialisation et marketing d\'un produit', 'Valoriser, vendre un produit'),
(20, 'Communication entreprise', 'Créer des outils de communication'),
(21, 'Droit et ressources humaines', 'Manager et gérer les ressources humaines'),
(22, 'Gestion d\'une production végétale', 'Calculer les paramètres de production et les optimiser.'),
(23, 'Gestion d\'une production animale', 'Gérer les paramètres de la nutrition et de la reproduction.'),
(24, 'Transformation des produits agricoles', 'Contrôler la qualité agroalimentaire des production.'),
(25, 'Appréciation de la qualité d\'un sol', 'Analyser chimique et biologique du sol.'),
(26, 'Optimisation agroécologique de la production', 'Modifier les pratiques acgricoles pour entrer dans un cadre durable '),
(27, 'Technologie', 'Utiliser des appareillages de production.'),
(28, 'Purification', 'Mettre en œuvre des opérations de séparation et de purification. '),
(29, 'Qualité', 'Vérifier la qualité du produit fini.'),
(30, 'Sécurité', 'Assurer la sécurité des installations industrielles.'),
(31, 'Gestion de projet', 'Travailler en groupe, en équipes.'),
(32, 'Comptabilité', 'Traiter et contrôler l\'ensemble des opérations comptables'),
(33, 'Finance', 'Analyser et contrôler des opérations relatives à l\'analyse financière'),
(34, 'Analyse budgétaire', 'Etablir les budgets'),
(35, 'Analyse de coûts', 'Analyser, contrôler et calculer les coûts'),
(36, 'Analyse de la rentabilté', 'Mesurer les performances de l\'organisation en concevant des tableaux de bords'),
(37, 'Gestion des systèmes ', 'Créer, controler et modifier des sytèmes, installation équipement électrique et électronique.'),
(38, 'Analyse de dysfonctionnement', 'Analyser des dysfonctionnement et mise en place de dépannage.'),
(39, 'Transformation de l\'énergie', 'Repérer et décrire des architectures de systèmes électroniques de conversion et de transformation de l\'énergie.'),
(40, 'Sécurité électrique', 'Travailler en sécurité (habilitation électrique)'),
(41, 'Développement d\'applications d\'automatisme', 'Réaliser l\'analyse fonctionnelle de l\'installation et la décliner en un programme d\'automatisation'),
(42, 'Programmation', 'Concevoir et développer les programmes et applications informatiques pour les organisations.'),
(43, 'Développement Web et mobile', 'Développer des sites et applications Web respectant les cahiers des charges et les standards d\'accessibilité en appliquant des méthodes de responsives web design.'),
(44, 'Gestion de base de données', 'Contrôler les données de réalisations Web en concevant et administrant leurs systèmes d\'information et leurs bases de données.'),
(45, 'Génie logiciel', 'Analyser et concevoir les solutions logicielles selont les méthodes, les outils et les bonnes pratiques de l\'ingénierie.'),
(46, 'Administration systèmes et réseaux', 'Contrôler les données de réalisations Web en concevant et administrrant leurs systèmes d\'information et leurs bases de données.'),
(47, 'Edition', 'Choisir des auteurs, sélectionner les manuscrits, les corriger, élaborer une politique éditoriale.'),
(48, 'PAO', 'Mettre en page, préparer les textes pour l\'imprimeur, faire un e-book.'),
(49, 'Vente-conseil', 'Pour un éditeur, vendre les livres sur tous supports et en tous pays; pour un libraire, animer une surface de vente.'),
(50, 'Communication', 'Faire de la publicité et de la promotion des livres, faire de la programmation culturelle en bibliothèque.'),
(51, 'Gestion de projet', 'Coordonner un projet d\'édition de livres, programmer un évènement culturel, créer des animations en magasins.'),
(52, 'Audio-visuel', 'Créer du contenu audio-visuel'),
(53, 'Sites web', 'Intégrer et implanter des sites web'),
(54, 'Web', 'Ecrire pour le web'),
(55, 'Maquettes digitales', 'Créer des maquettes digitales'),
(56, 'Applications web', 'Développer des applications web'),
(57, 'Metrologie', 'Choisir un instrument de mesure dans un contexte donné.'),
(58, 'Pilotage d’instrument', 'Réaliser un interfaçage homme-machine.'),
(59, 'Matériaux', 'Connaître les matériaux, leurs propriétés et leurs utilisations.'),
(60, 'Systèmes électroniques', 'Utiliser les composants actifs, leurs caractéristiques et les montages élecroniques associés.'),
(61, 'Thermique', 'Connaître les diverses machines thermiques et leurs performances.'),
(62, 'Réseaux', 'Concevoir, administrer et sécuriser des réseaux informatique'),
(63, 'Administration systèmes ', 'Administrer et/ou déployer des services sur windows et linux'),
(64, 'Développement Web et mobile', 'Conception et réalisation de site web et applications mobiles'),
(65, 'Téléphonie', 'Concevoir et administrer sur des réseaux ide téléphonie'),
(66, 'Transmission', 'Dimensionnnement et réalisation de systèmes de transmission (Optique,hertezien, ...)'),
(67, 'Analyser les risques', 'Analyser les risques'),
(68, 'Mesurer des données', 'Mesurer des données'),
(69, 'Mettre en place une démarche d\'évaluation des risques', 'Mettre en place une démarche d\'évaluation des risques'),
(70, 'Développer une politique HSE', 'Développer une politique HSE'),
(71, 'Participer à la gestion de crise', 'Participer à la gestion de crise'),
(77, 'Analyses chimiques', 'procéder à des analyses chimiques quantitatives et qualitatives.'),
(78, 'Analyses biologiques', 'procéder à des analyses biologiques quantitatives et qualitatives.'),
(79, 'Gestion de projet', 'Mener un projet professionnel.'),
(80, 'Communication', 'Se positionner dans son milieu professionnel.'),
(81, 'Qualité', 'Engager une démarche qualité.'),
(82, 'Contrôle', 'Utiliser des appareillages de production.'),
(83, 'Analyse chimique', 'Suivre des opérations de séparation et de purification. '),
(84, 'Qualité', 'Vérifier la qualité du produit fini.'),
(85, 'Sécurité', 'Assurer la sécurité des installations industrielles.'),
(86, 'Optimisation', 'Travailler en groupe, en équipes.');

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
