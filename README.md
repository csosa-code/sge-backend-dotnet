# SGE Backend (.NET 8)

Backend del **Sistema de Gestión Empresarial (SGE)** desarrollado con **.NET 8**, siguiendo principios de **Clean Architecture (Onion Architecture)**, enfocado en escalabilidad, mantenibilidad y separación de responsabilidades.

---

## 🧠 Descripción del proyecto

Este proyecto representa una API empresarial desacoplada, diseñada para gestionar empleados, áreas y autenticación de usuarios, incorporando reglas de negocio reales y patrones de arquitectura utilizados en entornos productivos.

Se implementa una estructura modular que permite:
- Escalar funcionalidades sin acoplar capas
- Mantener lógica de negocio independiente del framework
- Facilitar testing y evolución del sistema

---

## 🏗️ Arquitectura

El proyecto está estructurado en capas siguiendo **Clean Architecture**:

- `Sge.Enterprise.Api`  
  → Exposición de endpoints REST, configuración de middlewares, autenticación y CORS

- `Sge.Enterprise.Application`  
  → Casos de uso, lógica de negocio, validaciones, DTOs, mapeos y contratos de servicios

- `Sge.Enterprise.Domain`  
  → Entidades del dominio, interfaces de repositorio y reglas base del negocio

- `Sge.Enterprise.Infrastructure`  
  → Implementaciones concretas (EF Core, repositorios, acceso a datos, configuraciones)

---

## ⚙️ Principales decisiones técnicas

- Implementación de **Unit of Work + Repository Pattern**
- Uso de **DTOs** para desacoplar el dominio de la capa de presentación
- **AutoMapper** para transformación de entidades
- Manejo centralizado de errores con **Middleware**
- Estandarización de respuestas API mediante **ApiResponse wrapper**
- Autenticación basada en **JWT**
- Separación clara de responsabilidades (SRP)

---

## 🔐 Seguridad

- Autenticación con **JWT Bearer**
- Hash de contraseñas con **HMACSHA512 + Salt**
- Endpoints protegidos con `[Authorize]`
- Validación de usuarios activos/inactivos

---

## 📦 Funcionalidades implementadas

### 👤 Empleados
- CRUD completo
- Activación / desactivación lógica
- Validaciones:
  - Salario mínimo configurable (`EmployeeSettings`)
  - Documento único
  - Área válida existente
- Soporte de:
  - Paginación
  - Filtros dinámicos
  - Ordenamiento

### 🏢 Áreas
- CRUD básico
- Validación de nombre único

### 📊 Dashboard
- Resumen de métricas:
  - Total empleados
  - Activos / inactivos
  - Planilla mensual
  - Porcentajes

### 🔑 Autenticación
- Login
- Registro de usuarios
- Generación de JWT

---

## 🔄 Manejo de respuestas

Todas las respuestas siguen una estructura estándar:

```json
{
  "status": true,
  "message": "Proceso exitoso",
  "data": {}
}
