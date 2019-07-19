<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/CheckIP/{id}{ip}",
 *     description="Vérifier l'IP et renvoyer le compte correspondant",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
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

    if (isset($_GET["ip"]) && $_GET["ip"] != "") {
        $ip = $_GET["ip"];
        echo $helper->checkIP($ip);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Adresse IP non valide"
        ));
    }