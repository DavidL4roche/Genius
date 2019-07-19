<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Création"},
 *     path="/CreateUser/{pseudo}{mail}{pass}",
 *     description="Création d'un utilisateur dans la base",
 *     @OA\Parameter(
 *          name="connect",
 *          in="path",
 *          description="Pseudo du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="mail",
 *          in="path",
 *          description="Mail du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer")),
 *     @OA\Parameter(
 *          name="pass",
 *          in="path",
 *          description="Mot de passe du joueur",
 *          required=true,
 *          @OA\Schema(type="Integer"))
 * )
 */

require_once "Helper.php";

    $helper = new Helper();

    if (isset($_GET["pseudo"]) && isset($_GET["mail"]) && isset($_GET["pass"])) {
        $pseudo = $_GET["pseudo"];
        $mail = $_GET["mail"];
        $pass = $_GET["pass"];

        echo $helper->CreateUser($pseudo, $mail, $pass);
    }
    else {
        echo "Erreur, veuillez saisir tous les champs";
    }