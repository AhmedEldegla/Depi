<p align="center">
  <h1 align="center">DEPI Smart Freelance Platform</h1>
  <p align="center">
    <strong>AI-Powered Freelance Marketplace & Talent Ecosystem</strong>
    <br/>
    Built with ASP.NET Core 8.0 · Clean Architecture · CQRS · DDD
  </p>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet" alt=".NET 8"/>
  <img src="https://img.shields.io/badge/Architecture-Clean_Architecture-blue?style=flat" alt="Clean Architecture"/>
  <img src="https://img.shields.io/badge/Pattern-CQRS_%2B_MediatR-orange?style=flat" alt="CQRS"/>
  <img src="https://img.shields.io/badge/ORM-Entity_Framework_Core_8.0-5632D3?style=flat" alt="EF Core"/>
  <img src="https://img.shields.io/badge/Database-SQL_Server-CC2927?style=flat&logo=microsoft-sql-server" alt="SQL Server"/>
  <img src="https://img.shields.io/badge/Auth-JWT_Bearer-6DB33F?style=flat" alt="JWT"/>
  <img src="https://img.shields.io/badge/API-RESTful-success?style=flat" alt="REST"/>
  <img src="https://img.shields.io/badge/Tests-521_Passed-brightgreen?style=flat" alt="Tests"/>
</p>

---

## 📋 Overview

**DEPI Smart Freelance Platform** is a comprehensive, AI-driven freelance marketplace that connects Clients with Freelancers . It goes beyond traditional freelance platforms with advanced features: **AI-powered freelancer matching**, **Digital Guilds**, **Head Hunter talent scouting**, **Coaching & Student onboarding**, **Learning Management System**, **Community forums**, a **Job board**, and **Company profiles**.

### 🎯 What Problem Does It Solve?

| Problem | Solution |
|---------|----------|
| Finding the right freelancer is slow and manual | AI matching engine scores freelancers on 10 dimensions |
| Students lack a structured path to freelancing | 5-step onboarding journey: Profile → Portfolio → Skills → Coaching → Market |
| Freelancers need continuous skill development | Built-in LMS with courses, learning paths, and certifications |
| Traditional platforms lack team collaboration | Digital Guilds for freelancer collectives and group projects |
| Companies need vetted talent at scale | Head Hunters with AI-assisted talent recommendations |
| Payment disputes slow down work | Escrow system with milestone-based contracts |

---

## 🏗️ Architecture

```
┌──────────────────────────────────────────────────────┐
│                    DEPI.API (Presentation)            │
│  Controllers · Middleware · Filters · Swagger · Rate  │
│                     Limiting · CORS                   │
├──────────────────────────────────────────────────────┤
│              DEPI.Application (Application)           │
│  Use Cases (CQRS) · DTOs · Interfaces · Behaviors    │
│  FluentValidation · AutoMapper · MediatR · Result<T> │
├──────────────────────────────────────────────────────┤
│                DEPI.Domain (Core Business)            │
│  Entities · Enums · Domain Events · Base Classes      │
│  Business Invariants · Static Factory Methods         │
├──────────────────────────────────────────────────────┤
│             DEPI.Infrastructure (Data & Services)     │
│  EF Core DbContext · 64+ Repositories · Migrations   │
│  Identity · JWT Token Service · Email Service         │
└──────────────────────────────────────────────────────┘
```

### Design Patterns

| Pattern | Implementation |
|---------|---------------|
| **Clean Architecture** | 4-layer strict separation (Domain → Application → Infrastructure → API) |
| **CQRS** | Commands (writes) and Queries (reads) via MediatR 12.2.0 |
| **Repository Pattern** | 64+ repository interfaces with EF Core implementations |
| **Result Pattern** | `Result<T>` with functional `Map`/`Bind`/`Ensure` extensions |
| **Domain Events** | Entities raise events (e.g., `UserRegisteredEvent`) processed post-save |
| **Pipeline Behaviors** | Logging + Unhandled Exception decorators through MediatR pipeline |
| **Factory Methods** | Static `Create()` methods on entities enforce business invariants |

---

## 🚀 Tech Stack

### Core
| Technology | Version | Purpose |
|-----------|---------|---------|
| .NET | 8.0 | Runtime & SDK |
| ASP.NET Core | 8.0 | Web API framework |
| Entity Framework Core | 8.0 | ORM |
| SQL Server | - | Primary database |

### Packages
| Package | Version | Purpose |
|---------|---------|---------|
| **MediatR** | 12.2.0 | CQRS / Command-Query mediation |
| **FluentValidation** | 11.9.0 | Request validation pipeline |
| **AutoMapper** | 13.0.1 | Entity ↔ DTO mapping |
| **Swashbuckle** | 6.5.0 | Swagger/OpenAPI documentation |
| **JwtBearer** | 8.0.0 | JWT authentication |
| **Microsoft.Identity** | 8.17.0 | Token signing & validation |

### Testing
| Technology | Version |
|-----------|---------|
| xUnit | 2.6.4 |
| Moq | 4.20.70 |
| FluentAssertions | 6.12.0 |
| Coverlet | 6.0.0 + 3.2.0 |
| EF Core InMemory & SQLite | 8.0.0 |

---

## ✨ Features & Modules

### Core Freelance Platform
- **Projects** — Create, open, manage projects (Hourly/FixedPrice) with skill requirements
- **Proposals** — Submit, accept, reject, withdraw proposals with AI-backed scoring
- **Contracts** — Milestone-based contracts with pause/complete lifecycle
- **Wallets** — Multi-currency wallets with deposit, withdraw (1% fee), transfer, escrow
- **Reviews** — 5-star rating system with responses

### Identity & Security
- JWT Bearer authentication with refresh token rotation
- 6 user roles: Admin, Client, Freelancer, Student, HeadHunter, Coach
- Password policies (digit + lowercase + uppercase, min 6 chars)
- Rate limiting (100 req/min), CORS, global exception handling
- Soft delete with audit trail

### AI-Powered Features
- **AI Matching Engine** — Scores freelancers on 10 dimensions (skills, experience, budget, location, availability, success rate, response time, quality, reliability, domain expertise)
- **AI Price Prediction** — Predicts fair project pricing based on skills, experience, and complexity
- **Skill Gap Analysis** — Identifies missing skills for career growth

### Digital Guilds
- Create & join freelancer collectives organized by specialization
- Guild membership with skill portfolios
- Collaborative project capabilities

### Head Hunters
- Talent scouting agents with AI-assisted recommendations
- Assignment tracking and talent review workflows
- Top-performer leaderboards

### Coaching & Student Onboarding
- **5-Step Student Journey**: Profile → Portfolio → Skills → Coach Assignment → Market Ready
- Coach profiles with session scheduling
- Session notes, feedback, action items, and ratings

### Learning Platform (LMS) ( TODO )
- Courses with ordered lessons and progress tracking
- Learning paths (curated course sequences)
- Certifications upon completion
- Course reviews and ratings

### Community
- Posts (Text, Image, Video, Link, Poll, Announcement)
- Forum threads with categories and replies
- Likes, bookmarks, and sharing

### Recruitment
- Job board with categories and skill requirements
- Job applications with attachments
- Featured & type-filtered job listings

### Company Profiles
- Company pages with member management
- Company projects and followers
- Verified company badges

### Messaging
- Direct and group conversations
- Message attachments and reply-to support
- Notifications with read/unread tracking

### Connects System
- Gamified networking currency (purchase, earn, spend)
- 10 earning rules (Complete Project +10, 5-Star Review +5, First Project +20, 7-Day Streak +25, etc.)
- Subscription packs for premium freelancers

---

## 📊 Project Statistics

| Metric | Value |
|--------|-------|
| Controllers | 24 |
| API Endpoints | ~130 |
| Domain Entities | 65 |
| Repository Interfaces | 64+ |
| NuGet Packages | 30 |
| Seed Users | 7 (all roles)
---

## 📁 Project Structure

```
DEPI_Platform/
├── DEPI.Platform.sln
├── README.md
├── wiki/                           # GitHub Wiki pages
├── docs/                           # Additional documentation
├── src/
│   ├── DEPI.Domain/                # Entities, Enums, Domain Events
│   │   ├── Common/                 # Entity.cs, AuditableEntity.cs, DomainEventBase
│   │   ├── Entities/               # 19 entity groups (65 entities)
│   │   ├── Enums/                  # 14 enum types
│   │   └── Interfaces/             # IRepository<T>
│   ├── DEPI.Application/           # Business logic
│   │   ├── Behaviors/              # Logging + Exception pipeline behaviors
│   │   ├── Common/                 # Result<T>, Errors, ResultExtensions
│   │   ├── DependencyInjection/    # ApplicationDI.cs
│   │   ├── DTOs/                   # 12 DTO groups
│   │   ├── Interfaces/             # Repository & service interfaces
│   │   ├── MappingProfiles/        # 9 AutoMapper profiles
│   │   ├── Repositories/           # 14 domain-specific repo interfaces
│   │   ├── Services/               # AI Matching services
│   │   └── UseCases/               # 23 feature areas (CQRS handlers)
│   ├── DEPI.Infrastructure/        # Data access & external services
│   │   ├── DependencyInjection/    # InfrastructureDI.cs (64+ registrations)
│   │   ├── Persistence/
│   │   │   ├── ApplicationDbContext.cs  # 65 DbSets
│   │   │   ├── Configurations/     # 25 EF Core entity configs
│   │   │   ├── Migrations/         # 4 migrations + snapshot
│   │   │   └── Repositories/       # 23 repository implementations
│   │   └── Services/               # TokenService, EmailService
│   └── DEPI.API/                   # Web API
│       ├── Controllers/            # 24 API controllers
│       ├── Middleware/              # ExceptionHandlingMiddleware
│       ├── SeedData/               # Database seeder
│       ├── Filters/                # Action filters
│       └── Program.cs              # Entry point & pipeline
└── tests/
    └── DEPI.Tests/                 # Unit & integration tests
        ├── Application/
        │   ├── Handlers/           # CQRS handler tests
        │   └── Behaviors/          # Pipeline behavior tests
        └── Infrastructure/         # Repository tests
```

---

## 🔐 Authentication & Authorization

### JWT Token Flow
```
Client                    API Server
  │                           │
  ├── POST /api/auth/login ──►│ Validate credentials
  │                           │ Generate AccessToken (HMAC-SHA256, 15min)
  │                           │ Generate RefreshToken (64-byte random, 7 days)
  │◄── { accessToken,        │
  │      refreshToken } ──────┤
  │                           │
  ├── API call + Bearer ─────►│ Validate JWT (sub, email, jti, type claims)
  │                           │ Check [Authorize(Roles="...")] attribute
  │                           │
  ├── POST /refresh ─────────►│ Rotate refresh token
  │◄── New tokens ────────────┤
```

### Role-Based Access Control

| Role | Enum Value | Registered By | Core Permissions |
|------|-----------|---------------|-----------------|
| **Admin** | `3` | Seed only | Full access — manage skills, register coaches/hunters, promote students, toggle featured, all CRUD |
| **Client** | `2` | Self-register | Create projects, jobs, companies, guilds. Accept/reject proposals. Manage contracts |
| **Freelancer** | `1` | Self-register | Submit proposals, manage portfolio/services, join guilds, apply for jobs |
| **Student** | `4` | Self-register | 5-step onboarding journey, submit proposals (practice), build portfolio, join guilds, apply for jobs |
| **HeadHunter** | `5` | **Admin only** | Submit talent recommendations with AI scores, view assignments |
| **Coach** | `6` | **Admin only** | Schedule sessions, complete sessions with notes/feedback/rating |

### Role Enforcement Matrix

| Action | Admin | Client | Freelancer | Student | HeadHunter | Coach |
|--------|:-----:|:------:|:----------:|:-------:|:----------:|:-----:|
| Create Projects | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| Submit Proposals | ✅ | ❌ | ✅ | ✅ | ❌ | ❌ |
| Accept/Reject Proposals | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| Manage Contracts | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| Manage Portfolio/Services | ✅ | ❌ | ✅ | ✅ | ❌ | ❌ |
| Toggle Featured | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ |
| Create Company | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| Create Guild | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ |
| Join Guild | ✅ | ❌ | ✅ | ✅ | ❌ | ❌ |
| Post Jobs | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| Apply for Jobs | ✅ | ❌ | ✅ | ✅ | ❌ | ❌ |
| Community/Forum | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Enroll in Courses | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Wallet Operations | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Send Messages | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Submit Talent (HeadHunter) | ✅ | ❌ | ❌ | ❌ | ✅ | ❌ |
| Register Coach/HeadHunter | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ |
| Coaching Sessions | ✅ | ❌ | ❌ | ❌ | ❌ | ✅ |
| Student Onboarding | ✅ | ❌ | ❌ | ✅ | ❌ | ❌ |
| Promote Student | ✅ | ❌ | ❌ | ❌ | ❌ | ✅ |
| Assign Coach | ✅ | ❌ | ❌ | ❌ | ❌ | ✅ |
| AI Price Prediction | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |

---

## 🚀 Quick Start

### Prerequisites
- **.NET 8.0 SDK** ([download](https://dotnet.microsoft.com/download/dotnet/8.0))
- **SQL Server** (local or remote instance)
- **Visual Studio 2022 v17+** (recommended) or any IDE

### Setup

```bash
# 1. Clone the repository
git clone <repo-url>
cd DEPI_Platform

# 2. Configure connection string (appsettings.Development.json)
# Default: "Data Source=MCDIESEL;Integrated Security=True;..."

# 3. The JWT SecretKey is pre-configured for development

# 4. Run the application
cd src/DEPI.API
dotnet run

# Or open DEPI.Platform.sln in Visual Studio and press F5
```

### URLs
| Environment | URL |
|-------------|-----|
| HTTPS API | `https://localhost:10212` |
| HTTP API | `http://localhost:10213` |
| Swagger UI | `https://localhost:10212/swagger` |
| Health Check | `https://localhost:10212/health` |
```

## 📡 API Overview

### Complete Endpoint Map

| Controller | Base Route | Endpoints | Auth |
|-----------|-----------|-----------|------|
| **AuthController** | `/api/auth` | 8 (register, login, refresh, logout, me, forgot-password, reset-password, change-password) | Mixed |
| **ProjectsController** | `/api/projects` | 7 | Mixed |
| **ProposalsController** | `/api/proposals` | 6 | Authorized |
| **ContractsController** | `/api/contracts` | 8 | Authorized |
| **ReviewsController** | `/api/reviews` | 4 | Mixed |
| **WalletsController** | `/api/wallets` | 8 | Authorized |
| **ConversationsController** | `/api/conversations` | 5 | Authorized |
| **NotificationsController** | `/api/notifications` | 2 | Authorized |
| **ProfilesController** | `/api/profiles` | 5 | Mixed |
| **PortfolioController** | `/api/portfolio` | 9 | Mixed |
| **ServicesController** | `/api/services` | 9 | Mixed |
| **SkillsController** | `/api/skills` | 3 | Mixed |
| **MediaController** | `/api/media` | 4 | Authorized |
| **ConnectsController** | `/api/connects` | 8 | Mixed |
| **AIMatchingController** | `/api/ai` | 5 | Authorized |
| **PricingController** | `/api/pricing` | 1 | Public |
| **HeadHuntersController** | `/api/headhunters` | 7 | Mixed |
| **GuildsController** | `/api/guilds` | 7 | Mixed |
| **CompaniesController** | `/api/companies` | 5 | Mixed |
| **CommunityController** | `/api/community` | 5 | Mixed |
| **CoursesController** | `/api/courses` | 12 | Mixed |
| **JobsController** | `/api/jobs` | 3 | Mixed |
| **CoachingController** | `/api/coaching` | 6 | Mixed |
| **StudentsController** | `/api/students` | 9 | Authorized |

> **Total:** 24 Controllers | ~130 Endpoints

---

## 📚 Wiki

For detailed documentation, see the [Wiki](#):

| Page | Description |
|------|-------------|
| [Home](wiki/Home.md) | Wiki landing page & navigation |
| [Architecture Deep Dive](wiki/Architecture.md) | Clean Architecture, CQRS, DDD patterns |
| [API Reference](wiki/API-Reference.md) | Full endpoint catalog with request/response samples |
| [Database Schema](wiki/Database-Schema.md) | Entity relationships, table design, migrations |
| [Setup Guide](wiki/Setup-Guide.md) | Development environment setup, Docker, CI/CD |
| [Authentication](wiki/Authentication.md) | JWT flow, roles, permissions, security |
| [Modules Guide](wiki/Modules.md) | Deep dive into each business module |

---

## 🛡️ Security

- **JWT Bearer Authentication** with HMAC-SHA256 signing
- **Role-based Authorization** via `[Authorize(Roles = "...")]`
- **Rate Limiting** — 100 requests/minute with queue limit of 10
- **Global Exception Handling** — centralized middleware returns structured JSON errors
- **Input Validation** — FluentValidation pipeline for all commands/queries
- **CORS** — configurable allowed origins
- **HTTPS Redirection** — enforced
- **Soft Delete** — data is never truly deleted (auditable entities)
- **Password Policies** — digit, lowercase, uppercase, 6+ characters

---

## 📄 License

This project is proprietary. All rights reserved.

---
## 🔗 Links

| Link | Description |
|------|-------------|
| [Discord Channel](https://discord.gg/7nr8RGvZgJ) | فريق التطوير على Discord |

---

## Team
Backend
1-Ahmed Eldegla
2-Mahmoud Heggy
3-Hamsa Alaa
4-Fatma Hassan
5-Alyaa Yehia

---

<p align="center">
  <sub>Built with ❤️ using ASP.NET Core 8.0 | Clean Architecture | CQRS | DDD</sub>
</p>
