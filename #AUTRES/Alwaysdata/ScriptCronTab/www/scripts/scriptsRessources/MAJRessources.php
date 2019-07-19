<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "MAJ Joueur"},
 *     path="/MAJRessources/{id}",
 *     description="Récupère les ressources d'un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"])) {
    $id = $_GET["id"];
    echo $helper->MAJRessources($id);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Veuillez saisir un id de joueur."
    ));
}