<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupEntreprise/",
 *     description="Récupère les Entreprises"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupEntreprise();