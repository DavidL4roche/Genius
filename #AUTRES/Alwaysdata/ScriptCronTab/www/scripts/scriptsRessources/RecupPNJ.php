<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupPNJ/",
 *     description="Récupère les PNJ"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupPNJ();