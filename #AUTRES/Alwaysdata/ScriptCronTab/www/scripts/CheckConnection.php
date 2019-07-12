<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get"},
 *     path="/CheckConnection/{pseudo}{pass}",
 *     description="Vérification connexion d'un utilisateur",
 *     @OA\Parameter(
 *          name="pseudo",
 *          in="path",
 *          description="Pseudo du joueur",
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

    if (isset($_GET["pseudo"]) && isset($_GET["pass"])) {
        $pseudo = $_GET["pseudo"];
        $pass = $_GET["pass"];
        echo $helper->CheckConnexion($pseudo, $pass);
    }