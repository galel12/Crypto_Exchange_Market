# Crypto Exchange Marketplace

## Overview

Crypto Exchange Marketplace is a web-based platform that provides **secure user authentication, cryptocurrency wallet management, and real-time transaction tracking**.  
The application is built with **ASP.NET Core** for the backend and **Vue.js** with **TypeScript** for the frontend, ensuring a **modern, scalable architecture**.

## Features

### ✅ Implemented Features:
- 🔐 **Secure User Authentication**: Users can sign up, log in, and log out securely using JWT.
- 📜 **Protected Routes**: Certain pages (e.g., Wallet) are restricted to authenticated users.
- 🌙 **Dark Mode**: Users can toggle between dark and light mode for an enhanced UI experience.
- ⚡ **State Management with Pinia**: Optimized frontend state management for scalability.

## Work in Progress

This project is currently a work in progress. The following features are planned for future development:

### 🚀 Planned Features:
- 💰 **Cryptocurrency Wallet Management**: Allow users to store and manage crypto assets.
- 📈 **Real-time Cryptocurrency Prices**: Fetch and display live prices of various cryptocurrencies.
- 🔄 **Transaction History**: Enable users to view past transactions with full details.


## Technologies Used

- **Backend**: ASP.NET Core
- **Frontend**: Vue.js (Vite + TypeScript)  
- **State Management**: Pinia 
- **Database**: PostgreSQL (via Docker)
- **Authentication**: JWT (JSON Web Tokens)
- **Testing**: xUnit, Moq, Microsoft.AspNetCore.Mvc.Testing

## Project Structure

```markdown
Crypto-Exchange-Marketplace/
├── crypto/               # Backend - ASP.NET Core Web API
│   ├── Controllers/      # API endpoints (e.g., UserController.cs)
│   ├── Services/         # Business logic (e.g., UserService.cs)
│   ├── Repositories/     # Data access layer (e.g., UserRepository.cs)
│   ├── Models/           # Database models (e.g., User.cs)
│   ├── Dtos/             # Data Transfer Objects (e.g., NewUserDto.cs)
│   ├── Data/             # Database context (e.g., AppDbContext.cs)
|   ├── root/             # Docker configuration for PostgreSQL (docker-compose.yml)
│   ├── appsettings.json  # Configuration file for DB connection, JWT, etc.
│   ├── Program.cs        # Main entry point of the backend and application configuration
│
├── crypto-client/        # Frontend - Vue.js application
│   ├── src/              # Source code
│   │   ├── components/   # Vue components (e.g., Login.vue, Wallet.vue)
│   │   ├── router/       # Vue Router configuration (index.ts)
│   │   ├── stores/       # Pinia state management (Auth store)
│   │   ├── App.vue       # Root Vue component
│   │   ├── main.ts       # Main entry file for Vue app
│   │   ├── style.css     # Global styles
│   ├── public/           # Static assets (e.g., index.html)
│   ├── package.json      # Frontend dependencies and scripts
│   ├── vite.config.ts    # Vite configuration for Vue.js
│
├── Tests/                # Unit and integration tests (backend)
│   ├── UnitTests/        # Tests for individual components (e.g., UserServiceTests.cs)
│   ├── IntegrationTests/ # End-to-end tests (e.g., UserControllerIntegrationTests.cs)
│
├── README.md             # Project setup and documentation
└── .gitignore            # Ignored files for Git
```

## Getting Started
### Prerequisites:
Before setting up the project, ensure you have the following installed:

- **[.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)** - Required for running the backend.
- **[Node.js](https://nodejs.org/)** - Required for running the frontend.
- **[Docker & Docker Compose](https://docs.docker.com/get-docker/)** - Required for setting up the PostgreSQL database.

## Setup

1. **Clone the repository**
2. **Database setup**:
    - Ensure Docker is installed and running.
    - Navigate to the `root` directory:
    ```sh
    cd crypto/root
    ```
    - Ensure Docker Compose is installed
    - Start the PostgreSQL container using Docker Compose:
    ```sh
    docker-compose up
    ```
4. **Backend Setup**:
    - Navigate to the `crypto` directory
    ```sh
    cd crypto
    ```
    - Restore the .NET dependencies
    ```sh
    dotnet restore
    ```
    - Update the  `appsettings.json` file with your database connection string
    - Apply the database migrations:
    ```sh
    dotnet ef database update
    ```
    - Run the backend
    ```sh
    dotnet run
    ```
5. **Frontend Setup**:
    - Navigate to the `crypto-client` directory
    ```sh
    cd ../crypto-client
    ```
    - Install the Node.js dependencies
    ```sh
    npm install
    ```
    - Run the frontend
    ```sh
    npm run dev
    ```

## Contributing

Contributions are welcome! If you’d like to add features, fork the repo, create a new branch, and submit a pull request.
    
