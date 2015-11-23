Import-Module BitsTransfer

$storageDir = $pwd
$webclient = New-Object System.Net.WebClient
$url = "https://sagamasterclass.blob.core.windows.net/sql/SQLManagementStudio.x64.exe"
$file = "$storageDir\SQLManagementStudio.x64.exe"

Start-BitsTransfer -Source $url -Destination $file

& $file
