<?php
session_start();

if (!isset($_SESSION['user_id'])) {
    header("Location: index.php");
    exit;
}

require_once __DIR__ . '/../app/StockManager.php';
require_once __DIR__ . '/../app/OrderManager.php';
$manager = new StockManager();
$orderManager = new OrderManager();

// Handle Add Item
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['add_stock'])) {
    $nazev = $_POST['nazev'];
    $pocet = $_POST['pocet'];
    $cena = $_POST['cena'];
    $manager->AddItem($nazev, $pocet, $cena);
    header("Location: dashboard.php");
    exit;
}

// Handle Edit Item
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['edit_stock'])) {
    $id = $_POST['id'];
    $nazev = $_POST['nazev'];
    $pocet = $_POST['pocet'];
    $cena = $_POST['cena'];
    $manager->UpdateItem($id, $nazev, $pocet, $cena);
    header("Location: dashboard.php");
    exit;
}

// Handle Delete Item
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['deleteItem'])) {
    $id = $_POST['id'];
    $manager->DeleteItem($id);
    header("Location: dashboard.php");
    exit;
}

// Handle Add Order
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['add_order'])) {
    $orderData = [
        'nazev' => $_POST['order_nazev'],
        'pocet' => $_POST['order_pocet'],
        'cena' => $_POST['order_cena']
    ];
    $orderManager->AddOrder($orderData);
    header("Location: dashboard.php");
    exit;
}

// Handle Edit Order
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['edit_order'])) {
    $id = $_POST['order_id'];
    $orderData = [
        'nazev' => $_POST['order_nazev'],
        'pocet' => $_POST['order_pocet'],
        'cena' => $_POST['order_cena']
    ];
    $orderManager->UpdateOrder($id, $orderData);
    header("Location: dashboard.php");
    exit;
}

// Handle Delete Order
if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['deleteOrder'])) {
    $id = $_POST['order_id'];
    $orderManager->DeleteOrder($id);
    header("Location: dashboard.php");
    exit;
}
?>
<!DOCTYPE html> 
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Dashboard</title>
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
<div class="main-container">
    <div class="dashboard-container">
        <div class="dashboard-header">
            <div class="dashboard-title">Sklad</div>
            <div class="dashboard-actions">
                <button onclick="showAddPopUp();">Přidat položku</button>
            </div>
            <?php
                $allItems = $manager->GetAllItems();
                foreach ($allItems as $item): 
                    $id = htmlspecialchars($item['ID']);
                    $nazev = htmlspecialchars($item['nazev']);
                    $pocet = htmlspecialchars($item['pocet']);
                    $cena = htmlspecialchars($item['cena']);
            ?>
                    <div class="dashboard-content">
                        <div class="dashboard-panel">
                            <h2><?= $nazev ?></h2>
                            <p>Na skladě kusů: <?= $pocet ?></p>
                            <p>Cena za kus: <?= $cena ?> Kč</p>
                            <?php 
                                if($pocet < 10) {
                                    echo "<p style='color: red; font-weight: bold;'>Nízký stav zásob!</p>";
                                }
                            ?>
                            <button onclick="showEditPopUp('<?= $id ?>', '<?= $nazev ?>', '<?= $pocet ?>', '<?= $cena ?>');">Upravit</button>
                        </div>
                    </div>
            <?php endforeach; ?>
        </div>  
    </div>
    <div class="dashboard-container">
        <div class="dashboard-header">
            <div class="dashboard-title">Objednávky</div>
            <div class="dashboard-actions">
                <button onclick="showAddOrderPopUp();">Přidat objednávku</button>
            </div>
            <div class="dashboard-content">
                <?php
                    $allOrders = $orderManager->GetAllOrders();
                    foreach ($allOrders as $order):
                        $order_id = htmlspecialchars($order['ID'] ?? '');
                        $order_nazev = htmlspecialchars($order["produkty"]["nazev"] ?? '');
                        $order_zakaznik = htmlspecialchars($order["zakaznik"] ?? '');
                        $order_datum =  htmlspecialchars($order["datum_zalozeni"] ?? '');
                ?>
                    <div class="dashboard-panel">
                        <h2><?= $order_nazev ?></h2>
                        <p>Zákazník: <?= $order_zakaznik ?></p>
                        <p>Datum založení: <?= $order_datum ?></p>
                        <button onclick="showEditOrderPopUp('<?= $order_id ?>', '<?= $order_nazev ?>', '<?= $order_zakaznik ?>', '<?= $order_datum ?>');">Upravit</button>
                    </div>
                <?php endforeach; ?>
            </div>
        </div>  
    </div>

<!-- Add Stock PopUp -->
<div class="editPopUp" id="AddStockPopUp" style="display:none;"> 
    <form method="POST">
        <h2>Přidat položku</h2>
        <input type="text" name="nazev" placeholder="Název" required>
        <input type="number" name="pocet" placeholder="Počet" required>
        <input type="number" name="cena" placeholder="Cena" required>
        <div class="popUpActions">
            <button type="submit" name="add_stock">Save</button>
            <button type="button" onclick="hideAddPopUp();">Cancel</button>
        </div>
    </form>
</div>

<!-- Edit Stock PopUp -->
<div class="editPopUp" id="StockPopUp" style="display:none;"> 
    <form method="POST">
        <h2>Upravit položku</h2>
        <input type="hidden" name="id" id="edit_id">
        <input type="text" name="nazev" id="edit_nazev" required>
        <input type="number" name="pocet" id="edit_pocet" required>
        <input type="number" name="cena" id="edit_cena" required>
        <div class="popUpActions">
            <button type="submit" name="edit_stock">Uložit změny</button>
            <button type="button" onclick="hideEditPopUp();">Zrušit</button>
        </div>
    </form>
    <form id="deleteItemForm" method="post" action="" style="margin-top:10px; display:none;">
        <input type="hidden" name="deleteItem" value="1">
        <input type="hidden" name="id" id="edit_del_id">
        <button type="submit" onclick="return confirm('Are you sure you want to delete this task?')" style="background:#e74c3c; color:#fff;">Delete</button>
    </form>
</div>

<!-- Add Order PopUp -->
<div class="editPopUp" id="AddOrderPopUp" style="display:none;"> 
    <form method="POST">
        <h2>Přidat objednávku</h2>
        <input type="text" name="order_nazev" placeholder="Název" required>
        <input type="number" name="order_pocet" placeholder="Počet" required>
        <input type="number" name="order_cena" placeholder="Cena" required>
        <div class="popUpActions">
            <button type="submit" name="add_order">Save</button>
            <button type="button" onclick="hideAddOrderPopUp();">Cancel</button>
        </div>
    </form>
</div>

<!-- Edit Order PopUp -->
<div class="editPopUp" id="OrderPopUp" style="display:none;"> 
    <form method="POST">
        <h2>Upravit objednávku</h2>
        <input type="hidden" name="order_id" id="edit_order_id">
        <input type="text" name="order_nazev" id="edit_order_nazev" required>
        <input type="number" name="order_pocet" id="edit_order_pocet" required>
        <input type="number" name="order_cena" id="edit_order_cena" required>
        <div class="popUpActions">
            <button type="submit" name="edit_order">Uložit změny</button>
            <button type="button" onclick="hideEditOrderPopUp();">Zrušit</button>
        </div>
    </form>
    <form id="deleteOrderForm" method="post" action="" style="margin-top:10px; display:none;">
        <input type="hidden" name="deleteOrder" value="1">
        <input type="hidden" name="order_id" id="edit_del_order_id">
        <button type="submit" onclick="return confirm('Are you sure you want to delete this order?')" style="background:#e74c3c; color:#fff;">Delete</button>
    </form>
</div>

<script>
function showAddPopUp() {
    document.getElementById('AddStockPopUp').style.display = 'flex';
}
function hideAddPopUp() {
    document.getElementById('AddStockPopUp').style.display = 'none';
}
function showEditPopUp(id, nazev, pocet, cena) {
    document.getElementById('edit_id').value = id;
    document.getElementById('edit_del_id').value = id;
    document.getElementById('edit_nazev').value = nazev;
    document.getElementById('edit_pocet').value = pocet;
    document.getElementById('edit_cena').value = cena;
    document.getElementById('StockPopUp').style.display = 'flex';
}
function hideEditPopUp() {
    document.getElementById('StockPopUp').style.display = 'none';
}
function showAddOrderPopUp() {
    document.getElementById('AddOrderPopUp').style.display = 'flex';
}
function hideAddOrderPopUp() {
    document.getElementById('AddOrderPopUp').style.display = 'none';
}
function showEditOrderPopUp(id, nazev, pocet, cena) {
    document.getElementById('edit_order_id').value = id;
    document.getElementById('edit_del_order_id').value = id;
    document.getElementById('edit_order_nazev').value = nazev;
    document.getElementById('edit_order_pocet').value = pocet;
    document.getElementById('edit_order_cena').value = cena;
    document.getElementById('OrderPopUp').style.display = 'flex';
}
function hideEditOrderPopUp() {
    document.getElementById('OrderPopUp').style.display = 'none';
}
hideAddPopUp();
hideEditPopUp();
hideAddOrderPopUp();
hideEditOrderPopUp();
</script>
</div>
</body>
</html>