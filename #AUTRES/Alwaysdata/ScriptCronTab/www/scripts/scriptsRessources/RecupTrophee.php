<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupTrophee/",
 *     description="Récupère les Trophées"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupTrophee();