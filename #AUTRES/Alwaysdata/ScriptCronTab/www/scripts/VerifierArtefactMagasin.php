<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/VerifierArtefactMagasin/{id}",
 *     description="Verifie les objets Artefacts du Magasin",
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
    $id = $_GET["id"];
    echo $helper->VerifierArtefactMagasin($id);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champs id non renseigné"
    ));
}