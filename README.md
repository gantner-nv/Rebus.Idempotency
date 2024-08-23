Idempotence library for Rebus
=============================

As the official Rebus documentation states: [Idempotence](https://github.com/rebus-org/Rebus/wiki/Idempotence), when we are talking about messaging, is when a message redelivery can be handled without ending up in an unintended state.

This library ensures that message delivery in a service-oriented architecture is only handled once when you're running multiple instances of your service.
As such, only one instance of the service will be in charge of handling the message.
