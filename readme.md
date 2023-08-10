Azure function, which tracks statistics of Nowy Świat radio station on patronite.pl and creates plots with statistics.

- PowerShell based, inbound timer trigger, outbound save to Azure table storage.
- C# based timer trigger to save plot in Azure blob storage.

Used Github actions to deploy functions on every push to main branch (used 'export publish profile' in Azure portal).
Storage account table storage was created manually in Azure Portal (it's not automated).

Result plots (updated daily):

![monthly amount](https://nowyswiatfn3bd064.blob.core.windows.net/plots/MonthlyAmount.png)

![number of patrons](https://nowyswiatfn3bd064.blob.core.windows.net/plots/NumberOfPatrons.png)


