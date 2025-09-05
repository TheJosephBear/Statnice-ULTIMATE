<?php

class DatabaseManager {

    function ConnectToDatabase($name = "barvySro") {
        $mysqli= new mysqli("localhost", "root", "", $name);
        $mysqli->query("Set names utf8");
        if ($mysqli->connect_error) {
            echo $mysqli->connect_error;
            return;
        }
        return $mysqli;
    }
}

?>