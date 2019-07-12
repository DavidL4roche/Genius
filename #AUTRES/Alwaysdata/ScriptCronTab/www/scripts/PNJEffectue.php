<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/PNJEffectue/{idJoueur}{idArt}",
 *     description="Insère un PNJ effectué par le joueur (Artéfact)",
 *     @OA\Parameter(
 *          name="idJoueur",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idArt",
 *          in="path",
 *          description="Identifiant de l'artéfact",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idJoueur"]) && isset($_GET["idArt"])) {
    $idJoueur = $_GET["idJoueur"];
    $idArt = $_GET["idArt"];
    echo $helper->PNJEffectue($idJoueur, $idArt);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idExamen non renseignés"
    ));
}