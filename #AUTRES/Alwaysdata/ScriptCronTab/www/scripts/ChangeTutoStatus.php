<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update"},
 *     path="/ChangeTutoStatus/{idTuto}{idPlayer}{status}",
 *     description="Change le status d'un tuto",
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
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="status",
 *          in="path",
 *          description="Status du tuto (0, 1)",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["idTuto"]) && isset($_GET["idPlayer"]) && isset($_GET["status"])) {

    $idTuto = $_GET["idTuto"];
    $idPlayer = $_GET["idPlayer"];
    $status = $_GET["status"];

    echo $helper->ChangeTutoStatus($idTuto, $idPlayer, $status);
}