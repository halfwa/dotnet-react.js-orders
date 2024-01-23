using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OrderCreator.DataAccess.Entities;

namespace OrderCreator.DataAccess
{
    /// <summary>
    /// Подготовка базы данных 
    /// </summary>
    public static class PrepDb
    {
        /// <summary>
        /// Инициализация тестовых данных в БД
        /// </summary>
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<OrderCreatorDbContext>()!);
            }
        }

        private static void SeedData(OrderCreatorDbContext context)
        {
            if (context.Orders.Any())
            {
                return;
            }
            var orders = new List<OrderEntity>
                {
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Сыктывкар",
                        FromAddress = "Ул. Будёнова",
                        ToCity = "Махачкала",
                        ToAddress = "Ул. Геройская",
                        Weight = 99,
                        PickupDate = DateTime.Parse("2024-01-22T22:19:19.4911809+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Москва",
                        FromAddress = "Ул. Тверская",
                        ToCity = "Санкт-Петербург",
                        ToAddress = "Невский проспект",
                        Weight = 75,
                        PickupDate = DateTime.Parse("2024-01-23T10:30:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Екатеринбург",
                        FromAddress = "Ул. Ленина",
                        ToCity = "Новосибирск",
                        ToAddress = "Ул. Гагарина",
                        Weight = 120,
                        PickupDate = DateTime.Parse("2024-01-24T15:45:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Ростов-на-Дону",
                        FromAddress = "Ул. Пушкинская",
                        ToCity = "Краснодар",
                        ToAddress = "Ул. Лермонтова",
                        Weight = 88,
                        PickupDate = DateTime.Parse("2024-01-25T08:00:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Владивосток",
                        FromAddress = "Ул. Строителей",
                        ToCity = "Хабаровск",
                        ToAddress = "Ул. Ленинградская",
                        Weight = 105,
                        PickupDate = DateTime.Parse("2024-01-26T14:20:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Казань",
                        FromAddress = "Ул. Кремлевская",
                        ToCity = "Нижний Новгород",
                        ToAddress = "Ул. Волжская",
                        Weight = 65,
                        PickupDate = DateTime.Parse("2024-01-27T12:10:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Сочи",
                        FromAddress = "Ул. Морская",
                        ToCity = "Адлер",
                        ToAddress = "Ул. Солнечная",
                        Weight = 110,
                        PickupDate = DateTime.Parse("2024-01-28T18:30:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Уфа",
                        FromAddress = "Ул. Ленинградская",
                        ToCity = "Челябинск",
                        ToAddress = "Ул. Советская",
                        Weight = 80,
                        PickupDate = DateTime.Parse("2024-01-29T09:45:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Томск",
                        FromAddress = "Ул. Кирова",
                        ToCity = "Омск",
                        ToAddress = "Ул. Лермонтова",
                        Weight = 95,
                        PickupDate = DateTime.Parse("2024-01-30T16:00:00.0000000+00:00").ToUniversalTime()
                    },
                    new OrderEntity
                    {
                        Id = Guid.NewGuid(),
                        FromCity = "Красноярск",
                        FromAddress = "Ул. Сибирская",
                        ToCity = "Иркутск",
                        ToAddress = "Ул. Байкальская",
                        Weight = 70,
                        PickupDate = DateTime.Parse("2024-01-31T11:15:00.0000000+00:00").ToUniversalTime()
                    }
                };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
