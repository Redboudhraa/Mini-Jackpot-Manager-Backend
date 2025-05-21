# Mini Jackpot Manager

A full-stack application to manage and simulate jackpots built with ASP.NET Core backend and React frontend.

## Backend (ASP.NET Core)

### Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or VS Code

### Setup Instructions

1. Clone the repository:
   ```
   git clone https://github.com/Redboudhraa/Mini-Jackpot-Manager-Backend.git
   cd Mini-Jackpot-Manager-Backend
   ```

2. Update the connection string in `appsettings.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=JackpotManager;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

   Connection string format examples:
   - SQL Server LocalDB: `Data Source=.\\;Initial Catalog=MiniJackpotManager;User ID=YourUsername;Password=YourPassword;MultipleActiveResultSets=True;TrustServerCertificate=True;`
   - SQL Server: `Data Source=myServerAddress;Initial Catalog=JackpotManager;User ID=YourUsername;Password=YourPassword;MultipleActiveResultSets=True;TrustServerCertificate=True;`

3. Apply EF Core migrations to create the database:
   ```
   cd JackpotManager.API
   dotnet ef database update
   ```

   If you're using Visual Studio Package Manager Console:
   ```
   Update-Database -Project JackpotManager.API -StartupProject JackpotManager.API
   ```

4. Run the backend application:
   ```
   dotnet run
   ```
   
   The API will be available at `https://localhost:7001` and `http://localhost:5000` (ports may vary).

5. Alternatively, open the solution in Visual Studio and run the project from there.

### API Endpoints

- `GET /api/jackpots` - Returns all jackpots
- `POST /api/jackpots/{id}/contribute` - Adds an amount to a jackpot's current value
  - Request body: `{ "amount": decimal }`
  - Automatically resets the jackpot if the threshold is reached

### Testing

To run the tests:
```
cd JackpotManager.Tests
dotnet test
```

## Frontend (React with TypeScript)

The frontend application will be created in a separate folder using React with TypeScript. See the frontend README for more details.



