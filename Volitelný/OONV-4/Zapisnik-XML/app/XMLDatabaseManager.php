<?php

class XMLDatabaseManager {

private $accountsXmlPath = __DIR__ . '/../XML/accounts.xml';
private $accountsXsdPath = __DIR__ . '/../XML/accounts.xsd';
private $tasksXmlPath = __DIR__ . '/../XML/tasks.xml';
private $tasksXsdPath = __DIR__ . '/../XML/tasks.xsd';

    // Adds an account according to accounts.xsd rules
    public function addAccount($username, $password, $email) {
        if ($this->accountExists($username, $email)) {
            return false;
        }

        $dom = new DOMDocument();
        $dom->preserveWhiteSpace = false;
        $dom->formatOutput = true;

        if (file_exists($this->accountsXmlPath)) {
            $dom->load($this->accountsXmlPath);
        } else {
            // Create root element if file doesn't exist
            $root = $dom->createElement('accounts');
            $dom->appendChild($root);
        }

        $root = $dom->documentElement;

        $account = $dom->createElement('account');

        $usernameElem = $dom->createElement('username', htmlspecialchars($username));
        $passwordElem = $dom->createElement('password', password_hash($password, PASSWORD_DEFAULT));
        $emailElem = $dom->createElement('email', htmlspecialchars($email));
        $createdAtElem = $dom->createElement('created_at', date('c'));

        $account->appendChild($usernameElem);
        $account->appendChild($passwordElem);
        $account->appendChild($emailElem);
        $account->appendChild($createdAtElem);

        $root->appendChild($account);

        // Validate against XSD
        if (!$dom->schemaValidate($this->accountsXsdPath)) {
            return false;
        }

        $dom->save($this->accountsXmlPath);
        return true;
    }

    // Checks for duplicate username or email
    private function accountExists($username, $email) {
        if (!file_exists($this->accountsXmlPath)) {
            return false;
        }

        $xml = simplexml_load_file($this->accountsXmlPath);
        foreach ($xml->account as $account) {
            if ((string)$account->username === $username || (string)$account->email === $email) {
                return true;
            }
        }
        return false;
    }

    // Login function
    public function login($username, $password) {
        if (!file_exists($this->accountsXmlPath)) {
            return false;
        }

        $xml = simplexml_load_file($this->accountsXmlPath);
        foreach ($xml->account as $account) {
            if ((string)$account->username === $username) {
                if (password_verify($password, (string)$account->password)) {
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    // Register function
    public function register($username, $password, $email) {
        return $this->addAccount($username, $password, $email);
    }




    // Create a new task
    public function createTask($title, $description, $type, $createdBy) {
        $dom = new DOMDocument();
        $dom->preserveWhiteSpace = false;
        $dom->formatOutput = true;

        $nextId = 1;

        if (file_exists($this->tasksXmlPath)) {
            $dom->load($this->tasksXmlPath);
            // Ensure unique title
            $xpath = new DOMXPath($dom);
            $existingTask = $xpath->query("//task[title='" . htmlspecialchars($title, ENT_QUOTES) . "']");
            if ($existingTask->length > 0) {
                return false;
            }
            // Find max id
            $idNodes = $xpath->query("//task/id");
            foreach ($idNodes as $idNode) {
                $idVal = intval($idNode->nodeValue);
                if ($idVal >= $nextId) {
                    $nextId = $idVal + 1;
                }
            }
        } else {
            $root = $dom->createElement('tasks');
            $dom->appendChild($root);
        }

        $root = $dom->documentElement;

        $task = $dom->createElement('task');
        $idElem = $dom->createElement('id', $nextId);
        $titleElem = $dom->createElement('title', htmlspecialchars($title));
        $contentElem = $dom->createElement('content', htmlspecialchars($description));
        $createdAt = $dom->createElement('created_at', date('c'));
        $usernameElem = $dom->createElement('username', htmlspecialchars($createdBy));
        $stateElem = $dom->createElement('state', "Not finished");
        $typeElem = $dom->createElement('type', htmlspecialchars($type));

        $task->appendChild($idElem);
        $task->appendChild($titleElem);
        $task->appendChild($contentElem);
        $task->appendChild($createdAt);
        $task->appendChild($usernameElem);
        $task->appendChild($stateElem);
        $task->appendChild($typeElem);

        $root->appendChild($task);

        if (!$dom->schemaValidate($this->tasksXsdPath)) {
            return false;
        }

        $dom->save($this->tasksXmlPath);
        return true;
    }

    public function editTaskById($id, $newTitle, $description, $type, $state) {
        if (!file_exists($this->tasksXmlPath)) {
            return false;
        }

        $dom = new DOMDocument();
        $dom->preserveWhiteSpace = false;
        $dom->formatOutput = true;
        $dom->load($this->tasksXmlPath);

        $xpath = new DOMXPath($dom);
        // Find the task by its id
        $taskNode = $xpath->query("//task[id='$id']")->item(0);

        if (!$taskNode) {
            return false;
        }

        foreach ($taskNode->childNodes as $child) {
            if ($child->nodeName === 'title') {
                $child->nodeValue = htmlspecialchars($newTitle);
            }
            if ($child->nodeName === 'content') {
                $child->nodeValue = htmlspecialchars($description);
            }
            if ($child->nodeName === 'type') {
                $child->nodeValue = htmlspecialchars($type);
            }
            if ($child->nodeName === 'state') {
                $child->nodeValue = htmlspecialchars($state);
            }
        }

        if (!$dom->schemaValidate($this->tasksXsdPath)) {
            return false;
        }

        $dom->save($this->tasksXmlPath);
        return true;
    }

    // Delete a task
    public function deleteTask($id) {
        if (!file_exists($this->tasksXmlPath)) {
            return false;
        }

        $dom = new DOMDocument();
        $dom->preserveWhiteSpace = false;
        $dom->formatOutput = true;
        $dom->load($this->tasksXmlPath);

        $xpath = new DOMXPath($dom);
        $taskNode = $xpath->query("//task[id='$id']")->item(0);

        if (!$taskNode) {
            return false;
        }

        $taskNode->parentNode->removeChild($taskNode);

        if (!$dom->schemaValidate($this->tasksXsdPath)) {
            return false;
        }

        $dom->save($this->tasksXmlPath);
        return true;
    }

    // Change task state
    public function changeTaskState($id, $newState) {
        if (!file_exists($this->tasksXmlPath)) {
            return false;
        }

        $dom = new DOMDocument();
        $dom->preserveWhiteSpace = false;
        $dom->formatOutput = true;
        $dom->load($this->tasksXmlPath);

        $xpath = new DOMXPath($dom);
        $taskNode = $xpath->query("//task[id='$id']")->item(0);

        if (!$taskNode) {
            return false;
        }

        foreach ($taskNode->childNodes as $child) {
            if ($child->nodeName === 'state') {
                $child->nodeValue = htmlspecialchars($newState);
            }
        }

        if (!$dom->schemaValidate($this->tasksXsdPath)) {
            return false;
        }

        $dom->save($this->tasksXmlPath);
        return true;
    }

    // Get all tasks from the XML file
    public function getAllTasks() {
        if (!file_exists($this->tasksXmlPath)) {
            return [];
        }

        $xml = simplexml_load_file($this->tasksXmlPath);
        $tasks = [];

        foreach ($xml->task as $task) {
            $taskData = [];
            foreach ($task->children() as $key => $value) {
                $taskData[$key] = (string)$value;
            }
            $tasks[] = $taskData;
        }

        return $tasks;
    }

    public function importTaskFromXml($xmlContent, $username) {
    $taskXml = simplexml_load_string($xmlContent);
    if (!$taskXml) return false;

    // Validate structure (must have title, content, type, state, id, created_at)
    $required = ['id', 'title', 'content', 'created_at', 'state', 'type'];
    foreach ($required as $field) {
        if (!isset($taskXml->$field)) return false;
    }

    // Optionally check for duplicate by id
    $allTasks = $this->getAllTasks();
    foreach ($allTasks as $task) {
        if ($task['id'] == (string)$taskXml->id) return false;
    }

    // Create DOM and append task
    $dom = new DOMDocument();
    $dom->preserveWhiteSpace = false;
    $dom->formatOutput = true;
    if (file_exists($this->tasksXmlPath)) {
        $dom->load($this->tasksXmlPath);
    } else {
        $root = $dom->createElement('tasks');
        $dom->appendChild($root);
    }
    $root = $dom->documentElement;

    $taskElem = $dom->createElement('task');
    $taskElem->appendChild($dom->createElement('id', htmlspecialchars((string)$taskXml->id)));
    $taskElem->appendChild($dom->createElement('title', htmlspecialchars((string)$taskXml->title)));
    $taskElem->appendChild($dom->createElement('content', htmlspecialchars((string)$taskXml->content)));
    $taskElem->appendChild($dom->createElement('created_at', htmlspecialchars((string)$taskXml->created_at)));
    $taskElem->appendChild($dom->createElement('username', htmlspecialchars($username)));
    $taskElem->appendChild($dom->createElement('state', htmlspecialchars((string)$taskXml->state)));
    $taskElem->appendChild($dom->createElement('type', htmlspecialchars((string)$taskXml->type)));

    $root->appendChild($taskElem);

    if (!$dom->schemaValidate($this->tasksXsdPath)) return false;
    $dom->save($this->tasksXmlPath);
    return true;
}
}