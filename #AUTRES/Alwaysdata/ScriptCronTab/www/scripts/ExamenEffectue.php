<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/ExamenEffectue/{idJoueur}{idExamen}",
 *     description="Insère un examen effectué par le joueur",
 *     @OA\Parameter(
 *          name="idJoueur",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idExamen",
 *          in="path",
 *          description="Identifiant de l'examen",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idJoueur"]) && isset($_GET["idExamen"])) {
    $idJoueur = $_GET["idJoueur"];
    $idExamen = $_GET["idExamen"];
    echo $helper->ExamenEffectue($idJoueur, $idExamen);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idExamen non renseignés"
    ));
}