<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupQuartier/",
 *     description="Récupère les Quartiers"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupQuartier();