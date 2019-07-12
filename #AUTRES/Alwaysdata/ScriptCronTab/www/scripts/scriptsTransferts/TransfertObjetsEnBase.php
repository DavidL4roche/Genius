<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update", "Transfert en base"},
 *     path="/TransfertObjetsEnBase/{id}{idObjet}{type}",
 *     description="Actualise le nombre d'objets d'un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idObjet",
 *          in="path",
 *          description="Identifiant de l'objet",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="type",
 *          in="path",
 *          description="Nombre d'objets du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idObjet"]) && isset($_GET["valeur"])) {
    $id = $_GET["id"];
    $idObjet = $_GET["idObjet"];
    $valeur = $_GET["valeur"];
    echo $helper->TransfertObjetsEnBase($id, $idObjet, $valeur);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}