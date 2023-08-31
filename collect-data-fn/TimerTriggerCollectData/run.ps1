# Input bindings are passed in via param block.
param($Timer)

# Get the current universal time in the default string format
$currentUTCtime = (Get-Date).ToUniversalTime()

# The 'IsPastDue' porperty is 'true' when the current function invocation is later than scheduled.
if ($Timer.IsPastDue) {
    Write-Host "PowerShell timer is running late!"
}

Write-Host "PowerShell timer trigger function ran! TIME: $currentUTCtime"

$uri = "https://patronite.pl/radionowyswiat"
$html = Invoke-WebRequest -Uri $uri

$regexNoOfPatrons = '<b id="stats-patrons">([\d\s]{1,8})</b>'
$noOfPatrons = ($html | Select-String $regexNoOfPatrons -AllMatches).Matches.Groups[1].Value

$regexMonthyAmount = '<b id="stats-monthly">([\d\s]{3,8})</b>'
$monthlyAmount = ($html | Select-String $regexMonthyAmount -AllMatches).Matches.Groups[1].Value

$tableStorageRecord = @{
    partitionKey = (Get-Date).ToString("yyyy")
    rowKey = (Get-Date).ToString("yyyy-MM-dd")
    NoOfPatrons = $noOfPatrons
    MonthlyAmount = $monthlyAmount
}

Push-OutputBinding -Name "TableBinding" -Value $tableStorageRecord
