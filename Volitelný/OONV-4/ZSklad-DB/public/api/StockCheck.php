<?php
require_once __DIR__ . '/../../app/StockManager.php';

header('Content-Type: application/json; charset=utf-8');

if (!isset($_GET['nazev'])) {
    echo json_encode([
        "error" => "Chybí parametr 'nazev'"
    ]);
    exit;
}

$nazev = $_GET['nazev'];

$manager = new StockManager();
$items = $manager->GetAllItems();

// Najít produkt podle názvu
$found = null;
foreach ($items as $item) {
    if (strcasecmp($item['nazev'], $nazev) === 0) {
        $found = $item;
        break;
    }
}

if (!$found) {
    echo json_encode([
        "produkt" => $nazev,
        "error" => "Produkt nebyl nalezen"
    ]);
    exit;
}

$response = [
    "produkt" => $found['nazev'],
    "skladem" => $found['pocet'],
    "nizky_stav" => $found['pocet'] < 10
];

echo json_encode($response, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT);
