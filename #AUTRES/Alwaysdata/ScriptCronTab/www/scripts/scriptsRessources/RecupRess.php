<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupRess/",
 *     description="Récupère les Ressources"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupRess();