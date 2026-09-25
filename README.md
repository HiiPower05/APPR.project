Overview

This project is an ASP.NET Core Razor Pages web application developed as a prototype for a foundation/relief organisation.

The application provides information about the foundation, allows users to make donations, allows volunteers to register their interest, and provides employees with functionality for managing relief project updates and viewing volunteer submissions.

The project was developed using ASP.NET Core and Bootstrap, with ASP.NET Core Identity providing authentication and role-based authorization.

Technologies Used
  ASP.NET Core 9.0
  Razor Pages
  C#
  ASP.NET Core Identity
  Entity Framework Core
  SQL Server
  Bootstrap
  HTML/CSS
  Git/GitHub
  Azure App Service for deployment
Features
  Public Website
  Foundation-branded header and navigation
  Home page
  About page
  Donation page
  Volunteer registration page
  Contact page
  Responsive Bootstrap layout
Authentication and Roles

The application uses ASP.NET Core Identity for authentication.

There are two roles:

Employee
  Access to employee functionality
  Can post updates about ongoing relief projects
  Can view volunteer registrations
Donor
  Can register and log in
  Can make donations

Users can also make donations as anonymous guests without creating an account.

Donations

The donation system supports:

  One-time donations
  Recurring donations
  ZAR currency
  USD currency
  EUR currency
  Symbolic prototype donation values
  Anonymous donations
  Donation records stored in the database
  Placeholder tax certificate displayed after a donation

No real financial transactions are processed by this prototype.

Volunteer Registration

Visitors can register their interest in volunteering by providing:

Name
Skills
Availability

Volunteer submissions are stored by the application and can be viewed by employees.

Relief Project Updates

Employees can post updates relating to ongoing relief projects.

Public users can view the project updates through the website.

Project Structure
APPR.coreproject/
│
├── Areas/
│   └── Identity/
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Models/
│   ├── Donation.cs
│   ├── Volunteer.cs
│   └── [Project Update model]
│
├── Pages/
│   ├── Index.cshtml
│   ├── About.cshtml
│   ├── Contact.cshtml
│   ├── Donate/
│   ├── Volunteer/
│   ├── Employee/
│   └── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
│
├── Migrations/
├── Program.cs
├── appsettings.json
└── APPR.coreproject.csproj
Getting Started
Prerequisites

To run the project locally, install:

Visual Studio 2022
.NET 9 SDK
SQL Server / LocalDB
ASP.NET and web development workload
Running the Application
Clone the repository.
git clone [https://github.com/HiiPower05/APPR.project.git]
Open the .sln file in Visual Studio.
Restore the NuGet packages.
Check the database connection string in appsettings.json.
Apply the Entity Framework migrations using the Package Manager Console:
Update-Database
Run the application from Visual Studio.
Register or use the provided test accounts to test the different roles.
Test Accounts

For demonstration purposes, the application can be tested using the following accounts.

Account	Role	Purpose
employee@test.com	- Employee	Test employee functionality
donor@test.com- Donor	Test donor functionality

TEST ACCOUNT:
testuser@test.com
TestUser1!

Screenshots

Screenshots demonstrating the application's functionality are included below.

Home Page:
<img width="1917" height="1016" alt="Screenshot 2026-08-28 180009" src="https://github.com/user-attachments/assets/785efd21-21b6-49e7-a95b-06306e1372ba" />


Nav:
<img width="1917" height="1020" alt="Screenshot 2026-08-28 180018" src="https://github.com/user-attachments/assets/bea4ab1e-ef95-43c8-827a-61d2c4a86869" />

<img width="1917" height="1020" alt="Screenshot 2026-08-28 180024" src="https://github.com/user-attachments/assets/77d62b06-210a-4f52-9e4d-318c0abc41fa" />

Login:
<img width="1917" height="1018" alt="Screenshot 2026-08-28 180037" src="https://github.com/user-attachments/assets/9bd40a21-9b66-45e7-ba2f-7a7db887281d" />

Register: 
<img width="1917" height="911" alt="Screenshot 2026-08-28 180031" src="https://github.com/user-attachments/assets/34c6361a-1317-4961-ad68-2e50a9480545" />

Employee Dashboard:
<img width="1917" height="1018" alt="Screenshot 2026-08-28 180100" src="https://github.com/user-attachments/assets/98d1b179-2b64-4e5b-8bfa-09259733d241" />
