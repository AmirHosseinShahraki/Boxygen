# Boxygen - Secure Cloud File Storage

A **Secure Cloud File Storage** solution utilizing a **microservice architecture** and **event-driven design**. It’s built using .NET and leverages **MassTransit** for messaging with **RabbitMQ** as the transport. **Ocelot** is used to implement the API Gateway pattern, providing a unified entry point to the backend services. Each service within the architecture adheres strictly to **Clean Architecture** principles.

## Architecture
The system is composed of multiple services, each with a specific role:

**Storage Service:** Manages file storage with security and efficiency.

**Profile Service:** Handles user profile information and related operations.

**Authentication Service:** Responsible for user authentication and authorization.

**User Service:** Manages user data and orchestrates the user registration flow with a state machine-based saga.

**Email Service:** Facilitates sending emails for various parts of the application.

**Preferences Service:** Manages user preferences for UI

Each service uses Event-Driven Architecture to communicate.

## Features

**Microservice Architecture:** Each service is developed and deployed independently.

**Event-Driven Architecture:** Asynchronous communication between services with MassTransit and RabbitMQ.

**State Machine Saga for User Registration Flow:** Complex user registration process managed with a Saga.

**API Gateway Pattern:** A single entry point for clients with an API Gateway using Ocelot.

**Clean Architecture:** Each project within the services maintains a clean separation of concerns.

## Contribution
I want you to know that contributions to this project are welcome. Please adhere to the Clean Architecture guidelines when adding new features or services.

## License
This project is open-sourced under the MIT License. See the LICENSE file for more information.

## Contact
If you have any questions or would like to get in touch regarding this project, please file an issue in the repository, and I will get back to you.
