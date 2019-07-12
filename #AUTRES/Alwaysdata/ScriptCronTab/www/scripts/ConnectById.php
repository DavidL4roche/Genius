<?php

use OpenApi\Annotations as OA;

/**
 * @OA\(
 *     tags={"Get"},
 *     path="/ConnectById/{id}",
 *     description="Connection par id",
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
        echo $helper->connectById($id);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Adresse IP non valide"
        ));
    }
