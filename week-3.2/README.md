Delete Validation

![Ekran görüntüsü 2025-04-06 163105](https://github.com/user-attachments/assets/8969db5d-dc53-44ff-8650-4c439a1f4704)

Update Validation
![Ekran görüntüsü 2025-04-06 163041](https://github.com/user-attachments/assets/ad852748-0019-4139-aeb6-16d043cac60e)

GetByID Validation

![Ekran görüntüsü 2025-04-06 163006](https://github.com/user-attachments/assets/565fa9be-8542-4914-a504-1386e594ca33)


# 📌 Middleware Concept

Middleware are structures that intervene in the process from when a request comes from the client to when it is processed by the server and a response is sent back. In other words, by intervening in the request–response pipeline, we can filter incoming requests, modify the response before it's returned, or perform a completely new operation.

Thanks to middleware structures:

- Multiple processing chains (pipeline) can be added between the request and response.
- The order of operations in this chain can be controlled.
- It can be decided whether to continue the chain after each middleware.

# 🧪 Middleware in .NET

In .NET 5, middleware is defined inside the `Configure` method of the `Startup` class.

This method builds the middleware pipeline of the application.

# 🔁 Types of Middleware

## 1. `Run()` Method

Acts as a terminal middleware in the pipeline.

- Cannot call another middleware inside it.
- Any operations after it will not be executed.

**Example:**

```
csharp
app.Run(async context => {
    await context.Response.WriteAsync("Request ended here.");
});
```

## 2. `Use()` Method

Participates in the chain and can trigger middleware that comes after it.

- After completing the process, it can return and continue from where it left off.

**Example:**

```
csharp
app.Use(async (context, next) => {
    // before processing
    await next();
    // after processing
});
```

## 3. `Map()` Method

Allows routing middleware based on the URL path.

**Example:**

```
csharp
app.Map("/admin", adminApp => {
    adminApp.Run(async context => {
        await context.Response.WriteAsync("Admin panel");
    });
});
```

## 4. `MapWhen()` Method

Unlike `Map()`, which only works with paths, `MapWhen()` allows routing based on any condition.

**Example:**

```
csharp
app.MapWhen(context => context.Request.Query.ContainsKey("key"), appBuilder => {
    appBuilder.Run(async context => {
        await context.Response.WriteAsync("Key query found!");
    });
});
```

# 🛠 Custom Middleware and Using Extensions

We can write our own custom middleware and call them using `Use...()` extensions. This method reduces code duplication and creates a cleaner structure.

> **Note:** By convention, custom middleware methods should start with `Use`, e.g., `UseRequestLogger`.

# ⏳ Async-Await in Middleware

Middleware operations can be written asynchronously (`async`).

- If there is communication with an external system (e.g., database, API), use `await` to wait for the result.
- For operations like email services that do not require waiting, `await` may not be necessary.

Async methods return a `Task`.

# ☁️ Cloud Services for Logging

Middleware is often used for logging operations. HTTP requests or errors in the application can be logged through middleware.

Here are some logging services you can use:

- **Google Cloud Logging**
- **Azure Monitor**
- **AWS CloudWatch**
  
 Exception Handling in Middleware with Error Messaging:
 
![Ekran görüntüsü 2025-04-06 163139](https://github.com/user-attachments/assets/6d80e213-bb82-43ea-8ac8-9553b56641ae)

