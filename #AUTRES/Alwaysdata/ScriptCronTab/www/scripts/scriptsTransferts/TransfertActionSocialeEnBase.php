<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update", "Transfert en base"},
 *     path="/TransfertActionSocialeEnBase/{id}{idAmi}{type}",
 *     description="Actualise l'action sociale d'un joueur",
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
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="type",
 *          in="path",
 *          description="Type d'action sociale (ITEM/SKILL)",
 *          required=true,
 *          @OA\Schema(type="String"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idAmi"]) && isset($_GET["type"])) {
    $id = $_GET["id"];
    $idAmi = $_GET["idAmi"];
    $type = $_GET["type"];
    echo $helper->TransfertActionSocialeEnBase($id, $idAmi, $type);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}