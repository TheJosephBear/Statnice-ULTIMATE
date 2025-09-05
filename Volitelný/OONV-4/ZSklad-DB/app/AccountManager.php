<?php
require_once 'DatabaseManager.php';

class AccountManager {

    private $db;

    public function __construct() {
        $manager = new DatabaseManager();
        $this->db = $manager->ConnectToDatabase();
    }

    public function register($username, $password) {
        if (!$this->db) {
            return "Database connection error.";
        }

        $stmt = $this->db->prepare("SELECT id FROM uzivatele WHERE jmeno = ?");
        $stmt->bind_param("s", $username);
        $stmt->execute();
        $stmt->store_result();

        if ($stmt->num_rows > 0) {
            return "Username already taken.";
        }
        $stmt->close();

        // Insert new user
        $hashedPassword = password_hash($password, PASSWORD_DEFAULT);
        $stmt = $this->db->prepare("INSERT INTO uzivatele (jmeno, heslo) VALUES (?, ?)");
        $stmt->bind_param("ss", $username, $hashedPassword);
        $stmt->execute();

        return $stmt->affected_rows > 0 ? "Registration successful." : "Error during registration.";
    }

    function login($username, $password) {
        if (!$this->db) {
            return "Database connection error.";
        }
        
        $stmt = $this->db->prepare("SELECT id, jmeno, heslo FROM uzivatele WHERE jmeno = ?");
        $stmt->bind_param("s", $username);
        $stmt->execute();
        $result = $stmt->get_result();
        $user = $result->fetch_assoc();

        if ($user && password_verify($password, $user['heslo'])) {
            return $user;
        }
        return false;
    }



}


?>