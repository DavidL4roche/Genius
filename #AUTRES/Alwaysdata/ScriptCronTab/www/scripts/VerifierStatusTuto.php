<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/VerifierStatusTuto/{idTuto}{idPlayer}",
 *     description="Verifie le status d'un tuto pour un joueur",
 *     @OA\Parameter(
 *          name="idTuto",
 *          in="path",
 *          description="Identifiant du tutoriel",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idPlayer",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idTuto"]) && isset($_GET["idPlayer"])) {

    $idTuto = $_GET["idTuto"];
    $idPlayer = $_GET["idPlayer"];

    echo $helper->VerifierStatusTuto($idTuto, $idPlayer);
}