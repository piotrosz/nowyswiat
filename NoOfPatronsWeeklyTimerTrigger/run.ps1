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
$reg_exp = '<span class="author__stats--number" id="stats-patrons">([\d\s]{1,8})</span>'
$all_matches = ($html | Select-String $reg_exp -AllMatches).Matches
Write-Host (Get-Date).ToString("yyyy-MM-dd") $all_matches.Groups[1].Value

