# Input bindings are passed in via param block.
param($Timer)

# Get the current universal time in the default string format
$currentUTCtime = (Get-Date).ToUniversalTime()

# The 'IsPastDue' porperty is 'true' when the current function invocation is later than scheduled.
if ($Timer.IsPastDue) {
    Write-Host "PowerShell timer is running late!"
}

# Write an information log with the current time.
Write-Host "PowerShell timer trigger function ran! TIME: $currentUTCtime"

$html = Invoke-WebRequest -Uri "https://patronite.pl/radionowyswiat"

# TODO: Remove duplication

$regexNoOfPatrons = '<span class="author__stats--number" id="stats-patrons">([\d\s]{1,8})</span>'
$noOfPatrons = ($html | Select-String $regexNoOfPatrons -AllMatches).Matches.Groups[1].Value

$regexMonthyAmount = '<span id="stats-monthly">([\d\s]{3,8})</span>'
$monthlyAmount = ($html | Select-String $regexMonthyAmount -AllMatches).Matches.Groups[1].Value

$regexTotalAmount = '<span id="stats-total">([\d\s]{8,10})</span>'
$totalAmount = ($html | Select-String $regexTotalAmount -AllMatches).Matches.Groups[1].Value

$Entity = @{
    partitionKey = "partition1"
    rowKey = (Get-Date).ToString("yyyy-MM-dd")
    NoOfPatrons = $noOfPatrons
    MonthlyAmount = $monthlyAmount
    TotalAmount = $totalAmount
}

Push-OutputBinding -Name "TableBinding" -Value $Entity
