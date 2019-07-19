<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupTopicsAide/",
 *     description="Récupère les Topics du menu Aide"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupTopicsAide();