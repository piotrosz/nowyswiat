$html = Invoke-WebRequest -Uri "https://patronite.pl/radionowyswiat"

$reg_exp = '<span class="author__stats--number" id="stats-patrons">([\d\s]{1,8})</span>'
 
$all_matches = ($html | Select-String $reg_exp -AllMatches).Matches

Write-Host (Get-Date).ToString("yyyy-MM-dd") $all_matches.Groups[1].Value