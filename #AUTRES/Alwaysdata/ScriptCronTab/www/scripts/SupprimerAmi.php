<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Delete(
 *     tags={"Delete"},
 *     path="/SupprimerAmi/{id}{idAmi}",
 *     description="Supprimer l'ami d'un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idAmi",
 *          in="path",
 *          description="Identifiant de l'ami",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idAmi"])) {
    $id = $_GET["id"];
    $idAmi = $_GET["idAmi"];
    echo $helper->SupprimerAmi($id, $idAmi);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idami non renseignés"
    ));
}