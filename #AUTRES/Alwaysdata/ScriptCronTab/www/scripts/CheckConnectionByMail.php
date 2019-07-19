<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/CheckConnectionByMail/{mail}{pass}",
 *     description="Vérification connexion par mail d'un utilisateur",
 *     @OA\Parameter(
 *          name="pseudo",
 *          in="path",
 *          description="Mail du joueur",
 *          required=true,
 *          @OA\Schema(type="String")),
 *     @OA\Parameter(
 *          name="pass",
 *          in="path",
 *          description="Mot de passe du joueur",
 *          required=true,
 *          @OA\Schema(type="String"))
 * )
 */

require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["mail"]) && isset($_GET["pass"])) {
        $mail = $_GET["mail"];
        $pass = $_GET["pass"];
        echo $helper->CheckConnexionByMail($mail, $pass);
    }