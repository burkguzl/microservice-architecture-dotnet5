# Microservice Architecture NET5 - Docker, MongoDB, RabbitMQ


### Setup 🐋


From the root of the project run following:

```sh
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
### Docker Ports
  - PhonebookAPI - http://localhost:8001
  - ReportAPI - http://localhost:8002
### Local Ports
  - PhonebookAPI - http://localhost:5001
  - ReportAPI - http://localhost:5000
### Phonebook API

  - GET - api/v1/Person : Get all persons
  - GET - api/v1/Person/{id} : Get person by id 
  - POST - api/v1/Person : Add a new person
  - DELETE - api/v1/Person/{id} : Delete person
  - GET - api/v1/Person/{location} : It sends a report creation request to the report microservice over the rabbitmq according to the location information entered.
  - POST - api/v1/Address/{personId} : Add address information to the person
  - DELETE - api/v1/Address/{personId}/{addressId} : Delete the address information of the person
### Report API
  - GET - api/v1/Report : Get all reports
  - GET - api/v1/Report/{id} : Get report by id 
### Tecnologies Used

  - MongoDb
  - Rabbitmq
  - Docker Container
  - Swagger
  - .Net 5
  - Web API
### Architecture Used
  - Clean Architecture
  - N-Layer Architecture
  - Event-Driven Architecture (RabbitMQ)
  - CQRS Design Pattern
  - Mediator Desing Pattern
  - SOLID Architecture
