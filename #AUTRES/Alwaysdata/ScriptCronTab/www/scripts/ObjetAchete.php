<?php

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/ObjetAchete/{id}{idObjet}",
 *     description="Ajoute un objet dans les objets achetés d'un joueur",
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
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idObjet"])) {
    $id = $_GET["id"];
    $idObjet = $_GET["idObjet"];
    echo $helper->ObjetAchete($id, $idObjet);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idObjet non renseignés"
    ));
}