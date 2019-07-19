<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupDuree/",
 *     description="Récupère les Durées"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupDuree();