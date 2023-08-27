# Library Management System Fullstack Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-darkgreen)
![Docker](https://img.shields.io/badge/Docker-v.24-darkblue)

Using Library Management System as the topic, this project is created as a simple demonstration of how to provide a seamless experience for library users, as well as seperate management system for administrators using the following technologies:

- Architecture: Domain Driven Design, Clean Architecture
- Design Pattern: Repository Pattern, Unit of Work Pattern, Dependency Injection, CQRS, Mediator Pattern
- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP .NET Core, Entity Framework Core
- Database: PostgreSQL
- Testing: Moq, xUnit
- Deployment: Docker
- Documentation: Swagger
- Cloud: AWS EC2, AWS RDS, AWS S3
- Version Control: Git, GitHub
- CI/CD: GitHub Actions

## Table of Contents

1. [Features](#features)
   - [User Functionalities](#user-functionalities)
   - [Admin Functionalities](#admin-functionalities)
2. [Deployment](#deployment)
3. [Getting Started Locally](#getting-started-locally)

## Features

#### User Functionalities

1. User Management: Users are able to register for an account, login/logout, and update their account information.
2. Browse Books: Users are able to view all books, search for books by title, and view details of a book.
3. Add to Cart: Users are able to add books to a shopping cart, and manage cart.
4. Checkout: Users are able to reserve books from the website.
5. Loan Management: Users are able to view their all of their current loans and loan history.

#### Admin Functionalities

1. User Management: Admins are able to view and delete users (using Postman for now).
2. Book Management: Admins should be able to view, edit, delete and add new products. Admin can create new authors and genres as well (using Postman for now).
3. Loan Management: Admins should be able to view all loans (using Postman for now).

## Deployment

This project is deployed on AWS EC2 using Docker. AWS S3 bucket is used to serve the frontend. The database is deployed on AWS RDS. To access the website, please visit: http://piu-lib-website.s3-website-eu-west-1.amazonaws.com/

## Getting Started Locally

1. Clone the repository
   `git clone https://github.com/leminhtuan2015/LibraryManagementSystem.git`
2. Change the connection string in `appsettings.Development.json` to your own PostgreSQL database.
3. Install EF Core tools: `dotnet tool install --global dotnet-ef`
4. Run `dotnet ef migrations add InitiateDb` to create the database.
5. Run `dotnet ef database update` to update the database.
6. Change directory to `LibMgmtSys.Backend` and run `dotnet run start --project Api` to start the backend.
7. Open another terminal and change directory to `LibMgmtSys.Frontend` and run `npm install` to install all dependencies.
8. In the same terminal, run `npm run start` to start the frontend.

## Documentation
[Documentation markdown file](/Docs/docs.md)
