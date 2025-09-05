<?php
require_once 'DatabaseManager.php';

class StockManager {

    private $db;

    public function __construct() {
        $manager = new DatabaseManager();
        $this->db = $manager->ConnectToDatabase();
    }

    public function GetAllItems() {
        if (!$this->db) {
            return "Database connection error.";
        }

        $stmt = $this->db->prepare("SELECT ID, nazev, cena FROM produkty");
        $stmt->execute();
        $result = $stmt->get_result();
        $items = [];

        while ($item = $result->fetch_assoc()) {
            $skladStmt = $this->db->prepare("SELECT pocet_kusu FROM sklad WHERE id_produkt = ?");
            $skladStmt->bind_param("i", $item['ID']);
            $skladStmt->execute();
            $skladResult = $skladStmt->get_result();
            $skladData = $skladResult->fetch_assoc();
            $item['pocet'] = $skladData ? $skladData['pocet_kusu'] : 0;
            $items[] = $item;
            $skladStmt->close();
        }
        $stmt->close();

        return $items;
    }

    public function AddItem($nazev, $cena, $pocet) {
        if (!$this->db) {
            return "Database connection error.";
        }

        // Insert into produkty table
        $stmt = $this->db->prepare("INSERT INTO produkty (nazev, cena) VALUES (?, ?)");
        $stmt->bind_param("sd", $nazev, $cena);
        $stmt->execute();
        $produktId = $stmt->insert_id;
        $stmt->close();

        // Insert into sklad table
        $stmt = $this->db->prepare("INSERT INTO sklad (id_produkt, pocet_kusu) VALUES (?, ?)");
        $stmt->bind_param("ii", $produktId, $pocet);
        $stmt->execute();
        $stmt->close();

        return "Item added successfully.";
    }

    public function UpdateItem($id, $nazev, $cena, $pocet) {
        if (!$this->db) {
            return "Database connection error.";
        }

        // Update produkty table
        $stmt = $this->db->prepare("UPDATE produkty SET nazev = ?, cena = ? WHERE ID = ?");
        $stmt->bind_param("sdi", $nazev, $cena, $id);
        $stmt->execute();
        $stmt->close();

        // Update sklad table
        $stmt = $this->db->prepare("UPDATE sklad SET pocet_kusu = ? WHERE id_produkt = ?");
        $stmt->bind_param("ii", $pocet, $id);
        $stmt->execute();
        $stmt->close();

        return "Item updated successfully.";
    }

    public function DeleteItem($id) {
        if (!$this->db) {
            return "Database connection error.";
        }

        // Delete from sklad table
        $stmt = $this->db->prepare("DELETE FROM sklad WHERE id_produkt = ?");
        $stmt->bind_param("i", $id);
        $stmt->execute();
        $stmt->close();

        // Delete from produkty table
        $stmt = $this->db->prepare("DELETE FROM produkty WHERE ID = ?");
        $stmt->bind_param("i", $id);
        $stmt->execute();
        $stmt->close();

        return "Item deleted successfully.";
    }


}


?>