<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupGain/",
 *     description="Récupère les Gains"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupGain();