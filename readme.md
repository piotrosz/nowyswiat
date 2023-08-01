Azure function, which tracks statistics of Nowy Świat radio station on patronite.pl.

PowerShell based, inbound timer trigger, outbound save to Azure table storage.
C# based timer trigger to save plot in Azure blob storage.


Used Github actions to deploy function on every push to main branch (used 'export publish profile' in Azure portal).
Storage account table storage was created manually in Azure Portal (it's not automated).