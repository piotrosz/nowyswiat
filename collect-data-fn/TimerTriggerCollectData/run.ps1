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

$regexNoOfPatrons = '<span class="author__stats--number" id="stats-patrons">([\d\s]{1,8})</span>'
$noOfPatrons = ($html | Select-String $regexNoOfPatrons -AllMatches).Matches.Groups[1].Value

$regexMonthyAmount = '<span id="stats-monthly">([\d\s]{3,8})</span>'
$monthlyAmount = ($html | Select-String $regexMonthyAmount -AllMatches).Matches.Groups[1].Value

$tableStorageRecord = @{
    partitionKey = (Get-Date).ToString("yyyy")
    rowKey = (Get-Date).ToString("yyyy-MM-dd")
    NoOfPatrons = $noOfPatrons
    MonthlyAmount = $monthlyAmount
}

Push-OutputBinding -Name "TableBinding" -Value $tableStorageRecord
