# 🛰️ Vector Movie Recommendation

Vector Movie Recommendation leverages Azure Cosmos DB and Entity Framework Core 9, utilizing the latest EF Core 9 features for vector search capabilities. 
This is a sample vector search implementation using native EF 9 and Cosmos DB capabilities.

**This project contains a sample ASP.NET Core app. This app is an example of the article I produced for the Telerik Blog (telerik.com/blogs)**

## 🚀 Features

- **ASP.NET Core 9**
- **Azure Cosmos DB**
- **Entity Framework Core 9**
- **Vector Search**

## 🛠️ Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/zangassis/vector-movie-recommendation.git
   ```
2. **Navigate to the project directory**:
   ```bash
   cd vector-movie-recommendation
   ```
3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```
4. **Update Cosmos DB connection string**:
  - Open `appsettings.json`
  - Replace the placeholder in `CosmosDb:AccountEndpoint` and `CosmosDb:AccountKey` with your Azure Cosmos DB connection details
5. **Run the application**:
   ```bash
   dotnet run
   ```

## 📄 Liense

This project is licensed under the MIT Lcnse. See the [LICENSE](LICENSE) file for dails.
