#Set-ExecutionPolicy -ExecutionPolicy Bypass 
$mysqlpath = "C:\Program Files\MariaDB 5.5\bin"  # Caminho para a instalação do MySQL
$backuppath = "C:\XDSoftware\backups\"  # Caminho para armazenar os backups
$7zippath = "C:\Program Files\7-Zip"  # Caminho para a instalação do 7zip
$config = "C:\XDSoftware\backups\config.cnf"  # Caminho para o arquivo com as credenciais
$database = "xd" # Nome do nosso banco de dados
$errorLog = "C:\XDSoftware\backups\error_dump.log"  # Caminho para o nosso arquivo de log
$days = 7 # Dias para manter os arquivos de backup
$date = Get-Date
$timestamp = "_" + $date.day + $date.month + $date.year + "_" + $date.hour + $date.minute 
$backupfile = $backuppath + $database + $timestamp +".sql"
$backupzip = $backuppath + $database + $timestamp +".zip"
  
# Inicia o processo de backup 
CD $mysqlpath
.\mysqldump.exe --defaults-extra-file=$config --log-error=$errorLog  --result-file=$backupfile  --databases $database
 
# Inicia o processo de compactacao com 7zip  
CD $7zippath
.\7z.exe a -tzip $backupzip $backupfile
  
# Deleta o arquivo bruto
Del $backupfile
 
# Deleta arquivos antigos
CD $backuppath
$oldbackups = gci *.zip* 
  
for($i=0; $i -lt $oldbackups.count; $i++){ 
    if ($oldbackups[$i].CreationTime -lt $date.AddDays(-$days)){ 
        $oldbackups[$i] | Remove-Item -Confirm:$false
    } 
}