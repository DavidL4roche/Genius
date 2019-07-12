<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Update"},
 *     path="/ChangePlayerStats/{stat}{value}{id}",
 *     description="Change la stat d'un joueur",
 *     @OA\Parameter(
 *          name="stat",
 *          in="path",
 *          description="Statistique à changer",
 *          required=true,
 *          @OA\Schema(type="string")),
 *     @OA\Parameter(
 *          name="value",
 *          in="path",
 *          description="Valeur de la stat à ajouter",
 *          required=true,
 *          @OA\Schema(type="Integer")),
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

    if (isset($_GET["stat"]) && isset($_GET["value"]) && isset($_GET["id"])) {
        $stat = $_GET["stat"];

        if ($stat == "Password") {
            $value = password_hash($_GET["value"], PASSWORD_DEFAULT);
        }
        else {
            $value = $_GET["value"];
        }

        $id = $_GET["id"];
        echo $helper->SetPlayerStat($stat, $value, $id);
    }