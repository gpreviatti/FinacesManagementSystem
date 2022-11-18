# :moneybag: Finances Management System

[![CI-CD](https://github.com/gpreviatti/FinancesManagementSystem/actions/workflows/cicd.yaml/badge.svg?branch=main)](https://github.com/gpreviatti/FinancesManagementSystem/actions/workflows/cicd.yaml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=gpreviatti_FinancesManagementSystem&metric=alert_status)](https://sonarcloud.io/dashboard?id=gpreviatti_FinancesManagementSystem)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=gpreviatti_FinancesManagementSystem&metric=coverage)](https://sonarcloud.io/dashboard?id=gpreviatti_FinancesManagementSystem)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=gpreviatti_FinancesManagementSystem&metric=code_smells)](https://sonarcloud.io/dashboard?id=gpreviatti_FinancesManagementSystem)

This is a simple software to management your finances, I use concepts like:

- Solid

- Clean Code and

- Domain Driven Design

If you have any doubt or suggestion let me know or open an issue on project repository.

## :computer: Technologies

- Razor Pages
- .Net (6.0.0)
- Entity Framework Core
- XUnit
- AutoMapper
- Docker

## :runner: How to run locally

Clone the repository

Than run the following commands

Initialize database with docker

`cd ./infra`

`docker compose up db`

Running application

`cd ../src/Web`

`dotnet build`

`dotnet run`

## :runner: How to run with docker

`cd ./infra`

`docker compose up`

## Default user

Email: admin@admin.com
Pwd: admin
