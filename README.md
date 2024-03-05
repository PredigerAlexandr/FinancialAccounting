Добавление миграции и обновление базы выполнять с припиской - "--verbose --project CommandService.Data   --startup-project CommandService"
Например: 
    1.dotnet ef migrations add AddBankDepositEntity --verbose --project Infrastructure --startup-project FA.API
    2.dotnet ef database update --verbose --project Infrastructure --startup-project FA.API
