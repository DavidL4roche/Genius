<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupLieu/",
 *     description="Récupère les Lieux"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupLieu();