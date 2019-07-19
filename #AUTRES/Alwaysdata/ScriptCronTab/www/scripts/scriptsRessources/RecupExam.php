<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Get(
 *     tags={"Get", "Récupération données"},
 *     path="/RecupExam/",
 *     description="Récupère les Examens"
 * )
 */

require_once "../Helper.php";

$helper = new Helper();

echo $helper->RecupExam();