# CozyCare - Home Service Platform (Microservices Architecture)

**CozyCare** is a distributed home service platform that connects customers with reliable housekeepers and service providers for tasks such as cleaning, repairs, maintenance, and more — implemented using a microservices architecture.

---

## ⚙️ Technologies Used

* **ASP.NET Core (.NET 8)** – Scalable, modular microservices backend
* **SQL Server** – Dedicated database per microservice
* **Entity Framework Core** – Database access using code-first
* **Docker & Docker Compose** – For containerized development
* **YARP (Yet Another Reverse Proxy)** – API Gateway routing
* **RabbitMQ** *(optional)* – For asynchronous communication (event-driven)

---

## 📆 Architecture Overview

CozyCare follows a clean microservices architecture:

```
CozyCare.sln
├── /ApiGateway                # YARP-based API Gateway
├── /IdentityService      # Account management
├── /CatalogService       # Services and categories
├── /BookingService       # Booking and job posting
├── /JobService           # Assignment and reporting
├── /PaymentService       # Payments and promotions
│
├── /CozyCare.SharedKernel    # BaseEntity, Exceptions, Utilities
└── /CozyCare.Persistence     # GenericRepository, UnitOfWork
```

Each microservice owns its own:

* Codebase
* Database (isolated schema)
* API contracts
* Dependency injection container

---

## 📄 Services Description

### 👩‍💼 IdentityService

* Register, login, and manage user accounts
* Role-based: Customer, Housekeeper, Admin, Staff
* Account status: Active, Inactive

### 🏠 CatalogService

* Service category management
* Define services: pricing, duration, image
* Additional service options: type, unit, price

### ✅ BookingService

* Customers create job posts (booking)
* Promotions, booking status, and history tracking
* Handles booking schedule and service quantity

### 🧹 JobService

* Housekeepers see open jobs and self-assign
* Tracks assignment status and timing
* Housekeepers report task results and submit reviews

### 💳 PaymentService

* Payment processing and records
* Promo code validation (fixed/percentage)
* Payment status tracking: Paid, Unpaid, Refunded

---

## 🪜 Shared Components

### CozyCare.SharedKernel

* `BaseEntity`, `BaseResponse`, `CoreException`
* Helpers: `StatusCodeHelper`, `EnumHelper`, `TimeHelper`, etc.

### CozyCare.Persistence

* `IGenericRepository<T>`, `GenericRepository<T>`
* `IUnitOfWork`, `UnitOfWork`

Services define their own unit-of-work interfaces like `IIdentityUnitOfWork` to wrap needed repositories.

---

## 🏢 Deployment

* Each microservice is containerized with Docker
* Compose file orchestrates services
* API Gateway handles routing
* Each service runs on its own port with its database

---

## 📊 Roadmap

* [ ] React.js frontend + Tailwind
* [ ] Mobile app (Flutter or React Native)
* [ ] Real-time notification system
* [ ] Centralized logging (Serilog + Seq)
* [ ] OAuth2 or IdentityServer for SSO

---

## ✨ Getting Started

1. Clone the repo
2. Run `docker-compose up -d`
3. Services start at `http://localhost:{port}`
4. Swagger UI is enabled for each service

---

> Maintained by \[SU25 CozyCare Team] – 2025
