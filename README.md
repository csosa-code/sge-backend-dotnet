# SGE Backend (.NET 8)

Backend del **Sistema de Gestión Empresarial (SGE)** desarrollado con **.NET 8** usando arquitectura **Onion / Clean**, con las siguientes capas:

- `Sge.Enterprise.Api`           → Capa de presentación (API REST)
- `Sge.Enterprise.Application`   → Casos de uso, servicios, DTOs, validaciones
- `Sge.Enterprise.Domain`        → Entidades y contratos (repositorios, UoW)
- `Sge.Enterprise.Infrastructure`→ EF Core, DbContext, repositorios, migraciones

Esta versión corresponde a la **v1** del backend y contiene:

- CRUD básico de **Áreas**
- CRUD básico de **Empleados**
- Validaciones de negocio:
  - Salario mínimo por configuración (`EmployeeSettings`)
  - Número de documento único (`DocumentNumber`)
  - Área válida (`AreaId` existente)
- Respuestas estándar:
  ```json
  {
    "status": true,
    "message": "Proceso exitoso",
    "data": null | {} | []
  }