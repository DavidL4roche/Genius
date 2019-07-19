<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/ArtefactUtilise/{id}{idArt}",
 *     description="Insert un artefact utilisé par un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idArt",
 *          in="path",
 *          description="Identifiant de l'artefact",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idArt"])) {
    $id = $_GET["id"];
    $idArt = $_GET["idArt"];
    echo $helper->ArtefactUtilise($id, $idArt);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id et idArt non renseignés"
    ));
}