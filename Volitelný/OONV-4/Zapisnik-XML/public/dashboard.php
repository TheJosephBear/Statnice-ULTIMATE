<?php
    session_start();
    if (!isset($_SESSION['logged_user'])) {
        header("Location: index.php");
        exit;
    }

    require_once __DIR__ . '/../app/XMLDatabaseManager.php';
    // Create new task
    // Create new task
    if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['taskTitle'], $_POST['taskContent'], $_POST['taskType'])) {
        $manager = new XmlDatabaseManager();
        $manager->createTask($_POST['taskTitle'], $_POST['taskContent'], $_POST['taskType'], $_SESSION['logged_user']);
        header("Location: dashboard.php");
        exit;
    }

    // Edit existing task
    if (isset($_POST['editTask'], $_POST['taskId'], $_POST['newTaskTitle'], $_POST['taskContent'], $_POST['taskType'], $_POST['taskState'])) {
        $manager = new XmlDatabaseManager();
        $manager->editTaskById(
            $_POST['taskId'],
            $_POST['newTaskTitle'],
            $_POST['taskContent'],
            $_POST['taskType'],
            $_POST['taskState']
        );
        header("Location: dashboard.php");
        exit;
    }

    // Delete existing task
    if (isset($_POST['deleteTask'], $_POST['taskId'])) {
        echo "Deleting task with ID: " . htmlspecialchars($_POST['taskId']); // Debug line
        $manager = new XmlDatabaseManager();
        $manager->deleteTask(
            $_POST['taskId']
        );
        header("Location: dashboard.php");
        exit;
    }

    // Import task from XML
    if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_FILES['importTaskXml'])) {
        $manager = new XmlDatabaseManager();
        if ($_FILES['importTaskXml']['error'] === UPLOAD_ERR_OK) {
            $xmlContent = file_get_contents($_FILES['importTaskXml']['tmp_name']);
            $imported = $manager->importTaskFromXml($xmlContent, $_SESSION['logged_user']);
            if ($imported) {
                header("Location: dashboard.php");
                exit;
            } else {
                $importError = "Import failed. Invalid XML or duplicate task.";
            }
        } else {
            $importError = "File upload error.";
        }
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
    <div class="dashboard-container">
        <div class="dashboard-header">
            <div class="dashboard-title">Dashboard</div>
            <div class="dashboard-actions">
                <button>Add Item</button>
                <button onclick="document.getElementById('importTaskFormOverlay').style.display='flex'">Import Task</button>
            </div>
        </div>
        <?php if (!empty($importError)): ?>
            <div class="error"><?= htmlspecialchars($importError) ?></div>
        <?php endif; ?>
        <div id="importTaskFormOverlay" style="display:none; position:fixed; z-index:9999; top:0; left:0; width:100vw; height:100vh; background:rgba(0,0,0,0.5); align-items:center; justify-content:center;">
        <form method="post" enctype="multipart/form-data" style="background:#fff; padding:20px; border-radius:8px;">
            <h2>Import Task (XML)</h2>
            <input type="file" name="importTaskXml" accept=".xml" required>
            <div class="form-actions">
                <button type="button" onclick="document.getElementById('importTaskFormOverlay').style.display='none'">Cancel</button>
                <button type="submit">Import</button>
            </div>
        </form>
    </div>
        <div class="dashboard-content">
            <?php
            require_once __DIR__ . '/../app/XMLDatabaseManager.php';

            $manager = new XmlDatabaseManager();
            $allTasks = $manager->getAllTasks();

            foreach ($allTasks as $task): 
                $taskId = htmlspecialchars($task['id']);
                $taskTitle = htmlspecialchars($task['title']);
                $taskContent = htmlspecialchars($task['content']);
                $taskType = htmlspecialchars($task['type']);
                $taskState = htmlspecialchars($task['state']);
                $taskUser = htmlspecialchars($task['username']);
                $taskCreated = htmlspecialchars($task['created_at']);
            ?>
                <div class="dashboard-panel">
                    <h2><?= $taskTitle ?></h2>
                    <p><?= $taskContent ?></p>
                    <p>Type: <?= $taskType ?></p>
                    <p>State: <?= $taskState ?></p>
                    <p>Created by: <?= $taskUser ?> at <?= $taskCreated ?></p>

                    <button onclick="document.getElementById('editTaskFormOverlay<?= $taskId ?>').style.display='flex'">Edit</button>

                    <!-- Edit Form Overlay -->
                    <div id="editTaskFormOverlay<?= $taskId ?>" class="editTaskFormOverlay" style="display:none; position:fixed;
                     z-index:9999; top:0; left:0; width:100vw; height:100vh; background:rgba(0,0,0,0.5); align-items:center; justify-content:center;'">
                        <form method="post" action="" class="editTaskForm">
                            <input type="hidden" name="editTask" value="1">
                            <input type="hidden" name="taskId" value="<?= $taskId ?>">

                            <h2>Edit Task</h2>

                            <label for="editTitle<?= $taskId ?>">Title</label>
                            <input type="text" name="newTaskTitle" id="editTitle<?= $taskId ?>" value="<?= $taskTitle ?>" required>

                            <label for="editContent<?= $taskId ?>">Content</label>
                            <textarea name="taskContent" id="editContent<?= $taskId ?>" required><?= $taskContent ?></textarea>

                            <label for="editType<?= $taskId ?>">Type</label>
                            <select name="taskType" id="editType<?= $taskId ?>" required>
                                <option value="studium" <?= $taskType === 'studium' ? 'selected' : '' ?>>Studium</option>
                                <option value="osobní" <?= $taskType === 'osobní' ? 'selected' : '' ?>>Osobní</option>
                                <option value="práce" <?= $taskType === 'práce' ? 'selected' : '' ?>>Práce</option>
                            </select>

                            <label for="editState<?= $taskId ?>">State</label>
                            <select name="taskState" id="editState<?= $taskId ?>" required>
                                <option value="new" <?= $taskState === 'new' ? 'selected' : '' ?>>New</option>
                                <option value="in_progress" <?= $taskState === 'in_progress' ? 'selected' : '' ?>>In Progress</option>
                                <option value="done" <?= $taskState === 'done' ? 'selected' : '' ?>>Done</option>
                            </select>
                            <button type="submit">Save</button>
                            <button type="submit" form= "deleteTaskForm<?= $taskId ?>" style="margin-top:10px; background:#e74c3c; color:#fff;">Delete Task</button>
                            <button type="button" onclick="document.getElementById('editTaskFormOverlay<?= $taskId ?>').style.display='none'">Cancel</button>
                            
                            
                        </form>

                        <form id="deleteTaskForm<?= $taskId ?>" method="post" action="" style="margin-top:10px; display:none;">
                            <input type="hidden" name="deleteTask" value="1">
                            <input type="hidden" name="taskId" value="<?= $taskId ?>">
                            <button type="submit" onclick="return confirm('Are you sure you want to delete this task?')" style="background:#e74c3c; color:#fff;">Delete</button>
                        </form>
                    </div>
                </div>
            <?php endforeach; ?>
        </div>
    </div>

    <script>
    document.querySelector('.dashboard-actions button').addEventListener('click', function() {
        document.getElementById('addTaskFormOverlay').style.display = 'flex';
    });
    function closeForm() {
        document.getElementById('addTaskFormOverlay').style.display = 'none';
    }

    document.querySelector('.dashboard-actions button').addEventListener('click', function() {
        document.getElementById('addTaskFormOverlay').style.display = 'flex';
    });
    function closeForm() {
        document.getElementById('addTaskFormOverlay').style.display = 'none';
    }

    </script>
    
    <div id="addTaskFormOverlay">
        <form id="addTaskForm" method="post" action="">
            <h2>New Task</h2>
            <label for="taskTitle">Title</label>
            <input type="text" name="taskTitle" id="taskTitle" required>
            <label for="taskContent">Content</label>
            <textarea name="taskContent" id="taskContent" required></textarea>
            <label for="taskType">Type</label>
            <select name="taskType" id="taskType" required>
                <option value="studium">Studium</option>
                <option value="osobní">Osobní</option>
                <option value="práce">Práce</option>
            </select>
            <div class="form-actions">
                <button type="button" onclick="closeForm()">Cancel</button>
                <button type="submit">Create</button>
            </div>
        </form>
    </div>
</body>
</html>