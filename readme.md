Azure function, which tracks statistics of Nowy Świat radio station on patronite.pl.

PowerShell based, inbound timer trigger, outbound save to Azure table storage.

Used Github actions to deploy function on every push to main branch (used 'export publish profile' in Azure portal).
Storage account table storage was created manually in Azure Portal (it's not automated).