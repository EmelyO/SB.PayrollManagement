SB.PayrollManagement

Sistema de gestión de nómina (payroll) — API en .NET 8 + frontend en React (Vite + TypeScript).

Arquitectura

Backend: Clean Architecture en 4 capas:
- SB.PayrollManagement.Domain: entidades y tipos base.
- SB.PayrollManagement.Application: DTOs, interfaces, servicios (lógica de negocio), extension methods de mapeo.
- SB.PayrollManagement.Persistence: DbContext, repositorios (EF Core).
- SB.PayrollManagement.Api: controllers, autenticación, configuración.

Frontend: React + Vite + TypeScript, organizado por feature, consumiendo la API vía cookie de sesión (JWT en cookie HttpOnly).

Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) 18 o superior
- SQL Server (local o remoto) con la base de datos ya creada
- Visual Studio 2022 (recomendado para el backend) o cualquier editor + dotnet CLI

1. Base de datos

La aplicación database-first: las tablas ya deben existir en SQL Server antes de correr la API (no hay migraciones de EF Core en este proyecto). Tablas necesarias:  Users , Roles, Employees, EmployeeTypes, Departments, SalariedEmployees, HourlyEmployees, CommissionEmployees, SalariedCommissionEmployees, PayrollRecords, GovernmentEntities.

Carga al menos:
Roles: Administrador, Usuario
EmployeeTypes: Asalariado, Por Horas, Por Comisión, Asalariado por Comisión

2. Backend

En SB.PayrollManagement.Api/appsettings.Development.json, configura:

json
{
  "ConnectionStrings": {
    "DbTask": "Server=TU_SERVIDOR;Database=SB_PayrollManagement;Integrated security=true;TrustServerCertificate=true"
  },
  "Jwt": {
    "Key": "una-clave-secreta-larga-y-aleatoria",
    "Issuer": "SB.PayrollManagement.Api",
    "Audience": "SB.PayrollManagement.Api"
  }
}

-Primer usuario Administrador
No hay forma de crear el primer usuario Admin desde la API (crear usuarios requiere ya estar logueado como Admin). Hay que insertarlo directo en SQL Server con el password ya hasheado en BCrypt. Puedes generarlo con un script de C# rápido o cualquier generador de hash BCrypt, y luego:


INSERT INTO Users (Username, PasswordHash, RoleId)
VALUES ('admin', '<hash-de-bcrypt>', 1); -- RoleId = 1 debe ser "Administrador"

1.Correr el backend

cd SB.PayrollManagement.Api
dotnet run --launch-profile https
La API queda en https://localhost:7248 (Swagger en /swagger).

2.Frontend

cd frontend
npm install

Crea un archivo .env (basado en .env.example):

VITE_API_URL=https://localhost:7248/api

3.Correr el frontend

npm run dev
Queda disponible en http://localhost:5173.

Nota sobre el certificado HTTPS: el certificado de desarrollo de .NET es autofirmado. Si el navegador bloquea las llamadas desde React con un error de certificado, entra una vez a https://localhost:7248/swagger directamente y acepta la advertencia de seguridad, eso resuelve el problema para el resto de la sesión del navegador.

4. Flujo de uso básico
Login → POST /api/Auth/login con el usuario Admin.
Consultar el rol/usuario actual → GET /api/Auth/me.
Crear un empleado → POST /api/Employees.
Capturar su tarifa fija según su tipo → POST /api/HourlyEmployees (o el que corresponda).
Cerrar una semana (esto calcula y guarda el pago) → POST /api/PayrollRecords.
Consultar el pago actual → GET /api/Employees/{id}/pay.
Ver el historial → GET /api/PayrollRecords/{employeeId}.
Generar el reporte semanal → GET /api/PayrollRecords/report?weekStartDate=YYYY-MM-DD.

