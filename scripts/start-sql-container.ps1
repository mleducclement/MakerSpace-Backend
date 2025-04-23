param (
    [string]$Environment,
    [string]$containerName = "angry_colden",
    [string]$h = "localhost",
    [string]$ConnectionString,
    [int]$port = 1434,
    [int]$maxRetries = 20,
    [int]$waitSeconds = 1
)

function LogInfo($msg)    { Write-Host "`e[96m[INFO]    $msg`e[0m" }
function LogSuccess($msg) { Write-Host "`e[92m[SUCCESS] $msg`e[0m" }
function LogWarning($msg) { Write-Host "`e[93m[WAITING] $msg`e[0m" }
function LogError($msg)   { Write-Host "`e[91m[ERROR]   $msg`e[0m" }

LogInfo("[INFO] checking environment...")
LogInfo("[INFO] Environment is $Environment")

if ($Environment -ne "Development") {
    LogInfo("[INFO] Skipping container startup ($Environment)")
    exit 0
}

if (-not $connectionString) {
    LogError "Missing -ConnectionString parameter"
    exit 1
}

LogInfo("[INFO] Starting container $containerName...")
docker start $containerName | Out-Null

LogInfo("[INFO] Waiting for SQL Server to be available on port ${h}:${port}")

$retryCount = 0
while ($retryCount -lt $maxRetries) {
    try {
        $sqlConnecton = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
        $sqlConnecton.Open()
        $sqlConnecton.Close()
        LogSuccess("[SUCCESS] SQL Server is accepting connections.")
        exit 0
    } catch {
        $retryCount++
        LogWarning("[WAITING] Attempt $retryCount of $maxRetries... SQL not ready")
        Start-Sleep -Seconds $waitSeconds
    }
}

LogError("[ERROR] SQL Server did not become available in time.")
exit 1