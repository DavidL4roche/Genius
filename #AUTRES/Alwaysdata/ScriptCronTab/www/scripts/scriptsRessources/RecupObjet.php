<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupObjet/",
 *     description="Récupère les Objets"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupObjet();