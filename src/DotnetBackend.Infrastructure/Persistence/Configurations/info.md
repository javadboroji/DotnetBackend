# HasPrecision
builder.Property(x => x.Price)
    .IsRequired()
    // HasPrecision مستقل از نوع دیتابیس (Database-Agnostic) است.
    // فقط "۱۸ رقم با ۲ رقم اعشار" را تعریف می‌کنیم و EF Core
    // آن را به نوع داده مناسب هر دیتابیس ترجمه می‌کند
    // (مثلاً در SQL Server به decimal(18,2)).
    .HasPrecision(18, 2);
# DeleteBehavior.Restrict
اجازه نمی‌دهد دسته‌بندی‌ای که محصول دارد، به‌صورت فیزیکی حذف شود.