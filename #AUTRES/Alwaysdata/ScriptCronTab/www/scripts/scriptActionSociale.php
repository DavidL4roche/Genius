<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Delete(
 *     tags={"Delete"},
 *     path="/scriptActionSociale/",
 *     description="Suppression de toutes les actions sociales"
 * )
 */

require_once "configuration.php";

//Connexion à la base de Données
$connexion = mysqli_connect($host,$login,$password,$database);

// on supprimer les actions sociales
$requete = "DELETE FROM `action_sociale` WHERE 1";
if($result = mysqli_query($connexion,$requete)){
    echo "Suppression des actions sociales effectuée";
}
else{
    echo "La suppression des actions sociales n'a pas fonctionné";
}