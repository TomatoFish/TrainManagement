# TrainManagement
Web api for train cargo report generation

---

### Docker:

Run file docker-compose.yml

Api:

- Get report in json format: http[]()://localhost:8010/api/TrainInfo/json?trainNumber=[train_number]
- Get report in excel file: http[]()://localhost:8010/api/TrainInfo/excel?trainNumber=[train_number]
- Upload cars in .xml file: http[]()://localhost:8010/api/TrainInfo

---

### Debug:

Need to setup connection string for database in appsettings.Development.json before run.

Api:

- Get report in json format: http[]()://localhost:5018/api/TrainInfo/json?trainNumber=[train_number]
- Get report in excel file: http[]()://localhost:5018/api/TrainInfo/excel?trainNumber=[train_number]
- Upload cars in .xml file: http[]()://localhost:5018/api/TrainInfo

---

Authorization with JWT Bearer:

Temp token:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIn0.0EpItuYypbEL3SKOWJ_mcaD-BZZ89jxbVNXA5dMgD2M

---

### Used packages:

ASP.NET Core - open-source cross-platform web-app framework provided by Microsoft

Entity Framework Core - open-source ORM provided by Microsoft

AutoMapper - open-source object-to-object mapper

Postgresql - widespread free open-source database

EPPlus - excel files generation