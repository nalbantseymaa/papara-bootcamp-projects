# Order Management API

In this project, OrderController is used for order management. An OrderItemController class can also be added to manage order items. This additional class will facilitate the management, updating and deletion of items belonging to each order. In addition, validation operations are performed at the class level, thus ensuring the accuracy and validity of the data received from the user and increasing the reliability of the application.
 
![Ekran görüntüsü 2025-03-16 174704](https://github.com/user-attachments/assets/386d5598-fdc8-4a0f-9b75-8a8d2f0a0460)

## Endpoints

### OrderController

#### Get all orders
```
GET /orders
```
Retrieves a list of all orders.

#### Get order by ID
```
GET /orders/{id}
```
Retrieves a specific order by its ID.

#### Create a new order
```
POST /orders
```
Creates a new order. The request body should include order details such as `UserId`, `Items`, and `Status`.

#### Update an existing order
```
PUT /orders/{id}
```
Updates an existing order if the status is `Pending`.

#### Update order items
```
PATCH /orders/{orderId}/items
```
Updates the quantity of existing order items.

#### Delete an order
```
DELETE /orders/{id}
```
Deletes an order by its ID.

#### Filter orders
```
GET /orders/filtered?status={status}&sortOrder={sortOrder}
```
Retrieves filtered orders based on status and sorting order.

## Validation & Error Handling
- Class-level validations ensure data integrity.
- Proper exception handling is implemented for API robustness.

## Future Enhancements
- Implement authentication and authorization.
- Add database integration with Entity Framework Core.
- Improve logging and monitoring mechanisms.
