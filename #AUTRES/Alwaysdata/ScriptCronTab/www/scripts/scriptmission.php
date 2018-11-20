<?php
/**
 * Created by PhpStorm.
 * User: Nathan BERNARD
 * Date: 05/06/2018
 * Time: 10:04
 */
require_once "configuration.php";

//Connexion à la base de Données

$connexion = mysqli_connect($host,$login,$password,$database);
if (!$connexion) {
    die('Erreur de connexion : ' . mysqli_connect_error());
}
//Supprime toutes les missions présentes
$requete = "DELETE FROM `present_missions` WHERE 1";
if($result = mysqli_query($connexion,$requete)){
    echo "ça a marché la suppression <br><br>";
}
else{
    echo "ça a pas marché <br><br>";
}
$requete="DELETE FROM present_missions_done WHERE 1";
if($result = mysqli_query($connexion,$requete))
{

}
else
{
}
$requete="DELETE FROM entertainment_done WHERE 1";
if($result = mysqli_query($connexion,$requete))
{
	
}
else
{
}
//Récupère tout les ID de chaque mission
$listeMissions = array();
$requete = "SELECT * FROM mission";
if($result = mysqli_query($connexion,$requete)){
    while ($tuple = mysqli_fetch_assoc($result)){
        array_push($listeMissions,$tuple['IDMission']);
	//echo "ID = ".$tuple['IDMission'];
    }
}
else{
    echo "ça a pas marché dans la query";
}

//Récupère nombre total de quartier
$requete = "SELECT Count(*) AS Total, IDDistrict FROM district";
if($result = mysqli_query($connexion,$requete)){
    while ($tuple = mysqli_fetch_assoc($result)){
        $totalquartier = (int)$tuple['Total'];
        //echo $totalquartier." nombre total de quartier<br><br>";
    }
}
else{
    echo "ça a pas marché dans la query";
}
// on génère et place les pnj
$requete = "DELETE FROM `npc_present` WHERE 1";
if($result = mysqli_query($connexion,$requete)){
    echo "ça a marché le drop table de npc<br><br>";
}
else{
    echo "ça a pas marché le drop table de npc<br><br>";
}

$tabmissionA = array();
$tabmissionA2 = array();
$requete = "SELECT IDMission FROM mission WHERE IDRank in (Select IDRank from rank WHERE RankName ='A');";
if($result = mysqli_query($connexion,$requete)){
    while ($tuple = mysqli_fetch_assoc($result)){
        array_push($tabmissionA, $tuple['IDMission']);
    }
}
else{

}
for($i =0 ;$i < $totalquartier ; ++$i)
{
    if($i !=0){
        $rand = rand(0,100);
        if($rand < 5){
	    echo "<br>".sizeof($tabmissionA)."taille du premier tableau <br>";           
	    $tabmissionA2 = array_values($tabmissionA);
            $randmissionA = rand(0,sizeof($tabmissionA2)-1);
            $requete = "INSERT INTO npc_present SELECT IDNPCharacter,IDMission from np_character,mission where IDNPCharacter IN (Select IDNPCharacter from association_district_npc WHERE IDDistrict =".$i.") AND IDMission =".$tabmissionA2[$randmissionA].";";
            if($result = mysqli_query($connexion,$requete)){
                unset($tabmissionA[$randmissionA]);
                echo "ça a marché <br><br>";
            }
            else{
                echo "ça a pas marché l'insertion de npc<br><br>";
            }
        }
    }
}

//Génère et récupère le nombre de missions par quartier
$totaldemissions = 0;
$tabNbMissionParQuartier = array();
for($i = 0 ; $i<$totalquartier; ++$i)
{
    if($i ==0){
        $randomNbMission = 0;
    }
    else{
        $randomNbMission = random_int(1,4);
    }
    $tabNbMissionParQuartier[$i] = $randomNbMission;
    echo "Nombre de missions = ".$randomNbMission." pour le quartier ->".$i."<br><br>";
    $totaldemissions += $randomNbMission;
}

$missiondejala = array();

for($i=0; $i<sizeof($tabNbMissionParQuartier) ; ++$i)
{
    while ($tabNbMissionParQuartier[$i]>0)
    {
        $dejala = false;
        $randMission = rand(0,sizeof($listeMissions));
        $missionchoisi = $listeMissions[$randMission];
        for ($j = 0; $j<sizeof($missiondejala); ++$j)
        {
            if($missionchoisi == $missiondejala[$j]){
                $dejala = true;
                break;
            }
        }
        if ($dejala == true)
        {
            continue;
        }
        else
        {
            array_push($missiondejala,$missionchoisi);
            $identreprise = trouverUneEntreprise($i,$host,$login,$password,$database);
            $requete = "INSERT INTO present_missions(IDDistrict,IDMission,IDCompany) VALUE (".$i.",".$missionchoisi.",".$identreprise.");";
            if($result = mysqli_query($connexion,$requete)){
                echo "ça a marché <br><br>";
            }
            else{
                echo "ça a pas marché <br><br>";
            }
            $tabNbMissionParQuartier[$i] -= 1;
        }
    }
}

function trouverUneEntreprise($idquartier,$host,$login,$password,$database){
    $connexion = mysqli_connect($host,$login,$password,$database);
    $tableauentreprise = array();
    $randomtailleentreprise = rand(0,100);
    $tailleEntreprise=0;
    if($randomtailleentreprise<50){
        $tailleEntreprise=1;
    }
    elseif($randomtailleentreprise>94){
        $tailleEntreprise=4;
    }
    else{
        $tailleEntreprise=2;
    }
    $requete = "SELECT * from association_company_district WHERE IDDistrict =".$idquartier." AND IDCompany IN (SELECT IDCompany From company WHERE Size =".$tailleEntreprise.")";
    if($result = mysqli_query($connexion,$requete)){
        echo "ça a marché la recherche d'entreprise<br><br>";
        while ($tuple = mysqli_fetch_assoc($result)){
            array_push($tableauentreprise, $tuple['IDCompany']);
        }
        $identrepriserandom = $tableauentreprise[rand(0,sizeof($tableauentreprise)-1)];
        return $identrepriserandom;
    }
    else{
        echo "la recherche d'entreprise a pas marché :'(<br><br>";
        return 0;
    }
}
