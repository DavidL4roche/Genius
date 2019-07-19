<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Delete(
 *     tags={"Delete"},
 *     path="/scriptartefact/",
 *     description="Suppression de toutes les missions présentes"
 * )
 */

require_once "configuration.php";

//Connexion à la base de Données

$connexion = mysqli_connect($host,$login,$password,$database);

//Supprime toutes les missions présentes
$requete = "DELETE FROM `artefact_used` WHERE 1";
if($result = mysqli_query($connexion,$requete)){
    echo "ça a marché le drop table<br><br>";
}
else{
    echo "ça a pas marché le drop table <br><br>";
}
