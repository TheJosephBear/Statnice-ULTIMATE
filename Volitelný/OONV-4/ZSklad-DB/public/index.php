<?php
session_start();
require_once __DIR__ . '/../app/AccountManager.php';

$accountManager = new AccountManager();

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    if (isset($_POST['try-register'])) {
        $msg = $accountManager->register($_POST['username'], $_POST['password']);
        $error = $msg;
    }

    if (isset($_POST['try-login'])) {
        $user = $accountManager->login($_POST['username'], $_POST['password']);
        if ($user) {
            $_SESSION['user_id'] = $user['id'];
            $_SESSION['username'] = $user['username'];
            header("Location: dashboard.php");
            exit;
        } else {
            $error = "Invalid username or password.";
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
            <input type="hidden" name="try-login" value="1">
            <input type="text" id="username" name="username" required>
            <br>
            <label for="login-password">Password:</label>
            <input type="password" id="password" name="password" required>
            <br>
            <button type="submit" name="login">Login</button>
        </form>
    </div>

    <div id="registerForm" style="display:none;">
        <h2>Register</h2>
        <form method="post">
            <label for="register-username">Username:</label>
            <input type="hidden" name="try-register" value="1">
            <input type="text" id="username" name="username" required>
            <br>
            <label for="register-password">Password:</label>
            <input type="password" id="password" name="password" required>
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