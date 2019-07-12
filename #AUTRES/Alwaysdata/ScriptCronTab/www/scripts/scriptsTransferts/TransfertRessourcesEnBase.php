<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update", "Transfert en base"},
 *     path="/TransfertRessourcesEnBase/{id}{idRess}{valeur}",
 *     description="Actualise l'action sociale d'un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idRess",
 *          in="path",
 *          description="Identifiant de la ressource",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="valeur",
 *          in="path",
 *          description="Valeur de la ressource",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idRess"]) && isset($_GET["valeur"])) {
    $id = $_GET["id"];
    $idRess = $_GET["idRess"];
    $valeur = $_GET["valeur"];
    echo $helper->TransfertRessourcesEnBase($id, $idRess, $valeur);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}