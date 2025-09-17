# SmartInventoryAPI

SmartInventoryAPI is a **.NET 6 Web API** project for managing products and categories with **JWT authentication** and **role-based authorization**.  
It demonstrates a clean architecture using **Entity Framework Core**, **Repository + Service pattern**, and **ADO.NET integration** for flexibility.

---

## 🚀 Features

- **Product Management (CRUD)**  
  - Create, Read, Update, Delete products.  
  - Query products by category.  

- **Category Management** (via navigation property)  
  - One-to-many relationship (Category → Products).  

- **Authentication & Authorization**  
  - JWT-based authentication.  
  - Role-based access (`Admin`, `User`).  
  - Example: only Admins can add products.  

- **Database Integration**  
  - EF Core with SQL Server.  
  - Example raw SQL queries using ADO.NET.  

- **Developer Friendly**  
  - Swagger UI for API testing.  
  - Clean separation of concerns (Controller → Service → Repository → Database).

---

## 🛠️ Tech Stack

- **.NET 6 / ASP.NET Core Web API**
- **Entity Framework Core (SQL Server)**
- **ADO.NET (for raw queries)**
- **JWT Authentication & Role-based Authorization**
- **Swagger / OpenAPI**

---

## ⚙️ Setup & Run

### 1. Clone Repository
```bash
git clone https://github.com/yourusername/SmartInventoryAPI.git
cd SmartInventoryAPI
2. Configure Database
Update your appsettings.json with your SQL Server connection string:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SmartInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
3. Apply EF Core Migrations
bash
Copy code
dotnet ef database update
4. Run the Application
bash
Copy code
dotnet run
API will start at:

arduino
Copy code
https://localhost:5001
🔑 Authentication (Demo Users)
Username	Password	Role
admin	password	Admin
user	password	User

Get a JWT token from POST /api/auth/login.

Use it in Swagger/Postman under Authorization → Bearer Token.

📌 Example Endpoints
GET /api/products → Get all products (requires token).

POST /api/products → Add new product (Admin only).

GET /api/products/{id} → Get product by Id.

GET /api/products/category/{categoryId} → Get products by category.

📷 Swagger UI
When running locally, open:

bash
Copy code
https://localhost:5001/swagger
👨‍💻 Contribution
Pull requests are welcome!
For major changes, please open an issue first to discuss what you’d like to change.

📜 License
This project is licensed under the MIT License.

yaml
Copy code

---
