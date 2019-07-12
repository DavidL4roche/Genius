<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/GetAmi/{nomAmi}",
 *     description="Récupère les informations d'un ami",
 *     @OA\Parameter(
 *          name="nomAmi",
 *          in="path",
 *          description="Nom de l'ami",
 *          required=true,
 *          @OA\Schema(type="String"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["nomAmi"])) {
    $nomAmi = $_GET["nomAmi"];
    echo $helper->GetAmi($nomAmi);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs nomAmi non renseignés"
    ));
}