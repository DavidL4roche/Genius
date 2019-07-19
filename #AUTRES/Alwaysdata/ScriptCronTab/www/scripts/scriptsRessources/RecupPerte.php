<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupPerte/",
 *     description="Récupère les Pertes"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupPerte();