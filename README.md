# Apartment Construction Expense Management System

A professional C# Windows Forms application built with .NET and SQL Server to manage, track, and audit construction expenses, featuring robust role-based access control, transaction logging, and administration interfaces.

---

## 🛠️ Architecture & Features

This project utilizes a clean tier separation (Models, Services, Forms, Helpers) with raw ADO.NET SQL queries to ensure maximum runtime efficiency and database integrity.

### 🔐 Member 1 — Authentication & User Management
* **Role-Based Login System**: Supports both `Admin` and `User` dashboard interfaces.
* **Smart Security Checks**: Automatically flags and redirects `'Pending'` accounts to an approval wait page, and displays secure warnings for `'Blocked'` accounts.
* **Clean Registration**: Inserts new user accounts safely using parameterized SQL statements, defaulting their approval state to `'Pending'`.
* **Dynamic Status Filter**: An interactive ComboBox on the administrator table allowing live grid filtering by `'All Users'`, `'Pending'`, `'Approved'`, or `'Blocked'`.
* **Actionable Admin Grid**:
  * **Approve**: Instantly changes a user status to `'Approved'`.
  * **Reject**: Prompts for confirmation and deletes the user record from the database.
  * **Block / Unblock Toggle**: Dynamically toggles a user's status between `'Blocked'` and `'Approved'`.
  * **Details Form**: Opens a modal showing detailed user information and direct action buttons.

---

## 🗄️ Database Setup

The backend runs on **Microsoft SQL Server**. The schema consists of relational tables mapping users, expense categories, and transaction amounts.

### Method 1: Automatic Setup via Command Line
You can instantly initialize the database and load all demo/admin credentials by running the following command in your terminal or Command Prompt (CMD):

```bash
sqlcmd -S localhost\SQLEXPRESS -E -i "DatabaseSetup.sql"
```

#### 💡 Command Parameter Explanation:
* **`sqlcmd`**: The command-line command utility used to interact with Microsoft SQL Server.
* **`-S localhost\SQLEXPRESS`**: Specifies the target SQL Server instance name.
  * `localhost`: Refers to your local machine.
  * `\SQLEXPRESS`: Refers to the default SQL Express instance directory installed on your computer.
* **`-E`**: Uses **Windows Authentication** (trusted connection) to securely sign into the database server without having to expose or enter a password.
* **`-i "DatabaseSetup.sql"`**: Specifies the input file containing the SQL script. This will read the script from `DatabaseSetup.sql` and execute all commands (creating the database, defining tables, seeding initial administrator accounts) in sequential order.

### Method 2: Manual Setup via SSMS
1. Open **SQL Server Management Studio (SSMS)**.
2. Connect to your database engine (`localhost\SQLEXPRESS` or `(localdb)\MSSQLLocalDB`).
3. Open the `DatabaseSetup.sql` file in SSMS.
4. Click the **Execute** button (or press `F5`) to run the script.

### Connection String Setup
Configure your database connection inside the `.env` file in the project root:
```env
DB_CONNECTION_STRING="Data Source=localhost\SQLEXPRESS;Initial Catalog=ApartmentExpenseDB;Integrated Security=True;TrustServerCertificate=True"
```

---

## 🚀 How to Run the Project

1. Navigate to the project root directory in PowerShell or CMD.
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Build the application:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

---

## 📁 Project Structure Full Details

Here is the complete architectural layout of the application codebase. The project is strictly designed around clean **separation of concerns**, ensuring each module has a single, well-defined responsibility:

```
├── Controls/            # Custom UI Component & Rendering Layer
│   └── SidebarButton.cs         # Custom-drawn navigation button used in main sidebar layouts.
├── Forms/               # Visual User Interfaces (Forms)
│   ├── LoginForm.cs             # Portal page for signing in, with validation and user status routing.
│   ├── RegisterForm.cs          # Register form allowing new users to sign up into the database.
│   ├── PendingApprovalForm.cs   # Custom landing dashboard screen for users whose status is 'Pending'.
│   ├── AdminDashboardForm.cs   # Main management dashboard layout for administrator users.
│   ├── AdminUsersForm.cs        # Comprehensive table grid to review, filter, approve, reject, or block users.
│   ├── UserDetailsForm.cs       # Detailed modal card showing complete properties of a selected user.
│   ├── AdminCategoriesForm.cs   # Category management interface for adding, editing, and listing expense categories.
│   ├── AdminExpensesForm.cs     # Main administrator panel to view and audit all expenses.
│   ├── UserDashboardForm.cs     # Main dashboard interface for standard users.
│   ├── UserExpensesForm.cs      # User's personal panel to log and view their own expense submissions.
│   └── AddExpenseForm.cs        # Input popup form for submitting new expense transactions.
├── Helpers/             # Cross-cutting Utilities and Shared Systems
│   ├── DatabaseHelper.cs        # ADO.NET SQL Connection provider using environment configurations.
│   └── UITheme.cs               # Premium design tokens, colors, custom fonts, and styled UI helpers.
├── Models/              # Data Entity Layer (OOP Schemas)
│   ├── User.cs                  # Structured C# object mapping the properties of a 'Users' database row.
│   ├── Category.cs              # Represents an expense category (e.g. materials, labour, equipment).
│   └── Expense.cs               # Mapped schema holding transaction details, category relations, and dates.
├── Services/            # Business Logic & Database Communication Layer (DAL)
│   ├── AuthService.cs           # Login validation, registration, global session storage, and approval checkers.
│   ├── UserService.cs           # Database CRUD actions to fetch, update status, and remove user records.
│   ├── CategoryService.cs       # Database operations to query, insert, and list expense categories.
│   └── ExpenseService.cs        # Business operations to log transactions, compute sums, and aggregate statistics.
├── DatabaseSetup.sql    # Complete SQL database schema definitions & seed data
├── Program.cs           # Application entry point, initializing environments and starting the UI thread
└── README.md            # Comprehensive project documentation manual
```

---

## 📂 Structural Deep Dive

### 1. Model Layer (`Models/`)
Holds lightweight, strongly-typed data structures to safely transport information across layers.
* **`User.cs`**: Represents registered members (`UserID`, `Name`, `Email`, `Role`, `Status`, `JoinedAt`).
* **`Category.cs`**: Represents expense categories (`CategoryID`, `CategoryName`, `Icon`, `Description`).
* **`Expense.cs`**: Maps expense records (`ExpenseID`, `Title`, `Amount`, `ExpenseDate`, `CategoryID`, `UserID`, `CreatedAt`).

### 2. Service Layer (`Services/`)
Performs the core business operations and queries the database using parameterized ADO.NET queries to avoid SQL Injection.
* **`AuthService.cs`**: Validates login credentials, adds new user accounts, and stores the logged-in session in `CurrentUser`.
* **`UserService.cs`**: Fetches user sets, manages user approval status changes, blocks accounts, and performs secure deletions.
* **`CategoryService.cs`**: Handles category creation, validation, and lists active expense classifications.
* **`ExpenseService.cs`**: Records new expense items, tracks spending limits, and sums expense numbers for dashboards.

### 3. Helpers Layer (`Helpers/`)
* **`DatabaseHelper.cs`**: Safely loads database configurations and yields established `SqlConnection` sessions.
* **`UITheme.cs`**: Houses centralized graphical styles (rounded rectangle rendering, soft shadow drawing, active colors, custom buttons).

### 4. Custom Controls Layer (`Controls/`)
* **`SidebarButton.cs`**: Custom double-buffered UI buttons with hover animations used for navigation controls.

### 5. Presentation Layer (`Forms/`)
Manages standard Windows Forms lifecycle events, triggers UI threads, and handles user interactions.
* **`LoginForm.cs`**: Authenticates users and routes them to their dashboard depending on roles or approvals.
* **`RegisterForm.cs`**: User sign-up panel. All sign-ups default to a `'Pending'` state until approved by an administrator.
* **`PendingApprovalForm.cs`**: A warning screen reminding newly registered accounts to wait for admin activation.
* **`AdminDashboardForm.cs`**: Primary management cockpit for admins, managing tab navigations.
* **`AdminUsersForm.cs`**: Grid view containing comprehensive status filtering, detailed member view modals, approvals, blocks, and deletions.
* **`UserDetailsForm.cs`**: A detailed information card modal for individual users.
* **`AdminCategoriesForm.cs`**: Setup panel for registering and viewing expense groups.
* **`AdminExpensesForm.cs`**: Full transaction ledger for admins to review all employee expense claims.
* **`UserDashboardForm.cs`**: Main window for general users showing their custom totals.
* **`UserExpensesForm.cs`**: Local list of expense submissions logged by the logged-in user.
* **`AddExpenseForm.cs`**: Modal entry panel for registering a new transaction.

---

## 👥 Contributors

| Member | Role | Key Contributions |
| :--- | :--- | :--- |
| 👤 **Padmasree Saha** | **Member 1** | Core System Architecture, Authentication Engine, & Admin User Management Systems. |

