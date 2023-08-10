# Nowy Świat

2 Azure functions:

- one which tracks statistics of Nowy Świat radio station [link](https://nowyswiat.online) on patronite.pl [link](https://patronite.pl/nowyswiat) and saves data to table storage daily (PowerShell based, inbound timer trigger, outbound save to Azure table storage.),
- one that creates plots (C# based timer trigger to save plot in Azure blob storage).

Used Github actions to deploy functions on every push to main branch (used 'export publish profile' in Azure portal).
Storage account table storage was created manually in Azure Portal (it's not automated).

Result plots (updated daily):

![monthly amount](https://nowyswiatfn3bd064.blob.core.windows.net/plots/MonthlyAmount.png)

![number of patrons](https://nowyswiatfn3bd064.blob.core.windows.net/plots/NumberOfPatrons.png)


