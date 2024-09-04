Simple project to demonstrate how to use RabbitMQ as message broker.

Need to get a RabbitMQ server running using below Docker command :
docker run -d --hostname rmq --name rabbit-server -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password -p 8080:15672 -p 5672:5672 rabbitmq:3-management

Bogus is also used to generate fake data being sent to the queue and then processed by both the Receiver projects.
