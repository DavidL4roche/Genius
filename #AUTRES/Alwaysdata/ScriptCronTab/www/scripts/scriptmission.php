<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/scriptmission/",
 *     description="Script complet de missions pour générer toutes les missions quotidiennes"
 * )
 */

require_once "configuration.php";

// Paramètre et fonction de debogage (afficher avec echo ou non)
// Changer le paramètre $debug en true pour avoir tout les affichages
/**
 * Affiche une string (ou non) avec la méthode echo (à utiliser en debug)
 * @param string $string Phrase à afficher
 */
function echoDebug($string)
{
    $debug = true;
    echo($debug == true ? $string . "\n" : null);
}

//Connexion à la base de Données
$connexion = mysqli_connect($host, $login, $password, $database);

if (!$connexion) {
    die('Erreur de connexion : ' . mysqli_connect_error());
}

/* check connection */
if (mysqli_connect_errno()) {
    printf("Connection échouée: %s\n", mysqli_connect_error());
    exit();
}

if (mysqli_ping($connexion)) {
    printf("La connection est ok !\n");
} else {
    printf("Erreur : %s\n", mysqli_error($connexion));
}

//Supprime toutes les missions présentes
$requete = "DELETE FROM `present_missions` WHERE 1";
echoDebug("• Etape 1 : Suppression des missions présentes");
//echo ($debug == true ? "Etape 1 : Suppression des missions présentes \n" : null);
if ($result = mysqli_query($connexion, $requete)) {
    echoDebug("Suppression effectuée");
} else {
    echoDebug("La suppression ne s'est pas effectuée");
}

// Supprime tout les divertissements
$requete = "DELETE FROM entertainment_done WHERE 1";
echoDebug("• Etape 2 : Suppression des divertissements présents");
if ($result = mysqli_query($connexion, $requete)) {
    echoDebug("Suppression effectuée");
} else {
    echoDebug("La suppression ne s'est pas effectuée");
}


//Récupère tout les ID de chaque mission
$listeMissions = array();
$requete = "SELECT IDMission FROM mission ORDER BY IDMission";
echoDebug("• Etape 3 : Récupération des ID de missions");
if ($result = mysqli_query($connexion, $requete)) {
    while ($tuple = mysqli_fetch_assoc($result)) {
        array_push($listeMissions, $tuple['IDMission']);
        //echo "ID = ".$tuple['IDMission'];
    }
    // Affichage du tableau
    $res = "";
    foreach ($listeMissions as $result) {
        $res .= $result . ' - ';
    }
    echoDebug($res);
} else {
    echoDebug("Récupération des ID échouée");
}

//Récupère nombre total de quartiers
$requete = "SELECT COUNT(*) AS Total FROM district";
echoDebug("• Etape 4 : Récupération du nombre de quartiers");
if ($result = mysqli_query($connexion, $requete)) {
    $totalquartier = $result->fetch_assoc();
    echoDebug("Nombre total de quartiers : " . $totalquartier['Total']);
} else {
    echoDebug("La requête a échouée");
}

// Suppresion des PNJ présents
$requete = "DELETE FROM `npc_present` WHERE 1";
echoDebug("• Etape 5 : Génération et placement des PNJ");
if ($result = mysqli_query($connexion, $requete)) {
    echoDebug("Suppression des PNJ présents effectuée");
} else {
    echoDebug("La suppression des PNJ présents est un échec");
}

// Récupération des missions de rang A
$tabmissionA = array();
echoDebug("• Etape 6 : Récupération missions de rang A");
$requete = "SELECT IDMission FROM mission WHERE IDRank in (Select IDRank from rank WHERE RankName ='A');";
if ($result = mysqli_query($connexion, $requete)) {
    while ($tuple = mysqli_fetch_assoc($result)) {
        array_push($tabmissionA, $tuple['IDMission']);
    }
    // Affichage du tableau
    $res = "";
    foreach ($tabmissionA as $result) {
        $res .= $result . ' - ';
    }
    echoDebug($res);
} else {
    echoDebug("La récupération est un échec");
}

// Parcours des quartiers et génération des PNJ (missions de rang A?)
$tabmissionA2 = array();
$tabmissionA2 = array_values($tabmissionA);
echoDebug("• Etape 7 : Parcours des quartiers et insertion des npc");
if (!empty($totalquartier) && !empty($tabmissionA2)) {
    for ($i = 0; $i < $totalquartier['Total']; ++$i) {
        if (empty($tabmissionA2)) {
            break;
        }

        if ($i != 0) {
            $rand = rand(0, 100);

            // Probabilité de 5/100 de générer une mission de rang A
            if ($rand <= 100) { // A CHANGER
                $randmissionA = rand(0, sizeof($tabmissionA2) - 1);

                $requete = "INSERT INTO npc_present SELECT IDNPCharacter,IDMission 
                                                    FROM np_character,mission 
                                                    WHERE IDNPCharacter IN (SELECT IDNPCharacter 
                                                                            FROM association_district_npc 
                                                                            WHERE IDDistrict =" . $i . ") 
                                                    AND IDMission =" . $tabmissionA2[$randmissionA] . ";";

                if ($result = mysqli_query($connexion, $requete)) {
                    unset($tabmissionA2[$randmissionA]);
                    echoDebug("Insertion missions réussie");
                } else {
                    echoDebug("L'insertion des missions a échouée");
                }
            }
        }
    }
}


// Génère et récupère le nombre de missions par quartier
$totaldemissions = 0;
$tabNbMissionParQuartier = array();
echoDebug("• Etape 8 : Génération du nombre de missions par quartier");
for ($i = 0; $i < $totalquartier['Total']; ++$i) {
    if ($i == 0) {
        $randomNbMission = 0;
    } else {
        $randomNbMission = random_int(1, 10);
    }
    $tabNbMissionParQuartier[$i] = $randomNbMission;
    $totaldemissions += $randomNbMission;
}

if (!empty($tabNbMissionParQuartier)) {
    echoDebug("Nombre de missions générées par quartier réussie (total : " . $totaldemissions . ")");
} else {
    echoDebug("La génération du nombre de missions par quartier a échouée");
}

// Tableau des missions présentes
$missionsPresentes = array();

// Pour chaque quartier
for ($i = 1; $i < sizeof($tabNbMissionParQuartier); ++$i) {

    echoDebug("------ Nouvelle itération for : " . $i);

    // Jusqu'à ce qu'il n'y ait plus de missions à générer dans le quartier
    while ($tabNbMissionParQuartier[$i] > 0) {

        // On vérifie que toutes les missions ne soient pas déjà générées
        if (sizeof($missionsPresentes) == sizeof($listeMissions)) {
            echoDebug("Plus de missions restantes");
            $tabNbMissionParQuartier[$i] -= 1;
            break;
        }

        $dejala = false;
        $randMission = rand(0, sizeof($listeMissions) - 1);
        $missionchoisi = $listeMissions[$randMission];
        //echoDebug("Mission choisie : " . $missionchoisi);

        if (in_array($missionchoisi, $missionsPresentes)) {
            $dejala = true;
            //echoDebug("Mission déjà existante");
            continue;
        }

        array_push($missionsPresentes, $missionchoisi);

        // Trouve une entreprise (id) dans le quartier en cours
        $identreprise = trouverUneEntreprise($i, $host, $login, $password, $database);
        //echoDebug("Id entreprise : " . $identreprise);

        // Insertion de la nouvelle mission
        $requete = "INSERT INTO present_missions(IDDistrict,IDMission,IDCompany) VALUE (" . $i . "," . $missionchoisi . "," . $identreprise . ");";
        if ($result = mysqli_query($connexion, $requete)) {
            echoDebug("Insertion des missions actuelles réussie");
        } else {
            echoDebug("L'insertion des missions actuelles a échouée");
        }

        $tabNbMissionParQuartier[$i] -= 1;
        //echoDebug("Nb missions du quartier : " . $tabNbMissionParQuartier[$i]);
    }
}

// Affichage du tableau
$res = "";
foreach ($missionsPresentes as $result) {
    $res .= $result . ' - ';
}
echoDebug("Missions présentes : " . $res);

/**
 * Retourne une entreprise (identifiant) dans le quartier demandé
 * @param int $idquartier Identifiant du quartier où se trouvera l'entreprise
 * @param string $host Hôte de connexion à la bd)
 * @param string $login Login de connexion à la bd)
 * @param string $password Mot de passe de connexion à la bd)
 * @param string $database Base de données ciblée
 * @return int Identifiant de l'entreprise retournée
 */
function trouverUneEntreprise($idquartier, $host, $login, $password, $database)
{
    $connexion = mysqli_connect($host, $login, $password, $database);

    $tableauentreprise = array();
    $randomtailleentreprise = rand(0, 100);

    // On choisit aléatoirement la taille de l'entreprise (1, 2 ou 4)
    if ($randomtailleentreprise < 50) {
        $tailleEntreprise = 1;
    } elseif ($randomtailleentreprise > 94) {
        $tailleEntreprise = 4;
    } else {
        $tailleEntreprise = 2;
    }
    $requete = "SELECT * from association_company_district WHERE IDDistrict =" . $idquartier . " AND IDCompany IN (SELECT IDCompany From company WHERE Size =" . $tailleEntreprise . ")";
    //echoDebug($requete);
    if ($result = mysqli_query($connexion, $requete)) {
        //echoDebug("Recherche d'entreprise réussie");
        while ($tuple = mysqli_fetch_assoc($result)) {
            array_push($tableauentreprise, $tuple['IDCompany']);
        }

        // Affichage du tableau
        $res = "";
        foreach ($tableauentreprise as $result) {
            $res .= $result . ' - ';
        }
        //echoDebug($res);

        $identrepriserandom = $tableauentreprise[rand(0, sizeof($tableauentreprise) - 1)];
        return $identrepriserandom;
    } else {
        echoDebug("La recherche d'entreprise a échouée");
        return 0;
    }
}