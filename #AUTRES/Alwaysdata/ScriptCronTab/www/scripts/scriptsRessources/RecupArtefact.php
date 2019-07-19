<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupArtefact/",
 *     description="Recupère les artefacts"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupArtefact();