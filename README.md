# پروژه gRPC با MediatR و Unit Test

این پروژه نمونه‌ای از پیاده‌سازی سرویس‌های **gRPC** در دات‌نت با استفاده از الگوی **MediatR** برای مدیریت منطق کسب‌وکار و همچنین پوشش دادن کدها با **Unit Test** است.

---

## ویژگی‌های اصلی پروژه

- استفاده از **gRPC** برای ارتباطات بین سرویس‌ها و کلاینت‌ها با عملکرد بالا و ساختار قوی.
- به کارگیری **MediatR** به عنوان الگوی مدیا‌تور برای تفکیک منطق کسب‌وکار، ارسال درخواست‌ها و مدیریت دستورات و کوئری‌ها.
- پوشش دادن کدها با **Unit Test** برای اطمینان از صحت عملکرد و افزایش کیفیت نرم‌افزار.
- ساختار پروژه به صورت لایه‌ای و ماژولار برای سهولت توسعه و نگهداری.

---

## نکات مهم طراحی دامنه

در این پروژه، مقادیر **نام، نام خانوادگی، کد ملی و تاریخ تولد** بهتر است به عنوان **Value Object** در طراحی دامنه پیاده‌سازی شوند تا:

- قوانین اعتبارسنجی و منطق مرتبط با این مقادیر به صورت متمرکز و قابل اطمینان مدیریت شود.
- از بروز خطاهای ناخواسته جلوگیری شود و مدل دامنه قوی‌تر و قابل توسعه‌تر باشد.

اما به دلیل محدودیت زمانی و منابع، این موضوع در نسخه فعلی پروژه کامل پیاده‌سازی نشده است و در نسخه‌های بعدی قابل بهبود است.

---

## منابع و مقالات مرجع

در طراحی و پیاده‌سازی این پروژه، از مقالات و منابع زیر بهره گرفته شده است که می‌توانید برای درک بهتر و یادگیری بیشتر به آن‌ها مراجعه کنید:

- [CRUD Operation and Microservice Communication Using gRPC in .NET Core 6 Web API](https://medium.com/@jaydeepvpatil225/crud-operation-and-microservice-communication-using-grpc-in-net-core-6-web-api-9736b18c053c)
- [.NET 6.0 gRPC Server and Client Implementation](https://dev.to/techiesdiary/net-60-grpc-server-and-client-implementation-77m)
- [Creating a gRPC API in .NET Core with MediatR Pattern Using Layered Architecture](https://medium.com/@aziz.aydn2/creating-a-grpc-api-in-net-core-with-mediatr-pattern-using-layered-architecture-eac673689301)
- [Implement gRPC Global Exception Handler in ASP.NET](https://medium.com/better-programming/implement-grpc-global-exception-handler-in-asp-net-e371fb35b7b7)

---
