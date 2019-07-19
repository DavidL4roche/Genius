<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupDiplome/",
 *     description="Récupère les Diplômes"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupDiplome();