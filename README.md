# JobOpeningsManagementSystem

---------------------------
 Authentication (JWT)
---------------------------

This API is secured using JWT tokens. To use protected endpoints, you must authenticate first.

1. Use the following credentials to obtain a token:

   {
     "username": "admin",
     "password": "admin123"
   }

2. Send the above credentials to the endpoint:

   POST /api/v1/auth/login

3. The response will return a JWT token like this:

   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
   }

4. Use this token to authorize your requests.
   Add it to the Authorization header like:

   Authorization: Bearer {your_token}


---------------------------
 Technology Stack
---------------------------

- ASP.NET Core 6.0
- Entity Framework Core (with SQL Server or In-Memory DB for testing)
- JWT Token Authentication
- Swashbuckle (Swagger) for API documentation
- NUnit + Moq for Unit Testing


---------------------------
 Endpoints Overview
---------------------------

- POST   /api/v1/auth/login       --> Get JWT Token
- POST   /api/v1/jobs             --> Create Job (secured)
- PUT    /api/v1/jobs/{id}        --> Update Job (secured)
- POST   /api/jobs/list           --> List Jobs
- GET    /api/v1/jobs/{id}        --> Job Details

- POST   /api/v1/locations        --> Create Location (secured)
- PUT    /api/v1/locations/{id}   --> Update Location (secured)
- GET    /api/v1/locations        --> Get All Locations

- POST   /api/v1/departments      --> Create Department (secured)
- PUT    /api/v1/departments/{id} --> Update Department (secured)
- GET    /api/v1/departments      --> Get All Departments
---------------------------
   Notes
---------------------------

- Make sure your `appsettings.json` contains the JWT settings and admin credentials.
- Ensure your database is created and seeded with test data as needed.

