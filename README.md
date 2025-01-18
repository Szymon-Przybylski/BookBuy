# BookBuy

An application project built with .Net and microservice architecture

- Auctions Service
  - CRUD operations
  - PostgreSQL database
- Search Service
  - MongoDB database
  - search functionalities
  - database sync with auctions service
- (to be implemented) Bid Service handling bidding on auctions
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
- (to be implemented) CI/CD workflow using GitHub Actions
- Unit tests
  - XUnit, Moq, AutoFixture
- Integration tests
  - WebMotions, TestContainers
