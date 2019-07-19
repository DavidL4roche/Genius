<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update"},
 *     path="/UpdateDateCo/{id}",
 *     description="Actualise l'attribut LastConnection d'un Joueur donné",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"])) {
    $ip = $_GET["id"];
    echo $helper->updateDateCo($ip);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}