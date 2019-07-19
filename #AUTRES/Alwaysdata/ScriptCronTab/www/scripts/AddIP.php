<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création", "Update"},
 *     path="/AddIP/{ip}{playerId}",
 *     description="Relie une adresse IP à un identifiant de joueur",
 *     @OA\Parameter(
 *          name="ip",
 *          in="path",
 *          description="IP du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="playerId",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

    require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["ip"]) && isset($_GET["playerId"])) {
        $id = $_GET["ip"];
        $playerId = $_GET["playerId"];
        echo $helper->addIP($id, $playerId);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Champs ip et playerId non renseignés"
        ));
    }