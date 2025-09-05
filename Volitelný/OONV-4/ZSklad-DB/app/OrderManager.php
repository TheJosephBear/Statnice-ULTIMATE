<?php
require_once 'DatabaseManager.php';

class OrderManager {

    private $db;

    public function __construct() {
        $manager = new DatabaseManager();
        $this->db = $manager->ConnectToDatabase();
    }

    public function GetAllOrders() {
        if (!$this->db) {
            return "Database connection error.";
        }

        $stmt = $this->db->prepare("SELECT ID, ID_vyrizujici, ID_zakaznik, datum_zalozeni FROM objednavky");
        $stmt->execute();
        $result = $stmt->get_result();
        $orders = [];

        while ($order = $result->fetch_assoc()) {
    // Employee name
    $empStmt = $this->db->prepare("SELECT jmeno FROM uzivatele WHERE id = ?");
    $empStmt->bind_param("i", $order['ID_vyrizujici']);
    $empStmt->execute();
    $empRes = $empStmt->get_result()->fetch_assoc();
    $order['vyrizujici'] = $empRes ? $empRes['jmeno'] : "N/A";
    $empStmt->close();

    // Customer name
    $custStmt = $this->db->prepare("SELECT jmeno FROM zakaznici WHERE id = ?");
    $custStmt->bind_param("i", $order['ID_zakaznik']);
    $custStmt->execute();
    $custRes = $custStmt->get_result()->fetch_assoc();
    $order['zakaznik'] = $custRes ? $custRes['jmeno'] : "N/A";
    $custStmt->close();

    // Order contents (list of products in the order)
    $contentStmt = $this->db->prepare("SELECT id_produkt FROM obsah_objednavky WHERE id_objednavka = ?");
    $contentStmt->bind_param("i", $order['ID']);
    $contentStmt->execute();
    $contentRes = $contentStmt->get_result();
    $order['produkty'] = [];

    while ($row = $contentRes->fetch_assoc()) {
        // Get product info
        $prodStmt = $this->db->prepare("SELECT nazev, cena FROM produkty WHERE ID = ?");
        $prodStmt->bind_param("i", $row['id_produkt']);
        $prodStmt->execute();
        $prodRes = $prodStmt->get_result()->fetch_assoc();
        $prodStmt->close();

        // Get stock count (pocet_kusu) from sklad
        $skladStmt = $this->db->prepare("SELECT pocet_kusu FROM sklad WHERE id_produkt = ?");
        $skladStmt->bind_param("i", $row['id_produkt']);
        $skladStmt->execute();
        $skladRes = $skladStmt->get_result()->fetch_assoc();
        $skladStmt->close();

        $order['produkty'][] = [
            "nazev" => $prodRes ? $prodRes['nazev'] : "Unknown",
            "cena"  => $prodRes ? $prodRes['cena'] : 0,
            "pocet" => $skladRes ? $skladRes['pocet_kusu'] : 0
        ];
    }
    $contentStmt->close();

    $orders[] = $order;
}
$stmt->close();

return $orders;
    }

    public function AddOrder($idZakaznik, $idVyrizujici, $produkty) {
        if (!$this->db) {
            return "Database connection error.";
        }

        // Insert into objednavky
        $stmt = $this->db->prepare("INSERT INTO objednavky (ID_vyrizujici, ID_zakaznik, datum_zalozeni) VALUES (?, ?, NOW())");
        $stmt->bind_param("ii", $idVyrizujici, $idZakaznik);
        $stmt->execute();
        $orderId = $stmt->insert_id;
        $stmt->close();

        // Insert each product into obsah_objednavky
        $stmt = $this->db->prepare("INSERT INTO obsah_objednavky (id_objednavka, id_produkt, pocet_kusu) VALUES (?, ?, ?)");
        foreach ($produkty as $prod) {
            $stmt->bind_param("iii", $orderId, $prod['id_produkt'], $prod['pocet']);
            $stmt->execute();
        }
        $stmt->close();

        return "Order added successfully.";
    }

    public function UpdateOrder($id, $idZakaznik, $idVyrizujici, $produkty) {
        if (!$this->db) {
            return "Database connection error.";
        }

        // Update order header
        $stmt = $this->db->prepare("UPDATE objednavky SET ID_vyrizujici = ?, ID_zakaznik = ? WHERE ID = ?");
        $stmt->bind_param("iii", $idVyrizujici, $idZakaznik, $id);
        $stmt->execute();
        $stmt->close();

        // Clear existing products
        $stmt = $this->db->prepare("DELETE FROM obsah_objednavky WHERE id_objednavka = ?");
        $stmt->bind_param("i", $id);
        $stmt->execute();
        $stmt->close();

        // Reinsert updated products
        $stmt = $this->db->prepare("INSERT INTO obsah_objednavky (id_objednavka, id_produkt, pocet_kusu) VALUES (?, ?, ?)");
        foreach ($produkty as $prod) {
            $stmt->bind_param("iii", $id, $prod['id_produkt'], $prod['pocet']);
            $stmt->execute();
        }
        $stmt->close();

        return "Order updated successfully.";
    }

    public function DeleteOrder($id) {
        if (!$this->db) {
            return "Database connection error.";
        }

        // Delete contents
        $stmt = $this->db->prepare("DELETE FROM obsah_objednavky WHERE id_objednavka = ?");
        $stmt->bind_param("i", $id);
        $stmt->execute();
        $stmt->close();

        // Delete main order
        $stmt = $this->db->prepare("DELETE FROM objednavky WHERE ID = ?");
        $stmt->bind_param("i", $id);
        $stmt->execute();
        $stmt->close();

        return "Order deleted successfully.";
    }
}
?>
