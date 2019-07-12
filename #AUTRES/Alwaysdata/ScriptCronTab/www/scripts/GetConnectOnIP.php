<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/GetConnectOnIP/{ip}",
 *     description="Renvoie l'attribut isConnected pour un id de joueur donné",
 *     @OA\Parameter(
 *          name="ip",
 *          in="path",
 *          description="IP du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["ip"])) {
        $ip = $_GET["ip"];
        echo $helper->getConnectOnIP($ip);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Champ ip non renseigné"
        ));
    }