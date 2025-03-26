# ASP.NET Core Web API Project

This project is a web API application developed with ASP.NET Core. It has been built to meet specified requirements.

## Requirements

- Must adhere to REST standards.
- Should follow SOLID principles.
- Implement fake services using Dependency Injection.
- Develop an extension method for use in the API.
- Implement Swagger for API documentation.
- Include a global logging middleware (e.g., logging basic information such as entering an action).
- Implement a fake user authentication system and validate it using a custom attribute.
- Create a global exception middleware.

## Fake Service Usage

- HTTP requests are received by the controller.
- Methods are defined in an interface like `IOrderService`, and a service class implements this interface.
- The controller uses the `IOrderService` interface to call the relevant method in the `FakeOrderService` class.
- The fake service processes the request and returns a response from the controller.

## All API Routes
![Screenshot 2025-03-26 190322](https://github.com/user-attachments/assets/3dd429cc-be5c-4a3a-95d3-6e788bc7cf07)

# Examples

### Create Order
Endpoint for creating an order.

![Screenshot 2025-03-26 183818](https://github.com/user-attachments/assets/6b9e3477-e668-4984-8912-37ba0fe9dfe5)

### Get Order by ID
Endpoint for retrieving a specific order.

![Screenshot 2025-03-26 183932](https://github.com/user-attachments/assets/029f3433-f713-4b3b-9ab6-1a44c18606c7)

## Fake Auth Service

- The user sends a request with their username and password to log in.
- The `Login` method in the `AuthController` processes the incoming request.
- It calls the `Login` method in the `AuthService` class to validate user credentials.
- If the username and password are correct, the system returns a success message along with the user ID.
- Otherwise, an unauthorized access message is returned.

```json
{
  "message": "Login successful!",
  "userId": 1,
  "apiKey": "my-static-api-key"
}
```
## Protected Endpoint

- When a user logs in and tries to access a specific resource or perform an action, they send a request to this protected endpoint.
- **URL**: `GET /api/auth/protected`
- The user authenticates by sending this request with a `Bearer` token.

### ✅ Successful Authentication Response
```json
{
  "message": "You have accessed the protected endpoint successfully!"
}
```

### ❌ Invalid Authentication Response
```json
{
  "message": "Access denied. Invalid API Key!"
}
```

---

## Middleware and Extension Usage

- Middleware controls the request flow.
- Extension methods add additional functionality to existing classes.
- A global logging middleware is created to log all API requests.

<p align="justify">
Middleware intercepts and processes incoming HTTP requests, while extension methods help modularize this process. The logging middleware listens to each request, records details, and uses an extension method to extend its functionality. The <code>LogActionAsync</code> method is invoked by the middleware to log details like the request path and timestamp into a file. This middleware is activated in <code>Startup.cs</code> by adding <code>app.UseMiddleware&lt;LoggingMiddleware&gt;()</code>, ensuring every request passes through the middleware chain and gets logged centrally.
</p>

### 📌 Log Message Example:
```json
Entering action: /api/auth/login at 26.03.2025 15:41:06
Login attempt for username: Seyma at /api/auth/login at 26.03.2025 15:41:06
Login successful for username: Seyma at /api/auth/login at 26.03.2025 15:41:06
```

## Global Exception Middleware

- The global exception middleware centralizes error handling within the application.
- Inside the `InvokeAsync` method, if an error occurs while processing a request, it is redirected to `HandleExceptionAsync`.
- `HandleExceptionAsync` processes the captured error, generates an appropriate response, and returns it to the client.
- In case of an error, a JSON-formatted error message is returned to the client.

### ⚠️ Error Response Example:
```json
{
  "StatusCode": 500,
  "Message": "Server error: Customer ID cannot be negative."
}


