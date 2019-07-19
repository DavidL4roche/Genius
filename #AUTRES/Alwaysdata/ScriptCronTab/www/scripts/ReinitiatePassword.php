<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Mail"},
 *     path="/ReinitiatePassword/{mail}",
 *     description="Réinitialise le mot de passe",
 *     @OA\Parameter(
 *          name="mail",
 *          in="path",
 *          description="Mail du joueur",
 *          required=true,
 *          @OA\Schema(type="String"))
 * )
 */

require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["mail"]) && $_GET["mail"] != "") {
        $mail = $_GET["mail"];
        echo $helper->reinitiatePassword($mail);
    }
    else {
        echo json_encode(array(
            "result" => false,
            "msg" => "Veuillez saisir un mail."
        ));
    }