<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupSpeEntreprise/{id}",
 *     description="Récupère les spécialisations des entreprises",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant de l'entreprise",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"])) {
    $id = $_GET["id"];
    echo $helper->RecupSpeEntreprise($id);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Veuillez saisir un id d'entreprise."
    ));
}