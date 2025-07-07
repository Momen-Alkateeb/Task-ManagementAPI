# 🗂️ Task Management API

A full-featured **Task Management System** built with **ASP.NET Core Web API**, utilizing **JWT Authentication**, **ASP.NET Identity**, and **EF Core (Code First)**.  
The system is designed with **three roles** (Admin, Manager, Employee), and each has specific permissions and access levels.

---

## 🚀 General Overview

This API enables organizations to manage tasks efficiently through role-based access control. Each user is assigned a role (Admin, Manager, Employee), and can perform actions relevant to that role.

### 🎯 Key Functionalities:

- Registering and managing users
- Creating, assigning, editing, and deleting tasks
- Viewing all tasks or task details
- Updating task statuses
- Role-based access via separate controllers
- JWT Token generation on login
- Clean architecture using **Repository Pattern**
- Database schema designed using **EF Core Code-First**

---

## 🧾 Roles and Permissions

### 🔐 Admin

- Can register new users (only Admin can register)
- Can delete users
- Can update user data
- Can reset/change passwords
- Has access to the User Management endpoints

### 📋 Manager

- Can create new tasks and assign them to employees
- Can view all tasks or a specific task
- Can filter tasks by employee name or task ID
- Can edit or delete tasks by employee name or task ID

### 👨‍💼 Employee

- Can view only tasks assigned to them
- Can update the task status (e.g., In Progress, Completed)
- Helps managers track task progress

> **Note:** Each role has access to its own controller and only to authorized endpoints.

---

## 🔐 Authentication & Authorization

- Uses **ASP.NET Identity** for user management
- Uses **JWT Tokens** for secure login and authentication
- Only the Admin can create new user accounts

---

## 🛠️ Technologies Used

| Technology          | Purpose                              |
|---------------------|---------------------------------------|
| ASP.NET Core Web API| Backend development                  |
| Entity Framework Core | Database interaction (Code First)   |
| SQL Server          | Primary database                     |
| ASP.NET Identity    | User management and authentication   |
| JWT                 | Token-based authentication           |
| Swagger UI          | API testing and documentation        |
| Repository Pattern  | Clean and testable code architecture |

---

## 🧱 Database Design

The database is designed using **Code First** approach. It includes:

- `Users` table (inherits from `IdentityUser`)
- `Tasks` table

### 🔗 Relationship:
- **One-to-Many**: One user can have many tasks, but each task is assigned to only one user.

---

## 📷 Swagger UI Screenshots

## 📂 Project Structure

- Follows layered architecture for separation of concerns
- Organized into folders: Controllers, Models, DTOs, Repositories, Data
- Uses Repository Pattern for maintainability

plaintext
Task-ManagementAPI/
├── Controllers/
│   ├── AdminController.cs          - Admin endpoints
│   ├── ManagerController.cs        - Manager endpoints
│   ├── EmployeeController.cs       - Employee endpoints
│   ├── AuthController.cs           - Login/Register
├── DTO/
│   ├── AddingTaskDTO.cs
│   ├── DisplayTaskDTO.cs
│   └── UserDTOs.cs
├── Models/
│   └── Task.cs
├── Repositories/
│   ├── ITaskRepo.cs
│   ├── TaskRepository.cs
│   └── AuthRepository.cs
├── Data/
│   └── AppDbContext.cs
├── Program.cs
├── appsettings.json
└── README.md




## ▶️ Getting Started

- Make sure you have **Visual Studio 2022+** and **.NET 6 or later**
- Set up your local **SQL Server** instance
- Clone the repo:

```bash
git clone https://github.com/Momen-Alkateeb/Task-ManagementAPI.git
dotnet ef database update
Run the project from Visual Studio or use:
```
---

### 🔗 API Examples

```markdown
## 🔗 API Examples

- `POST /api/Auth/Register`  
  - Register new user (**Admin only**)

- `POST /api/Auth/Login`  
  - Login with email & password, returns JWT token

- `POST /api/Manager/AddTask`  
  - Manager creates task and assigns to employee

- `GET /api/Manager/GetAllTasks`  
  - Manager fetches all tasks

- `GET /api/Manager/GetTaskById/{id}`  
  - Manager fetches task by ID

- `GET /api/Manager/GetTasksByUserName/{username}`  
  - Manager gets tasks for a specific employee

- `PUT /api/Manager/UpdateTask/{id}`  
  - Manager updates task by ID

- `DELETE /api/Manager/DeleteTask/{id}`  
  - Manager deletes task

- `GET /api/Employee/MyTasks`  
  - Employee views their assigned tasks

- `PUT /api/Employee/UpdateStatus/{taskId}`  
  - Employee updates task status

## 👤 Developer
-Name: Momen Omar Alkateeb
-Degree: B.Sc. in Computer Engineering – Tafila Technical University
-LinkedIn: Momen Alkateeb

