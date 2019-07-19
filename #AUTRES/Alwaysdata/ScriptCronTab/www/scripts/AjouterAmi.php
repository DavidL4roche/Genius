<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/AjouterAmi/{id}{idAmi}",
 *     description="Ajoute un ami à un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idAmi",
 *          in="path",
 *          description="Identifiant de l'ami à ajouter",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idAmi"])) {
    $id = $_GET["id"];
    $idAmi = $_GET["idAmi"];
    echo $helper->AjouterAmi($id, $idAmi);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idami non renseignés"
    ));
}