<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update"},
 *     path="/ChangeRessource/{idRessource}{idJoueur}{value}",
 *     description="Change la ressource renseignée pour un joueur donnée",
 *     @OA\Parameter(
 *          name="idRessource",
 *          in="path",
 *          description="Identifiant de la ressource",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idJoueur",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="value",
 *          in="path",
 *          description="Valeur de la ressource à changer",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idRessource"]) && isset($_GET["idJoueur"]) && isset($_GET["value"])) {
    $idRessource = $_GET["idRessource"];
    $idJoueur = $_GET["idJoueur"];
    $value = $_GET["value"];
    echo $helper->SetRessources($idRessource, $idJoueur, $value);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs non renseignés"
    ));
}