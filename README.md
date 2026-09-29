# BankWebAPI

# Full-Stack Enterprise Banking Application Architecture

A scalable, full-stack fintech banking solution engineered using C# and modern software design patterns. The application bridges a decoupled web browser client interface directly to a relational database engine.

## 🚀 Architectural Pillars
*   **Frontend Dashboard:** A responsive HTML5 and CSS dashboard utilizing asynchronous JavaScript `fetch()` API calls to handle two-way JSON data package streams and CORS cross-origin policies.
*   **Backend RESTful Web API:** An ASP.NET Core Web API layer utilizing structured dependency controllers to expose automated `GET` reading queries and `POST` transaction pipelines.
*   **Storage Tier & ORM:** Uses Entity Framework Core (ORM) to manage data transitions automatically via EF Core Migrations, backed permanently by a local **PostgreSQL** relational database server.
*   **Object-Oriented Integrity:** Implements solid OOP inheritance foundations with concrete `SavingsAccount` domain child models extending validated abstract parent base layouts.
