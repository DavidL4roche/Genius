<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/CheckFirstConnection/{id}",
 *     description="Vérifie si on lance le tutoriel ou non",
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
        echo $helper->checkFirstConnection($id);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Adresse IP non valide"
        ));
    }
