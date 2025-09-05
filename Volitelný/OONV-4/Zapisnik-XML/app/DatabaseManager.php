<?php

function ConnectToDatabase($name = "ujepRpg") {
    $mysqli= new mysqli("localhost", "root", "", $name);
    $mysqli->query("Set names utf8");
    if ($mysqli->connect_error) {
        echo $mysqli->connect_error;
        return;
    }
}



?>