<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupBonus/",
 *     description="Récupère les bonus"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupBonus();