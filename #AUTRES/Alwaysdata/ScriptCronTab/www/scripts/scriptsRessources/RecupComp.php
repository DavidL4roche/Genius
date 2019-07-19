<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupComp/",
 *     description="Récupère les Compétences"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupComp();