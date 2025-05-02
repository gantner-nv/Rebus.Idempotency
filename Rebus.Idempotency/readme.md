# Change log Rebus.Idempotency

## Version 2.3.2

* Obsoleted the use of a HashSet in the IdempotencyData

## Version 2.3.1

* Use ConcurrentDictionary instead of HashSet for IdempotencyData._handledMessageIds
