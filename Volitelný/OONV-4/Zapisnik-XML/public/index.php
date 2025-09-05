<?php
session_start(); // Start session for user tracking

// Handle form submission
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    require_once __DIR__ . '/../app/XMLDatabaseManager.php';
    $db = new XMLDatabaseManager();

    if (isset($_POST['login'])) {
        $username = $_POST['username'] ?? '';
        $password = $_POST['password'] ?? '';
        if ($db->login($username, $password)) {
            $_SESSION['logged_user'] = $username; // Store logged in user
            header('Location: dashboard.php');
            exit;
        } else {
            $error = "Invalid username or password.";
        }
    } elseif (isset($_POST['register'])) {
        $username = $_POST['username'] ?? '';
        $password = $_POST['password'] ?? '';
        $email = ''; // Add email field to your form and logic if needed
        if ($db->register($username, $password, $email)) {
            $success = "Registration successful. You can now log in.";
        } else {
            $error = "Registration failed. Username or email may already exist.";
        }
    }
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Login / Registration</title>
    <link rel="stylesheet" href="css/style.css">
</head>
<body>
    <?php if (!empty($error)): ?>
        <div class="error"><?= htmlspecialchars($error) ?></div>
    <?php endif; ?>
    <?php if (!empty($success)): ?>
        <div class="success"><?= htmlspecialchars($success) ?></div>
    <?php endif; ?>

    <button id="toggleFormBtn" type="button">Registration</button>

    <div id="loginForm">
        <h2>Login</h2>
        <form method="post">
            <label for="login-username">Username:</label>
            <input type="text" id="login-username" name="username" required>
            <br>
            <label for="login-password">Password:</label>
            <input type="password" id="login-password" name="password" required>
            <br>
            <button type="submit" name="login">Login</button>
        </form>
    </div>

    <div id="registerForm" style="display:none;">
        <h2>Register</h2>
        <form method="post">
            <label for="register-username">Username:</label>
            <input type="text" id="register-username" name="username" required>
            <br>
            <label for="register-password">Password:</label>
            <input type="password" id="register-password" name="password" required>
            <br>
            <button type="submit" name="register">Register</button>
        </form>
    </div>

    <script>
        const toggleBtn = document.getElementById('toggleFormBtn');
        const loginForm = document.getElementById('loginForm');
        const registerForm = document.getElementById('registerForm');

        toggleBtn.addEventListener('click', () => {
            if (loginForm.style.display !== 'none') {
                loginForm.style.display = 'none';
                registerForm.style.display = 'block';
                toggleBtn.textContent = 'Login';
            } else {
                loginForm.style.display = 'block';
                registerForm.style.display = 'none';
                toggleBtn.textContent = 'Registration';
            }
        });
    </script>