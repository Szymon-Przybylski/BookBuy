# BookBuy

An application project built with .Net and microservice architecture

- Auctions Service
  - CRUD operations
  - PostgreSQL database
- Search Service
  - MongoDB database
  - search functionalities
  - database sync with auctions service
- (to be implemented) Biding Service handling bidding on auctions
  - placing bids on auctions
  - background service for auction finished event
  - MongoDB database
- Identity Service
  - Duende Identity Server
  - external identity provider
- Gateway Service
  - Microsoft YARP
  - an entry point into the microservice network
- Client side app
  - built with Next.js
  - using react hot toast for notifications
  - using zustand for state management
  - using Auth.js for authorization
- Dockerization of services
- Synchronous communication
  - HTTP
  - gRPC
- Asynchronous communication
  - RabbitMQ
- (to be implemented) CI/CD workflow using GitHub Actions
- Unit tests
  - XUnit, Moq, AutoFixture
- Integration tests
  - WebMotions, TestContainers
