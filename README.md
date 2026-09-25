# APPR.coreproject

<details>
<summary><strong>Overview</strong></summary>

This project is an ASP.NET Core Razor Pages web application developed as a prototype for a foundation/relief organisation.

The application provides information about the foundation, allows users to make donations, allows volunteers to register their interest, and provides employees with functionality for managing relief project updates and viewing volunteer submissions.

The project was developed using ASP.NET Core and Bootstrap, with ASP.NET Core Identity providing authentication and role-based authorization.

</details>

<details>
<summary><strong>Technologies Used</strong></summary>

* ASP.NET Core 9.0
* Razor Pages
* C#
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* Bootstrap
* HTML/CSS
* Git/GitHub
* Azure App Service for deployment

</details>

<details>
<summary><strong>Features</strong></summary>

### Public Website

* Foundation-branded header and navigation
* Home page
* About page
* Donation page
* Volunteer registration page
* Contact page
* Responsive Bootstrap layout

</details>

<details>
<summary><strong>Authentication and Roles</strong></summary>

The application uses ASP.NET Core Identity for authentication.

There are two roles:

### Employee

* Access to employee functionality
* Can post updates about ongoing relief projects
* Can view volunteer registrations

### Donor

* Can register and log in
* Can make donations

Users can also make donations as anonymous guests without creating an account.

</details>

<details>
<summary><strong>Donations</strong></summary>

The donation system supports:

* One-time donations
* Recurring donations
* ZAR currency
* USD currency
* EUR currency
* Symbolic prototype donation values
* Anonymous donations
* Donation records stored in the database
* Placeholder tax certificate displayed after a donation

No real financial transactions are processed by this prototype.

</details>

<details>
<summary><strong>Volunteer Registration</strong></summary>

Visitors can register their interest in volunteering by providing:

* Name
* Skills
* Availability

Volunteer submissions are stored by the application and can be viewed by employees.

</details>

<details>
<summary><strong>Relief Project Updates</strong></summary>

Employees can post updates relating to ongoing relief projects.

Public users can view the project updates through the website.

</details>

<details>
<summary><strong>Project Structure</strong></summary>

```text
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
```

</details>

<details>
<summary><strong>Getting Started</strong></summary>

### Prerequisites

To run the project locally, install:

* Visual Studio 2022
* .NET 9 SDK
* SQL Server / LocalDB
* ASP.NET and web development workload

### Running the Application

1. Clone the repository.

```bash
git clone [https://github.com/HiiPower05/APPR.project.git]
```

2. Open the `.sln` file in Visual Studio.

3. Restore the NuGet packages.

4. Check the database connection string in `appsettings.json`.

5. Apply the Entity Framework migrations using the Package Manager Console:

```powershell
Update-Database
```

6. Run the application from Visual Studio.

7. Register or use the provided test accounts to test the different roles.

</details>

<details>
<summary><strong>Test Accounts</strong></summary>

For demonstration purposes, the application can be tested using the following accounts.

### Employee

**Role:** Employee
**Purpose:** Employee functionality

**Email:** [employee@test.com](mailto:employee@test.com)
**Password:** `Employee123!`

### Donor

**Email:** [donor@test.com](mailto:donor@test.com)
**Password:** `Donor123!`

### TEST ACCOUNT

[testuser@test.com](mailto:testuser@test.com)
**Password:** `TestUser1!`

### Test Donor

**Email:** [testdonor@example.com](mailto:testdonor@example.com)
**Password:** `TestDonor123!`

</details>

<details>
<summary><strong>Screenshots</strong></summary>

Screenshots demonstrating the application's functionality are included below.

</details>


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


Part-2:Screenshots
Section A-
Successful Deployment & Live URL:
<img width="1600" height="842" alt="Image 2026-09-25 at 17 45 50" src="https://github.com/user-attachments/assets/a11ef147-670f-4cc4-9556-b2165ffb3960" />

<img width="1600" height="502" alt="Image 2026-09-25 at 17 45 50 (1)" src="https://github.com/user-attachments/assets/8da04356-aafb-4e92-92e7-674d4dcf9ca8" />


Postman Test
<img width="1600" height="672" alt="Image 2026-09-25 at 18 10 52" src="https://github.com/user-attachments/assets/ae07b84a-ba01-43b4-9a00-aa8527a0ebce" />



Section D-
Evidence of updated Visual Studio solution:

<img width="873" height="602" alt="image" src="https://github.com/user-attachments/assets/1e008ae7-a4eb-43e7-9b76-cf61d7f27892" />

<img width="1902" height="821" alt="image" src="https://github.com/user-attachments/assets/a1b462b0-ce4c-4c6c-8e21-c0cb94dcb6f1" />

<img width="952" height="231" alt="image" src="https://github.com/user-attachments/assets/73fbbaa3-e045-4c96-a19b-80f62ed71ec1" />

<img width="1881" height="382" alt="image" src="https://github.com/user-attachments/assets/8f6eb497-65c9-4724-a389-23863a0ee803" />

<img width="660" height="223" alt="image" src="https://github.com/user-attachments/assets/e49b3eaf-bd5d-4437-92e8-b62b79a05bbf" />

<img width="1590" height="900" alt="image" src="https://github.com/user-attachments/assets/0503a6fb-8ee0-4db3-968e-24ac4a2b81a1" />

