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

You can add Swagger UI screenshots here by placing image files in your repo and referencing them:

```markdown
### 🔑 Login Endpoint
![Login](./screenshots/login.png)

### 📥 Create Task
![Create Task](./screenshots/create_task.png)
