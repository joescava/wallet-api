📦 Wallet API - Prueba Técnica .NET 8

🧠 Descripción General

API REST construida en .NET 8 usando el patrón Clean Architecture para gestionar billeteras y transacciones de saldo. La solución permite:
	•	CRUD sobre billeteras
	•	Registro y consulta de transacciones
	•	Validaciones de negocio
	•	Manejo centralizado de errores
	•	Inyección de dependencias estructurada
	•	Pruebas unitarias y de integración

🏗️ Arquitectura del Proyecto

El proyecto sigue Clean Architecture y está dividido en capas:

wallet-api/
├── src/
│   ├── WalletApi.API           --> Capa de entrada (Controllers, Middleware, Program.cs)
│   ├── WalletApi.Application   --> Casos de uso, interfaces, DTOs, servicios
│   ├── WalletApi.Domain        --> Entidades del dominio y enums
│   ├── WalletApi.Infrastructure--> Persistencia (EF Core), Repositorios, Configuración
├── test/
│   ├── WalletApi.UnitTest      --> Pruebas unitarias para lógica de negocio
│   └── WalletApi.IntegrationTest --> Pruebas de integración (endpoints)

⚙️ Tecnologías Usadas
	•	.NET 8
	•	C#
	•	Entity Framework Core
	•	SQLite (por simplicidad)
	•	xUnit
	•	Clean Architecture
	•	Principios SOLID
	•	Inyección de Dependencias

🚀 Cómo Ejecutar el Proyecto
	1.	Clonar el repositorio:
        git clone <https://github.com/joescava/wallet-api.git>
        cd wallet-api

    2.	Restaurar dependencias:
        dotnet restore src/WalletApi.sln

	3.	Ejecutar migraciones:
        dotnet ef database update --project src/WalletApi.Infrastructure

	4.	Correr la API:
        dotnet run --project src/WalletApi.API

    5.	La API estará disponible en:
        http://localhost:5000

📑 Endpoints (Resumen)
	•	POST /wallet
	•	GET /wallet/{id}
	•	PUT /wallet/{id}
	•	DELETE /wallet/{id}
	•	POST /transaction
	•	GET /transaction/wallet/{walletId}

Swagger puede activarse si deseas visualizar los endpoints fácilmente.

## Preguntas Clave

1. ¿Cómo tu implementación puede ser escalable a miles de transacciones?
    La solución está construida con una arquitectura limpia que separa responsabilidades, lo cual facilita desacoplar componentes según crezca la demanda. En escenarios de alto volumen, podríamos escalar horizontalmente el servicio, distribuir las operaciones críticas con colas de eventos, y mover la base de datos a una solución más robusta como PostgreSQL o Cosmos DB. Desde la infraestructura, es clave tener métricas, monitoreo y autoescalado bien configurado para ajustar recursos dinámicamente sin afectar la operación.

2. ¿Cómo tu implementación asegura el principio de idempotencia?
    Aunque no se incluyó un identificador idempotente explícito en esta versión, la estructura de la solución permite agregarlo fácilmente en los DTOs de las transacciones. En entornos reales, incluiría un referenceId en cada transacción para evitar duplicados ante reintentos o caídas de red. Esta lógica estaría soportada a nivel de base de datos y en la lógica de negocio para asegurar que la operación solo se procese una vez.

3. ¿Cómo protegerías tus servicios para evitar ataques de Denegación de Servicio, SQL Injection y CSRF?
	De entrada, Entity Framework ayuda a prevenir SQL Injection al generar consultas parametrizadas. Para ataques de denegación de servicio, es recomendable implementar rate limiting por IP o usuario, preferiblemente gestionado desde un API Gateway. En cuanto a CSRF, al tratarse de una API REST sin sesiones ni cookies, el riesgo es bajo, pero de usarse desde navegadores se debe controlar con CORS correctamente configurado y tokens JWT bien firmados.

4. ¿Cuál sería tu estrategia para migrar un monolito a microservicios?
	Lo primero es identificar los dominios funcionales y separar lo que puede vivir de forma autónoma (por ejemplo, billeteras vs transacciones). A partir de ahí, se puede extraer cada dominio como servicio independiente, encapsular su propia lógica y base de datos, y conectarlos vía APIs. Usaría un gateway para exponer todo de forma unificada y pipelines CI/CD para mantener control en los despliegues. Es importante tener visibilidad de logs, trazabilidad distribuida y asegurar consistencia eventual si se usan colas o eventos.

5. ¿Qué alternativas a la solución requerida propondrías para una solución escalable?
	Cambiaría SQLite por una base más adecuada a producción, como PostgreSQL o Azure SQL. Para escenarios con carga variable, introduciría colas de procesamiento asincrónico, cache en Redis para datos de solo lectura, y particionado de datos si fuese necesario. A nivel de arquitectura, vale la pena considerar una evolución hacia CQRS, y monitoreo activo con Application Insights para anticipar cuellos de botella.

✅ Buenas Prácticas Git Aplicadas
	•	Ramas por feature (feature/*)
	•	Commits semánticos (feat, fix, chore, etc.)
	•	Merges progresivos a develop y main
	•	Organización modular por capa