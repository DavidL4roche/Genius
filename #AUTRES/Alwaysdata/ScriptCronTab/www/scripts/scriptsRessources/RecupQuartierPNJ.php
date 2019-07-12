<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupQuartierPNJ/{id}",
 *     description="Récupère les quartiers des pnj",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du PNJ",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"])) {
    $id = $_GET["id"];
    echo $helper->RecupQuartierPNJ($id);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Veuillez saisir un id de pnj."
    ));
}