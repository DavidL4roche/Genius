<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupDivert/",
 *     description="Récupère les Divertissements"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupDivert();