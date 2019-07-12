<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Mail"},
 *     path="/EnvoiMail/{mail}",
 *     description="Envoi d'un mail personnalisé à un destinataire",
 *     @OA\Parameter(
 *          name="mail",
 *          in="path",
 *          description="Mail du destinataire",
 *          required=true,
 *          @OA\Schema(type="String"))
 * )
 */

require_once "Helper.php";

$helper = new Helper();

if (isset($_GET["mail"])) {
    $mail = $_GET["mail"];
    $helper->sendMail($mail);
}