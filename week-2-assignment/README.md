# Proje Adı

Bu proje, ASP.NET Core ile geliştirilen bir API uygulamasıdır. Proje, belirtilen gereksinimlere uygun olarak geliştirilmiştir.

## Gereksinimler

- İlk hafta geliştirdiğiniz API kullanılacaktır.
- REST standartlarına uygun olmalıdır.
- SOLID prensiplerine uyulmalıdır.
- Fake servisler geliştirilerek Dependency Injection kullanılmalıdır.
- API'nizde kullanılmak üzere extension geliştirin.
- Projede Swagger implementasyonu gerçekleştirilmelidir.
- Global loglama yapan bir middleware (sadece actiona girildi gibi çok basit düzeyde).
- Bonus: Fake bir kullanıcı giriş sistemi yapın ve custom bir attribute ile bunu kontrol edin.
- Global exception middleware oluşturun.

## Fake Servis Kullanımı

### Akış
- **Kullanıcı İsteği**: Kullanıcı, istekleri controller tarafından alınır. Controller, gelen isteği alır. `IOrderService` gibi arayüzdeki metodları implement ederek servis sınıfını oluştururuz.
- **Servis Çağrısı**: Controller, `IOrderService` arayüzünü kullanarak fake servis olan `FakeOrderService` sınıfındaki ilgili metodunu çağırır.
- **Yanıt**: Fake servis içinde gerekli işlemler yapıldıktan sonra, controllera dönen yanıt kullanıcıya iletilir.

![API Endpoint Resmi](link-to-api-endpoint-image)

### Tüm Endpointler
- **Örnek Olarak Create ve GetById API'si**:
  - **Create Order**: Sipariş oluşturma için kullanılan endpoint.
  - **Get Order by ID**: Belirli bir siparişi almak için kullanılan endpoint.

## Fake Auth Service

### Akış
- **Kullanıcı Bilgileri**: Kullanıcı, giriş yapmak için kullanıcı adı ve şifre bilgilerini içeren bir istek gönderir. `AuthController` sınıfındaki `Login` metodu, gelen isteği işler. `AuthService` sınıfından `Login` metodunu çağırarak kullanıcı bilgilerini kontrol eder. Eğer kullanıcı adı ve şifre doğruysa, sistem kullanıcıya giriş başarılı mesajı ile birlikte kullanıcı ID'sini döner. Aksi halde, yetkisiz giriş mesajı döner.

![Login Successful Mesajı](link-to-login-success-image)

### Korumalı Endpoint
- Kullanıcı, giriş yaptıktan sonra belirli bir kaynak veya işlem için erişmek istediğinde, bu korumalı endpoint'e istek gönderir.
- **URL**: `GET /api/auth/protected`

Kullanıcı, bu istekle birlikte giriş bilgilerini başlıkta (header) göndermelidir.

### Middleware ve Extension Kullanımı

#### Middleware
- **Tanım**: ASP.NET Core'da, bir HTTP isteğini işleyen bileşenlerdir. Her middleware, isteği alır, işlemesini yapar ve bir sonraki middleware'e veya controller'a geçiş yapar.
- **Loglama Middleware**: Uygulamamızda, tüm API çağrılarını loglamak için global bir loglama middleware'i oluşturduk.

#### İş Akışı
1. Kullanıcı, bir API isteği gönderir.
2. İstek, middleware zincirine girer.
3. Loglama middleware'i, istek bilgilerini loglar.
   - **Log Mesajı**: "Actiona girildi: POST /api/orders"
4. İlgili controller'daki metod çağrılır.
5. İşlem tamamlandığında, yanıt middleware aracılığıyla istemciye iletilir.

#### Extension Method
- **Tanım**: Mevcut bir sınıfa ek işlevsellik kazandırmak için kullanılan özel statik metotlardır. Middleware'i uygulamamıza eklemek için bir extension method oluşturduk.

#### İş Akışı
1. Kullanıcı bir HTTP isteği gönderir.
2. `Program.cs` dosyasında `UseGlobalLogging()` methodu çağrılır.
3. Middleware zincirine eklenen loglama middleware'i, istekleri dinler ve loglar.

### Global Exception Middleware
- Global exception middleware, uygulamanızdaki hata yönetimini basit ve merkezi bir hale getirir.
- **Hata Yakalama**: `InvokeAsync` metodu içinde, gelen isteklerin işlenmesi sırasında bir hata oluşursa, bu hatayı yakalayarak `HandleExceptionAsync` metoduna yönlendirir.
- **Hata İşleme**: `HandleExceptionAsync` metodu, yakalanan hatayı işler, uygun bir yanıt oluşturur ve bu yanıtı istemciye gönderir.
- **JSON Yanıtı**: Hata durumunda, istemciye JSON formatında bir hata mesajı döner.

## Görseller
- **API Endpoint Resmi**: Tüm endpointleri içeren bir görsel.
- **Login Successful Mesajı**: Kullanıcı girişinin başarılı olduğunu gösteren bir görsel.
