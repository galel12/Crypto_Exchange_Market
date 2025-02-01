# Crypto Exchange Marketplace

## Overview

Crypto Exchange Marketplace is a web application that allows users to sign in and sign out, with authentication handled using JWT (JSON Web Tokens). The application is built using ASP.NET Core for the backend and Vue.js for the frontend.

## Features

- User sign-in and sign-out
- Authentication using JWT

## Work in Progress

This project is currently a work in progress. The following features are planned for future development:

- **Cryptocurrency Wallet Management**: Enable users to manage their cryptocurrency wallets.
- **Transaction History**: Provide users with a history of their transactions.
- **Real-time Cryptocurrency Prices**: Display real-time prices of various cryptocurrencies.

## Technologies Used

- **Backend**: ASP.NET Core
- **Frontend**: Vue.js
- **Database**: Entity Framework Core with SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **Testing**: xUnit, Moq, Microsoft.AspNetCore.Mvc.Testing

## Getting Started
### Prerequisites:
- .NET 8.0 SDK
- Node.js (for the frontend)
- SQL Server

## Setup

1. **Clone the repository**
2. **Backend Setup**:
    - Navigate to the crypto directory
    ```sh
    cd crypto
    ```
    - Restore the .NET dependencies
    ```sh
    dotnet restore
    ```
    - Update the appsettings.json file with your database connection string
    - Apply the database migrations:
    ```sh
    dotnet ef database update
    ```
    - Run the backend
    ```sh
    dotnet run
    ```
3. **Frontend Setup**:
    - Navigate to the crypto-client directory
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
    
