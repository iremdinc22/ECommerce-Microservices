# 🧩 Modular E-Commerce Platform  
## Distributed Microservices Architecture

A containerized, production-oriented microservices system built with:

**ASP.NET Core · Ocelot API Gateway · CQRS + MediatR · RabbitMQ · Redis · PostgreSQL**

This project demonstrates the design and implementation of a scalable, domain-driven, event-enabled distributed system using modern backend engineering principles.

---

## 🎯 Purpose

The objective of this project is to simulate a real-world enterprise backend architecture where:

- Each domain operates as an isolated microservice  
- Services communicate via synchronous and asynchronous channels  
- Authentication and authorization are centralized but scalable  
- Infrastructure components are containerized and reproducible  
- Cross-cutting concerns are managed cleanly and consistently  

> This is not a monolithic CRUD application.  
> It is an architecture-first backend system.

---

## 🏗 System Architecture

### Service Domains

| Service | Responsibility |
|----------|----------------|
| Identity Service | JWT generation, user authentication, role management |
| Catalog Service | Product & category management |
| Basket Service | Redis-based cart management |
| Order Service | Order creation & event publishing |
| Discount Service | Coupon and pricing logic |
| Comment Service | Product review management |
| Statistics Service | Aggregated reporting |
| Message Service | Event-driven message handling |
| API Gateway (Ocelot) | Centralized routing & entry point |

### Service Characteristics

Each service:

- Owns its own database (**Database-per-Service pattern**)  
- Is containerized  
- Is independently deployable  
- Exposes REST endpoints  
- Uses DTO-based responses  

---

## 🔁 Communication Model

### 1️⃣ Synchronous Communication

All external client requests flow through:

```text
Client → Ocelot API Gateway → Target Microservice
```

This ensures:

- Centralized routing  
- Simplified client-side integration  
- Single entry point enforcement  
- Token validation at gateway level  

---

### 2️⃣ Asynchronous Communication (Event-Driven)

Certain operations (e.g., order creation) publish events via RabbitMQ.

Example flow:

```text
Order Created → RabbitMQ → Message Service → Notification / Logging
```

Benefits:

- Loose coupling  
- Non-blocking operations  
- Improved scalability  
- Clear domain boundaries  

---

## 🧠 Architectural Patterns Applied

- Microservices Architecture  
- Clean Architecture (per service)  
- CQRS (Command Query Responsibility Segregation)  
- MediatR Pattern  
- Repository Pattern  
- API Gateway Pattern  
- Event-Driven Architecture  
- Distributed Caching  
- Token-based Security  
- Service-Level Database Isolation  

---

## 🔐 Security Model

- JWT-based authentication  
- Role-based authorization  
- Custom token handler logic  
- Client credential support  
- Gateway-level token validation  
- Identity service separation  

This structure prevents tight coupling between authentication logic and business domains.

---

## ⚡ Performance & Scalability Considerations

### Redis Caching (Basket Service)

- Session-level cart persistence  
- Reduced relational database load  
- Faster response times  

### Async Processing

- Non-blocking I/O  
- Task-based asynchronous pattern  
- Improved throughput  

### Database Isolation

- Independent scaling per service  
- Avoids shared schema coupling  
- Supports future horizontal scaling  

### DTO-Based Response Layer

- Prevents entity leakage  
- Versioning flexibility  
- Clear contract boundaries  

---

## 🐳 Infrastructure

All components are containerized using Docker.

### Included Services

- PostgreSQL (multiple instances)  
- Redis  
- RabbitMQ  
- All microservices  
- Ocelot Gateway  

### Run Locally

```bash
docker-compose up --build
```

This ensures reproducibility and environment consistency.

---

## 📡 External Integrations

- Google Cloud Storage (image handling)  
- SignalR (real-time updates)  
- RabbitMQ (message broker)  

---

## 🧩 Design Decisions & Trade-Offs

### Why Database per Service?

To ensure service autonomy and prevent cross-service schema dependency.

### Why CQRS?

To separate write complexity from read optimization and maintain clarity in domain intent.

### Why Ocelot?

To centralize routing and enable scalable API management without exposing internal service topology.

### Why RabbitMQ?

To decouple services and support asynchronous processing patterns.

### Why Redis?

To optimize state-heavy operations (basket) and reduce relational database load.

---

## 🧪 Operational Considerations

Potential production upgrades:

- Kubernetes deployment  
- Centralized logging (ELK / Seq)  
- OpenTelemetry tracing  
- Circuit breaker pattern  
- Rate limiting  
- Health check endpoints  
- CI/CD integration  

---

## 📁 Project Structure

```bash
/src
 ├── Services
 │    ├── IdentityService
 │    ├── CatalogService
 │    ├── BasketService
 │    ├── OrderService
 │    ├── DiscountService
 │    ├── CommentService
 │    └── StatisticsService
 ├── Gateway
 │    └── OcelotGateway
 ├── Shared
 └── docker-compose.yml
```

---

## 👩‍💻 Author

**İrem Dinç**  
Computer Engineer  
Backend & Distributed Systems Enthusiast  

