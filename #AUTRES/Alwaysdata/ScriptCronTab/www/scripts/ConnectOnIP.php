<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update"},
 *     path="/ConnectOnIP/{connect}{playerId}",
 *     description="Change l'attribut isConnected pour un id de joueur donné",
 *     @OA\Parameter(
 *          name="connect",
 *          in="path",
 *          description="Booléen estConnecte",
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

    if (isset($_GET["connect"]) && isset($_GET["playerId"])) {
        $connect = $_GET["connect"];
        $playerId = $_GET["playerId"];
        echo $helper->connectOnIP($connect, $playerId);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Champs connect et playerId non renseignés"
        ));
    }