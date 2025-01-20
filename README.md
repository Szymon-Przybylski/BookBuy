# **BookBuy**

## **BookBuy is a .NET application designed with a microservice architecture, supporting seamless online book auctions. The system includes services like Auctions, Search, Bidding, and Identity, each tailored to specific functions such as CRUD operations, database synchronization, and user authentication. It uses technologies like PostgreSQL, MongoDB, SignalR, and gRPC for efficient communication and real-time features. The architecture incorporates Microsoft YARP for gateway functionality, RabbitMQ for asynchronous messaging, and Next.js for the client-side app. Additional features include Dockerized deployment, unit and integration testing.**

![BookBuy architecture schema](images/BookBuy%20architecture%20schema.png "BookBuy architecture schema")

- **Auctions Service**
  - CRUD operations:
    - get all auctions
    - get auction by id
    - create new auction
    - update auction by id
    - delete auction by id
  - PostgreSQL database
- **Search Service**
  - MongoDB database
  - search functionalities: search by books name or author
  - database sync with auctions service using Http
- **Bidding Service**
  - placing bids on auctions
  - background service checking database and creating auction finished event if needed
  - allowing user to place bids on auctions not available in search service database using gRPC
  - MongoDB database
- **Identity Service**
  - Duende Identity Server
  - external identity provider
  - OpenID Connect standard
  - OAuth 2.0 with access tokens
- **Gateway Service**
  - using Microsoft YARP
  - allowing CORS
  - providing an entry point into the microservice network
- **Notifications Service**
  - using SignalR to allow real-time functionality for client app
- **Client side app**
  - using Next.js
  - using react hot toast for notifications
  - using zustand for state management
  - using Auth.js for authentication
- **Dockerization of services**
  - providing easy to use, out of the box solution
- **Synchronous communication**
  - HTTP example for search <-> auctions communication
  - gRPC example for bidding <-> auctions communication
- **Asynchronous communication**
  - RabbitMQ as message broker
- **(to be implemented) CI/CD workflow using GitHub Actions**
- **Unit tests**
  - using mainly XUnit, Moq, AutoFixture
- **Integration tests**
  - creating & sharing fixtures
  - usingWebMotions and TestContainers to help with testing
