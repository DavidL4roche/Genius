-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Hôte : mysql-nathberoux.alwaysdata.net
-- Généré le :  jeu. 30 août 2018 à 15:46
-- Version du serveur :  10.1.31-MariaDB
-- Version de PHP :  7.2.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `nathberoux_genius`
--

-- --------------------------------------------------------

--
-- Structure de la table `aptitude`
--

CREATE TABLE `aptitude` (
  `IDAptitude` int(20) NOT NULL,
  `AptitudeName` varchar(50) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `LevelRequired` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `artefact`
--

CREATE TABLE `artefact` (
  `IDArtefact` int(11) NOT NULL,
  `ArtefactName` varchar(20) NOT NULL,
  `IDBonus` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `artefact`
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

CREATE TABLE `artefact_pc` (
  `IDArtefact` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `artefact_pc`
--

INSERT INTO `artefact_pc` (`IDArtefact`, `IDPCharacter`) VALUES
(6, 1);

-- --------------------------------------------------------

--
-- Structure de la table `artefact_used`
--

CREATE TABLE `artefact_used` (
  `IDArtefact` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `artefact_used`
--

INSERT INTO `artefact_used` (`IDArtefact`, `IDPCharacter`) VALUES
(6, 1);

-- --------------------------------------------------------

--
-- Structure de la table `association_city_district`
--

CREATE TABLE `association_city_district` (
  `IDCity` int(20) NOT NULL,
  `IDDistrict` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `association_city_district`
--

INSERT INTO `association_city_district` (`IDCity`, `IDDistrict`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `association_company_district`
--

CREATE TABLE `association_company_district` (
  `IDCompany` int(11) NOT NULL,
  `IDDistrict` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `association_company_district`
--

INSERT INTO `association_company_district` (`IDCompany`, `IDDistrict`) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(1, 8),
(2, 3),
(2, 4),
(2, 5),
(5, 1),
(5, 2),
(5, 6),
(5, 8),
(6, 3),
(6, 5),
(6, 8),
(7, 1),
(7, 4),
(7, 5),
(8, 5),
(8, 6),
(8, 8),
(9, 3),
(9, 6),
(9, 8),
(10, 1),
(10, 2),
(10, 3),
(11, 2),
(11, 3),
(11, 4),
(12, 1),
(12, 2),
(12, 4);

-- --------------------------------------------------------

--
-- Structure de la table `association_diploms`
--

CREATE TABLE `association_diploms` (
  `IDDiplom` int(11) NOT NULL,
  `IDDiplomRequiered` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `association_diploms`
--

INSERT INTO `association_diploms` (`IDDiplom`, `IDDiplomRequiered`) VALUES
(3, 2);

-- --------------------------------------------------------

--
-- Structure de la table `association_district_entertainment`
--

CREATE TABLE `association_district_entertainment` (
  `IDDistrict` int(11) NOT NULL,
  `IDEntertainment` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `association_district_entertainment`
--

INSERT INTO `association_district_entertainment` (`IDDistrict`, `IDEntertainment`) VALUES
(1, 1),
(2, 2);

-- --------------------------------------------------------

--
-- Structure de la table `association_district_npc`
--

CREATE TABLE `association_district_npc` (
  `IDDistrict` int(20) NOT NULL,
  `IDNPCharacter` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `association_district_npc`
--

INSERT INTO `association_district_npc` (`IDDistrict`, `IDNPCharacter`) VALUES
(1, 1),
(2, 3);

-- --------------------------------------------------------

--
-- Structure de la table `association_district_place`
--

CREATE TABLE `association_district_place` (
  `IDDistrict` int(11) NOT NULL,
  `IDPlace` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `association_job_character`
--

CREATE TABLE `association_job_character` (
  `IDJob` int(20) NOT NULL,
  `IDPCharacter` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `association_job_skill`
--

CREATE TABLE `association_job_skill` (
  `IDJob` int(20) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `IDRank` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `association_place_exam`
--

CREATE TABLE `association_place_exam` (
  `IDPlace` int(11) NOT NULL,
  `IDExam` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `association_place_exam`
--

INSERT INTO `association_place_exam` (`IDPlace`, `IDExam`) VALUES
(2, 1),
(2, 2),
(2, 3);

-- --------------------------------------------------------

--
-- Structure de la table `association_ressource_pc`
--

CREATE TABLE `association_ressource_pc` (
  `IDRessource` int(20) NOT NULL,
  `IDPCharacter` int(20) NOT NULL,
  `Value` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `association_ressource_pc`
--

INSERT INTO `association_ressource_pc` (`IDRessource`, `IDPCharacter`, `Value`) VALUES
(1, 1, 5997),
(4, 1, 100),
(3, 1, 100),
(2, 1, 5),
(1, 3, 0),
(2, 3, 0),
(3, 3, 15),
(4, 3, 15),
(1, 4, 0),
(2, 4, 0),
(3, 4, 0),
(4, 4, 0);

-- --------------------------------------------------------

--
-- Structure de la table `association_shop_item`
--

CREATE TABLE `association_shop_item` (
  `IDShop` int(11) NOT NULL,
  `IDItem` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `association_shop_item`
--

INSERT INTO `association_shop_item` (`IDShop`, `IDItem`) VALUES
(1, 2),
(1, 1),
(1, 3),
(1, 5),
(1, 6),
(1, 9),
(1, 10);

-- --------------------------------------------------------

--
-- Structure de la table `bonus`
--

CREATE TABLE `bonus` (
  `IDBonus` int(20) NOT NULL,
  `BonusName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `bonus`
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

CREATE TABLE `city` (
  `IDCity` int(20) NOT NULL,
  `NameCity` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `city`
--

INSERT INTO `city` (`IDCity`, `NameCity`) VALUES
(1, 'Daedelus');

-- --------------------------------------------------------

--
-- Structure de la table `company`
--

CREATE TABLE `company` (
  `IDCompany` int(20) NOT NULL,
  `CompanyName` varchar(50) NOT NULL,
  `Size` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `company`
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

CREATE TABLE `company_specialization` (
  `IDDistrict` int(11) NOT NULL,
  `IDCompany` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `company_specialization`
--

INSERT INTO `company_specialization` (`IDDistrict`, `IDCompany`) VALUES
(1, 1),
(1, 7),
(2, 1),
(2, 11),
(3, 1),
(3, 10),
(4, 1),
(4, 12),
(5, 1),
(5, 6),
(6, 1),
(6, 9),
(8, 1),
(8, 8);

-- --------------------------------------------------------

--
-- Structure de la table `diplom`
--

CREATE TABLE `diplom` (
  `IDDiplom` int(11) NOT NULL,
  `DiplomName` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `diplom`
--

INSERT INTO `diplom` (`IDDiplom`, `DiplomName`) VALUES
(1, 'Bac S'),
(2, 'DUT Info'),
(3, 'Licence WEB');

-- --------------------------------------------------------

--
-- Structure de la table `diplom_pc`
--

CREATE TABLE `diplom_pc` (
  `IDDiplom` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `diplom_pc`
--

INSERT INTO `diplom_pc` (`IDDiplom`, `IDPCharacter`) VALUES
(1, 1),
(2, 1);

-- --------------------------------------------------------

--
-- Structure de la table `district`
--

CREATE TABLE `district` (
  `IDDistrict` int(11) NOT NULL,
  `DistrictName` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `district`
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

CREATE TABLE `duration` (
  `IDDuration` int(11) NOT NULL,
  `DurationName` varchar(11) NOT NULL,
  `DurationValue` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `duration`
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

CREATE TABLE `entertainment` (
  `IDEntertainment` int(11) NOT NULL,
  `EntertainmentName` varchar(20) NOT NULL,
  `IDRank` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `entertainment`
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

CREATE TABLE `entertainment_done` (
  `IDEntertainment` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `entertainment_done`
--

INSERT INTO `entertainment_done` (`IDEntertainment`, `IDPCharacter`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `exam`
--

CREATE TABLE `exam` (
  `IDExam` int(20) NOT NULL,
  `ExamName` varchar(50) NOT NULL,
  `IDDiplom` int(20) NOT NULL,
  `IDRank` int(20) NOT NULL,
  `IDSkillSlot1` int(11) DEFAULT NULL,
  `IDSkillSlot2` int(11) DEFAULT NULL,
  `IDSkillSlot3` int(11) DEFAULT NULL,
  `IDSkillSlot4` int(11) DEFAULT NULL,
  `IDSkillSlot5` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `exam`
--

INSERT INTO `exam` (`IDExam`, `ExamName`, `IDDiplom`, `IDRank`, `IDSkillSlot1`, `IDSkillSlot2`, `IDSkillSlot3`, `IDSkillSlot4`, `IDSkillSlot5`) VALUES
(1, 'DUT Informatique', 2, 2, 5, 4, 2, 1, 3),
(2, 'Exam Licence WEB', 3, 1, 5, 2, 3, 1, 4),
(3, 'Bac S', 1, 1, 5, 2, 3, 1, 4);

-- --------------------------------------------------------

--
-- Structure de la table `friend`
--

CREATE TABLE `friend` (
  `IDPCharacter` int(11) NOT NULL,
  `IDFriend` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `friend`
--

INSERT INTO `friend` (`IDPCharacter`, `IDFriend`) VALUES
(1, 3),
(1, 4);

-- --------------------------------------------------------

--
-- Structure de la table `gain`
--

CREATE TABLE `gain` (
  `IDGain` int(11) NOT NULL,
  `GainName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `gain`
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

CREATE TABLE `item` (
  `IDItem` int(20) NOT NULL,
  `ItemName` varchar(50) NOT NULL,
  `IDBonus` int(20) NOT NULL,
  `IDRank` int(11) NOT NULL,
  `BonusGain` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `item`
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

CREATE TABLE `item_bought` (
  `IDItem` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `item_bought`
--

INSERT INTO `item_bought` (`IDItem`, `IDPCharacter`) VALUES
(1, 1),
(2, 1),
(3, 1);

-- --------------------------------------------------------

--
-- Structure de la table `item_pc`
--

CREATE TABLE `item_pc` (
  `IDItem` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `item_pc`
--

INSERT INTO `item_pc` (`IDItem`, `IDPCharacter`, `Quantity`) VALUES
(1, 1, 1),
(1, 3, 10),
(1, 4, 0),
(2, 1, 1),
(2, 3, 0),
(2, 4, 0),
(3, 1, 0),
(3, 3, 0),
(3, 4, 0),
(4, 1, 0),
(4, 3, 0),
(4, 4, 0),
(5, 1, 0),
(5, 3, 0),
(5, 4, 0),
(6, 1, 0),
(6, 3, 0),
(6, 4, 0),
(7, 1, 0),
(7, 3, 0),
(7, 4, 0),
(8, 1, 1),
(8, 3, 0),
(8, 4, 0),
(9, 1, 14),
(9, 3, 0),
(9, 4, 0),
(10, 1, 0),
(10, 3, 0),
(10, 4, 0),
(11, 1, 0),
(11, 3, 0),
(11, 4, 0),
(12, 1, 0),
(12, 3, 0),
(12, 4, 0);

-- --------------------------------------------------------

--
-- Structure de la table `job`
--

CREATE TABLE `job` (
  `IDJob` int(11) NOT NULL,
  `JobName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `loss`
--

CREATE TABLE `loss` (
  `IDLoss` int(11) NOT NULL,
  `LossName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `loss`
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

CREATE TABLE `mission` (
  `IDMission` int(20) NOT NULL,
  `MissionName` varchar(50) NOT NULL,
  `IDRank` int(20) NOT NULL,
  `IDSkill1` int(20) DEFAULT NULL,
  `IDSkill2` int(20) DEFAULT NULL,
  `IDSkill3` int(20) DEFAULT NULL,
  `IDSkill4` int(20) DEFAULT NULL,
  `IDSkill5` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `mission`
--

INSERT INTO `mission` (`IDMission`, `MissionName`, `IDRank`, `IDSkill1`, `IDSkill2`, `IDSkill3`, `IDSkill4`, `IDSkill5`) VALUES
(1, 'Créer un logiciel', 2, 2, 5, NULL, NULL, NULL),
(2, 'Réparation de voiture', 1, 3, NULL, NULL, NULL, NULL),
(3, 'Monter ces personnages', 1, 1, NULL, NULL, NULL, NULL),
(4, 'Manger du pain', 3, 2, 5, 4, 1, NULL),
(5, 'Mission de dieu', 4, 1, 2, 3, 4, 5),
(6, 'Manger des nems', 2, 2, 1, NULL, NULL, NULL),
(7, 'Jouer au ping pong', 1, 5, NULL, NULL, NULL, NULL),
(8, 'Taper dieu', 4, 2, 1, 3, 4, 5),
(9, 'Savoir réfléchir', 3, 5, 5, 5, 5, NULL);

-- --------------------------------------------------------

--
-- Structure de la table `npc_present`
--

CREATE TABLE `npc_present` (
  `IDNPCharacter` int(11) NOT NULL,
  `IDMission` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `npc_social_mission`
--

CREATE TABLE `npc_social_mission` (
  `IDNPCharacter` int(20) NOT NULL,
  `IDSMission` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `np_character`
--

CREATE TABLE `np_character` (
  `IDNPCharacter` int(20) NOT NULL,
  `NPCName` varchar(50) NOT NULL,
  `IDArtefact` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `np_character`
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

CREATE TABLE `place` (
  `IDPlace` int(11) NOT NULL,
  `PlaceName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `place`
--

INSERT INTO `place` (`IDPlace`, `PlaceName`) VALUES
(2, 'SalleExamEIMP');

-- --------------------------------------------------------

--
-- Structure de la table `present_missions`
--

CREATE TABLE `present_missions` (
  `IDDistrict` int(11) NOT NULL,
  `IDMission` int(11) NOT NULL,
  `IDCompany` int(11) DEFAULT NULL,
  `Date` timestamp NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `present_missions`
--

INSERT INTO `present_missions` (`IDDistrict`, `IDMission`, `IDCompany`, `Date`) VALUES
(1, 1, 12, '2018-08-28 10:50:34'),
(1, 8, 1, '2018-08-28 10:50:34'),
(2, 3, 12, '2018-08-28 10:50:34'),
(2, 4, 5, '2018-08-28 10:50:34'),
(2, 6, 5, '2018-08-28 10:50:34'),
(2, 7, 5, '2018-08-28 10:50:34'),
(3, 5, 2, '2018-08-28 10:50:34'),
(4, 2, 2, '2018-08-28 10:50:34'),
(5, 9, 2, '2018-08-28 10:50:34');

-- --------------------------------------------------------

--
-- Structure de la table `present_missions_done`
--

CREATE TABLE `present_missions_done` (
  `IDMission` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `present_missions_done`
--

INSERT INTO `present_missions_done` (`IDMission`, `IDPCharacter`) VALUES
(3, 1);

-- --------------------------------------------------------

--
-- Structure de la table `p_character`
--

CREATE TABLE `p_character` (
  `IDPCharacter` int(11) NOT NULL,
  `PCName` varchar(50) NOT NULL,
  `Password` varchar(30) NOT NULL,
  `LastConnection` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `p_character`
--

INSERT INTO `p_character` (`IDPCharacter`, `PCName`, `Password`, `LastConnection`) VALUES
(1, 'nathan', 'nathan', '2018-08-30 13:39:00'),
(3, 'lilith', 'lilith', '2018-08-28 13:45:04'),
(4, 'jeanval', 'jean', '2018-08-29 13:25:52');

-- --------------------------------------------------------

--
-- Structure de la table `rank`
--

CREATE TABLE `rank` (
  `IDRank` int(20) NOT NULL,
  `RankName` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `rank`
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

CREATE TABLE `ressource` (
  `IDRessource` int(11) NOT NULL,
  `RessourceName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `ressource`
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

CREATE TABLE `shop` (
  `IDShop` int(20) NOT NULL,
  `ShopName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `shop`
--

INSERT INTO `shop` (`IDShop`, `ShopName`) VALUES
(1, 'Magasin');

-- --------------------------------------------------------

--
-- Structure de la table `skill`
--

CREATE TABLE `skill` (
  `IDSkill` int(20) NOT NULL,
  `SkillName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `skill`
--

INSERT INTO `skill` (`IDSkill`, `SkillName`) VALUES
(1, 'Informatique'),
(2, 'Art Plastique'),
(3, 'Comptabilité'),
(4, 'Mécanique'),
(5, 'Anglais');

-- --------------------------------------------------------

--
-- Structure de la table `skill_job`
--

CREATE TABLE `skill_job` (
  `IDJob` int(20) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `LevelRequiered` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `skill_npc`
--

CREATE TABLE `skill_npc` (
  `IDNPCharacter` int(20) NOT NULL,
  `IDSkill` int(20) NOT NULL,
  `SkillLevel` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `skill_pc`
--

CREATE TABLE `skill_pc` (
  `IDSkill` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL,
  `SkillLevel` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `skill_pc`
--

INSERT INTO `skill_pc` (`IDSkill`, `IDPCharacter`, `SkillLevel`) VALUES
(1, 1, 95),
(1, 3, 0),
(1, 4, 0),
(2, 1, 29),
(2, 3, 0),
(2, 4, 0),
(3, 1, 69),
(3, 3, 0),
(3, 4, 0),
(4, 1, 25),
(4, 3, 0),
(4, 4, 0),
(5, 1, 100),
(5, 3, 0),
(5, 4, 0);

-- --------------------------------------------------------

--
-- Structure de la table `social_mission`
--

CREATE TABLE `social_mission` (
  `IDSMission` int(20) NOT NULL,
  `IDRank` int(20) NOT NULL,
  `IDItem` int(20) NOT NULL,
  `IDAptitude` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `social_network`
--

CREATE TABLE `social_network` (
  `IDSNetwork` int(20) NOT NULL,
  `SNetworkName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `trophy`
--

CREATE TABLE `trophy` (
  `IDTrophy` int(11) NOT NULL,
  `IDRank` int(11) NOT NULL,
  `Description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `trophy`
--

INSERT INTO `trophy` (`IDTrophy`, `IDRank`, `Description`) VALUES
(1, 3, 'Réussir 100 missions de rang C'),
(2, 1, 'Obtenir votre premier objet');

-- --------------------------------------------------------

--
-- Structure de la table `trophy_pc`
--

CREATE TABLE `trophy_pc` (
  `IDTrophy` int(11) NOT NULL,
  `IDPCharacter` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `trophy_pc`
--

INSERT INTO `trophy_pc` (`IDTrophy`, `IDPCharacter`) VALUES
(1, 1),
(2, 1);

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `aptitude`
--
ALTER TABLE `aptitude`
  ADD PRIMARY KEY (`IDAptitude`),
  ADD KEY `id_skill_index` (`IDSkill`);

--
-- Index pour la table `artefact`
--
ALTER TABLE `artefact`
  ADD PRIMARY KEY (`IDArtefact`),
  ADD KEY `fk_id_bonus_artefact` (`IDBonus`);

--
-- Index pour la table `artefact_pc`
--
ALTER TABLE `artefact_pc`
  ADD PRIMARY KEY (`IDArtefact`,`IDPCharacter`),
  ADD KEY `fk_id_pc_artefact` (`IDPCharacter`);

--
-- Index pour la table `artefact_used`
--
ALTER TABLE `artefact_used`
  ADD PRIMARY KEY (`IDArtefact`,`IDPCharacter`),
  ADD KEY `fk_id_pc_artefact_used` (`IDPCharacter`);

--
-- Index pour la table `association_city_district`
--
ALTER TABLE `association_city_district`
  ADD KEY `id_district_index` (`IDDistrict`) USING BTREE,
  ADD KEY `fk_id_city` (`IDCity`);

--
-- Index pour la table `association_company_district`
--
ALTER TABLE `association_company_district`
  ADD PRIMARY KEY (`IDCompany`,`IDDistrict`),
  ADD KEY `fk_id_district_company` (`IDDistrict`);

--
-- Index pour la table `association_diploms`
--
ALTER TABLE `association_diploms`
  ADD KEY `id_diplom_index` (`IDDiplomRequiered`,`IDDiplom`) USING BTREE,
  ADD KEY `fk_id_diplom` (`IDDiplom`);

--
-- Index pour la table `association_district_entertainment`
--
ALTER TABLE `association_district_entertainment`
  ADD PRIMARY KEY (`IDDistrict`,`IDEntertainment`),
  ADD KEY `fk_id_entertainment_district` (`IDEntertainment`);

--
-- Index pour la table `association_district_npc`
--
ALTER TABLE `association_district_npc`
  ADD KEY `id_district_index` (`IDDistrict`),
  ADD KEY `fk_id_npc` (`IDNPCharacter`);

--
-- Index pour la table `association_district_place`
--
ALTER TABLE `association_district_place`
  ADD PRIMARY KEY (`IDDistrict`,`IDPlace`),
  ADD KEY `fk_id_place_district` (`IDPlace`);

--
-- Index pour la table `association_job_character`
--
ALTER TABLE `association_job_character`
  ADD KEY `id_job_index` (`IDJob`),
  ADD KEY `id_character_index` (`IDPCharacter`);

--
-- Index pour la table `association_job_skill`
--
ALTER TABLE `association_job_skill`
  ADD KEY `id_job_index` (`IDJob`),
  ADD KEY `id_skill_index` (`IDSkill`),
  ADD KEY `id_rank_index` (`IDRank`);

--
-- Index pour la table `association_place_exam`
--
ALTER TABLE `association_place_exam`
  ADD PRIMARY KEY (`IDPlace`,`IDExam`),
  ADD KEY `fk_id_exam_place` (`IDExam`);

--
-- Index pour la table `association_ressource_pc`
--
ALTER TABLE `association_ressource_pc`
  ADD KEY `id_ressource_index` (`IDRessource`),
  ADD KEY `id_character_index` (`IDPCharacter`);

--
-- Index pour la table `association_shop_item`
--
ALTER TABLE `association_shop_item`
  ADD KEY `id_shop_index` (`IDShop`),
  ADD KEY `id_item_index` (`IDShop`),
  ADD KEY `fk_item_shop` (`IDItem`);

--
-- Index pour la table `bonus`
--
ALTER TABLE `bonus`
  ADD PRIMARY KEY (`IDBonus`);

--
-- Index pour la table `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`IDCity`);

--
-- Index pour la table `company`
--
ALTER TABLE `company`
  ADD PRIMARY KEY (`IDCompany`);

--
-- Index pour la table `company_specialization`
--
ALTER TABLE `company_specialization`
  ADD PRIMARY KEY (`IDDistrict`,`IDCompany`),
  ADD KEY `fk_id_company_spec_district` (`IDCompany`);

--
-- Index pour la table `diplom`
--
ALTER TABLE `diplom`
  ADD PRIMARY KEY (`IDDiplom`);

--
-- Index pour la table `diplom_pc`
--
ALTER TABLE `diplom_pc`
  ADD PRIMARY KEY (`IDDiplom`,`IDPCharacter`),
  ADD KEY `fk_id_pc_diplom` (`IDPCharacter`);

--
-- Index pour la table `district`
--
ALTER TABLE `district`
  ADD PRIMARY KEY (`IDDistrict`);

--
-- Index pour la table `duration`
--
ALTER TABLE `duration`
  ADD PRIMARY KEY (`IDDuration`);

--
-- Index pour la table `entertainment`
--
ALTER TABLE `entertainment`
  ADD PRIMARY KEY (`IDEntertainment`),
  ADD KEY `fk_id_rank_entertainment` (`IDRank`);

--
-- Index pour la table `entertainment_done`
--
ALTER TABLE `entertainment_done`
  ADD PRIMARY KEY (`IDEntertainment`,`IDPCharacter`),
  ADD KEY `fk_id_pc_entertainment_done` (`IDPCharacter`);

--
-- Index pour la table `exam`
--
ALTER TABLE `exam`
  ADD PRIMARY KEY (`IDExam`),
  ADD KEY `fk_id_skill1` (`IDSkillSlot1`),
  ADD KEY `fk_id_skill2` (`IDSkillSlot2`),
  ADD KEY `fk_id_skill3` (`IDSkillSlot3`),
  ADD KEY `fk_id_skill4` (`IDSkillSlot4`),
  ADD KEY `fk_id_skill5` (`IDSkillSlot5`),
  ADD KEY `fk_id_diplom_exam` (`IDDiplom`),
  ADD KEY `fk_id_rank_exam` (`IDRank`);

--
-- Index pour la table `friend`
--
ALTER TABLE `friend`
  ADD PRIMARY KEY (`IDPCharacter`,`IDFriend`),
  ADD KEY `fk_id_pc_friend` (`IDFriend`);

--
-- Index pour la table `gain`
--
ALTER TABLE `gain`
  ADD PRIMARY KEY (`IDGain`);

--
-- Index pour la table `item`
--
ALTER TABLE `item`
  ADD PRIMARY KEY (`IDItem`),
  ADD KEY `id_bonus_index` (`IDBonus`) USING BTREE,
  ADD KEY `fk_id_rank_item` (`IDRank`);

--
-- Index pour la table `item_bought`
--
ALTER TABLE `item_bought`
  ADD PRIMARY KEY (`IDItem`,`IDPCharacter`),
  ADD KEY `fk_id_pc_item_bought` (`IDPCharacter`);

--
-- Index pour la table `item_pc`
--
ALTER TABLE `item_pc`
  ADD PRIMARY KEY (`IDItem`,`IDPCharacter`),
  ADD KEY `fk_id_pc_item` (`IDPCharacter`);

--
-- Index pour la table `job`
--
ALTER TABLE `job`
  ADD PRIMARY KEY (`IDJob`);

--
-- Index pour la table `loss`
--
ALTER TABLE `loss`
  ADD PRIMARY KEY (`IDLoss`);

--
-- Index pour la table `mission`
--
ALTER TABLE `mission`
  ADD PRIMARY KEY (`IDMission`),
  ADD KEY `id_rank_index` (`IDRank`),
  ADD KEY `fk_skill4` (`IDSkill4`),
  ADD KEY `fk_skill5` (`IDSkill5`),
  ADD KEY `id_skill_index` (`IDSkill1`,`IDSkill2`,`IDSkill3`,`IDSkill4`,`IDSkill5`) USING BTREE,
  ADD KEY `fk_skill3` (`IDSkill3`) USING BTREE,
  ADD KEY `fk_skill2` (`IDSkill2`);

--
-- Index pour la table `npc_present`
--
ALTER TABLE `npc_present`
  ADD PRIMARY KEY (`IDMission`),
  ADD KEY `fk_id_npc_present` (`IDNPCharacter`);

--
-- Index pour la table `npc_social_mission`
--
ALTER TABLE `npc_social_mission`
  ADD KEY `id_npcharacter_index` (`IDNPCharacter`),
  ADD KEY `id_smission_index` (`IDSMission`);

--
-- Index pour la table `np_character`
--
ALTER TABLE `np_character`
  ADD PRIMARY KEY (`IDNPCharacter`),
  ADD KEY `fk_id_artefact_npc` (`IDArtefact`);

--
-- Index pour la table `place`
--
ALTER TABLE `place`
  ADD PRIMARY KEY (`IDPlace`);

--
-- Index pour la table `present_missions`
--
ALTER TABLE `present_missions`
  ADD PRIMARY KEY (`IDDistrict`,`IDMission`),
  ADD KEY `fk_id_mission_district` (`IDMission`),
  ADD KEY `fk_company_mission` (`IDCompany`);

--
-- Index pour la table `present_missions_done`
--
ALTER TABLE `present_missions_done`
  ADD KEY `fk_id_mission_present` (`IDMission`),
  ADD KEY `fk_id_mission_pc` (`IDPCharacter`);

--
-- Index pour la table `p_character`
--
ALTER TABLE `p_character`
  ADD PRIMARY KEY (`IDPCharacter`);

--
-- Index pour la table `rank`
--
ALTER TABLE `rank`
  ADD PRIMARY KEY (`IDRank`);

--
-- Index pour la table `ressource`
--
ALTER TABLE `ressource`
  ADD PRIMARY KEY (`IDRessource`);

--
-- Index pour la table `shop`
--
ALTER TABLE `shop`
  ADD PRIMARY KEY (`IDShop`);

--
-- Index pour la table `skill`
--
ALTER TABLE `skill`
  ADD PRIMARY KEY (`IDSkill`);

--
-- Index pour la table `skill_job`
--
ALTER TABLE `skill_job`
  ADD KEY `id_job_index` (`IDJob`),
  ADD KEY `id_skill_index` (`IDSkill`);

--
-- Index pour la table `skill_npc`
--
ALTER TABLE `skill_npc`
  ADD KEY `id_npcharacter_index` (`IDNPCharacter`),
  ADD KEY `id_skill_index` (`IDSkill`);

--
-- Index pour la table `skill_pc`
--
ALTER TABLE `skill_pc`
  ADD PRIMARY KEY (`IDSkill`,`IDPCharacter`),
  ADD KEY `fk_id_character_skill` (`IDPCharacter`);

--
-- Index pour la table `social_mission`
--
ALTER TABLE `social_mission`
  ADD PRIMARY KEY (`IDSMission`),
  ADD KEY `id_rank_index` (`IDRank`),
  ADD KEY `id_item_index` (`IDItem`),
  ADD KEY `id_aptitude_index` (`IDAptitude`);

--
-- Index pour la table `social_network`
--
ALTER TABLE `social_network`
  ADD PRIMARY KEY (`IDSNetwork`);

--
-- Index pour la table `trophy`
--
ALTER TABLE `trophy`
  ADD PRIMARY KEY (`IDTrophy`),
  ADD KEY `fk_id_rank_trophy` (`IDRank`);

--
-- Index pour la table `trophy_pc`
--
ALTER TABLE `trophy_pc`
  ADD PRIMARY KEY (`IDTrophy`,`IDPCharacter`),
  ADD KEY `fk_id_pc_trophy` (`IDPCharacter`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `aptitude`
--
ALTER TABLE `aptitude`
  MODIFY `IDAptitude` int(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `artefact`
--
ALTER TABLE `artefact`
  MODIFY `IDArtefact` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `bonus`
--
ALTER TABLE `bonus`
  MODIFY `IDBonus` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT pour la table `city`
--
ALTER TABLE `city`
  MODIFY `IDCity` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `company`
--
ALTER TABLE `company`
  MODIFY `IDCompany` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT pour la table `diplom`
--
ALTER TABLE `diplom`
  MODIFY `IDDiplom` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `district`
--
ALTER TABLE `district`
  MODIFY `IDDistrict` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `duration`
--
ALTER TABLE `duration`
  MODIFY `IDDuration` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT pour la table `entertainment`
--
ALTER TABLE `entertainment`
  MODIFY `IDEntertainment` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `exam`
--
ALTER TABLE `exam`
  MODIFY `IDExam` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `gain`
--
ALTER TABLE `gain`
  MODIFY `IDGain` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `item`
--
ALTER TABLE `item`
  MODIFY `IDItem` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT pour la table `job`
--
ALTER TABLE `job`
  MODIFY `IDJob` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `loss`
--
ALTER TABLE `loss`
  MODIFY `IDLoss` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT pour la table `mission`
--
ALTER TABLE `mission`
  MODIFY `IDMission` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `np_character`
--
ALTER TABLE `np_character`
  MODIFY `IDNPCharacter` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT pour la table `place`
--
ALTER TABLE `place`
  MODIFY `IDPlace` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pour la table `p_character`
--
ALTER TABLE `p_character`
  MODIFY `IDPCharacter` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `rank`
--
ALTER TABLE `rank`
  MODIFY `IDRank` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT pour la table `ressource`
--
ALTER TABLE `ressource`
  MODIFY `IDRessource` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT pour la table `shop`
--
ALTER TABLE `shop`
  MODIFY `IDShop` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `skill`
--
ALTER TABLE `skill`
  MODIFY `IDSkill` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT pour la table `social_mission`
--
ALTER TABLE `social_mission`
  MODIFY `IDSMission` int(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `social_network`
--
ALTER TABLE `social_network`
  MODIFY `IDSNetwork` int(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `trophy`
--
ALTER TABLE `trophy`
  MODIFY `IDTrophy` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Contraintes pour les tables déchargées
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
  ADD CONSTRAINT `fk_id_character_2` FOREIGN KEY (`IDPCharacter`) REFERENCES `p_character` (`IDPCharacter`),
  ADD CONSTRAINT `fk_id_ressource` FOREIGN KEY (`IDRessource`) REFERENCES `ressource` (`IDRessource`);

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
  ADD CONSTRAINT `fk_id_diplom_pc` FOREIGN KEY (`IDDiplom`) REFERENCES `diplom` (`IDDiplom`),
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
  ADD CONSTRAINT `fk_id_skill1` FOREIGN KEY (`IDSkillSlot1`) REFERENCES `skill` (`IDSkill`),
  ADD CONSTRAINT `fk_id_skill2` FOREIGN KEY (`IDSkillSlot2`) REFERENCES `skill` (`IDSkill`),
  ADD CONSTRAINT `fk_id_skill3` FOREIGN KEY (`IDSkillSlot3`) REFERENCES `skill` (`IDSkill`),
  ADD CONSTRAINT `fk_id_skill4` FOREIGN KEY (`IDSkillSlot4`) REFERENCES `skill` (`IDSkill`),
  ADD CONSTRAINT `fk_id_skill5` FOREIGN KEY (`IDSkillSlot5`) REFERENCES `skill` (`IDSkill`);

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
  ADD CONSTRAINT `fk_rank` FOREIGN KEY (`IDRank`) REFERENCES `rank` (`IDRank`),
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
  ADD CONSTRAINT `fk_id_skill_pc` FOREIGN KEY (`IDSkill`) REFERENCES `skill` (`IDSkill`);

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
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
