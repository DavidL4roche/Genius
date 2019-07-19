<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupMission/",
 *     description="Récupère les Missions"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupMission();