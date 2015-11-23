Import-Module BitsTransfer

$storageDir = $pwd
$webclient = New-Object System.Net.WebClient
$url = "https://sagamasterclass.blob.core.windows.net/sql/SqlLocalDB.x64.msi"
$file = "$storageDir\SqlLocalDB.msi"

Start-BitsTransfer -Source $url -Destination $file

& $file /qn IACCEPTSQLLOCALDBLICENSETERMS=YES
