<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/MissionDivertEffectuee/{idJoueur}{idMission}",
 *     description="Insère une mission divertissement effectuée par le joueur",
 *     @OA\Parameter(
 *          name="idJoueur",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idMission",
 *          in="path",
 *          description="Identifiant de la mission",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idJoueur"]) && isset($_GET["idMission"])) {
    $idJoueur = $_GET["idJoueur"];
    $idMission = $_GET["idMission"];
    echo $helper->MissionDivertEffectuee($idJoueur, $idMission);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idArt non renseignés"
    ));
}