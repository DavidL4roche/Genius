<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update", "Transfert en base"},
 *     path="/TransfertCompetencesEnBase/{id}{idComp}{valeur}",
 *     description="Actualise la compétence d'un joueur",
 *     @OA\Parameter(
 *          name="id",
 *          in="path",
 *          description="Identifiant du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="idComp",
 *          in="path",
 *          description="Identifiant de la compétence",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="valeur",
 *          in="path",
 *          description="Valeur de la compétence",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

if (isset($_GET["id"]) && isset($_GET["idComp"]) && isset($_GET["valeur"])) {
    $id = $_GET["id"];
    $idComp = $_GET["idComp"];
    $valeur = $_GET["valeur"];
    echo $helper->TransfertCompetencesEnBase($id, $idComp, $valeur);
}
else {
    echo json_encode(array(
        "result" => false,
        "msg" => "Champ id non renseigné"
    ));
}