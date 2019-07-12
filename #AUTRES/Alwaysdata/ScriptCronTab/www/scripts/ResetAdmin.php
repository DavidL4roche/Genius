<?php

use OpenApi\Annotations as OA;

/**
 * @OA\Put(
 *     tags={"Admin"},
 *     path="/ResetAdmin/",
 *     description="Reset l'admin (lui mets les valeurs de base pour test)"
 * )
 */

require_once "Helper.php";

$helper = new Helper();
echo $helper->resetAdmin();