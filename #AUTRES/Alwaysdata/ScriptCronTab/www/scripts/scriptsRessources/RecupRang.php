<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupRang/",
 *     description="Récupère les Rangs"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupRang();