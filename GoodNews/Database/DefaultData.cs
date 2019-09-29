using Microsoft.AspNetCore.Identity;
using Shared.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Responses;
using Shared.Links;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Shared.Prices;

namespace GoodNews.Database
{
    public class DefaultData
    {

        private static List<string> orgNames = new List<string>
        {
            "Лукойл",
"X5 Retail Group",
"Сургутнефтегаз",
"Магнит",
"Татнефть",
"Группа компаний Мегаполис",
"Evraz",
"НЛМК",
"Новатэк",
"UC Rusal",
"VEON (Vimpelcom)",
"Норильский никель",
"Сибур",
"Северсталь",
"МТС",
"ММК",
"Группа УГМК",
"Мегафон",
"Лента",
"Металлоинвест"
        };
        private static List<string> userNames = new List<string>
        {
            "Фомичёв Лавр Федотович",
"Пономарёв Аверьян Яковович",
"Самойлов Аверкий Аристархович",
"Алексеев Кондратий Михаилович",
"Орлов Ярослав Проклович",
"Мухин Ипполит Яковлевич",
"Виноградов Остап Ярославович",
"Егоров Гаянэ Протасьевич",
"Вишняков Кондрат Валерьянович",
"Беспалов Панкрат Григорьевич",
"Евсеев Елисей Борисович",
"Сысоев Наум Аркадьевич",
"Ширяев Аверьян Филатович",
"Русаков Леонард Евсеевич",
"Шилов Артем Геннадьевич",
"Лазарева Марианна Тарасовна",
"Николаева Весняна Еремеевна",
"Орлова Рая Федоровна",
"Шарапова Нина Пётровна",
"Селиверстова Арьяна Михайловна",
"Князева Наталья Ильяовна",
"Лукина Лили Мироновна",
"Гаврилова Видана Никитевна",
"Устинова Мия Аристарховна",
"Назарова Эллада Тимуровна",
"Брагина Магда Протасьевна",
"Ширяева Эмма Пантелеймоновна",
"Ефимова Розалина Валерьяновна",
"Антонова Инесса Андреевна",
"Мясникова Биргит Федосеевна",
        };


        private class checker : IEqualityComparer<string>
        {
            public bool Equals([AllowNull] string x, [AllowNull] string y)
            {
                var a = x.Split(" ")[0] + " " + x.Split(" ")[1];
                var b = x.Split(" ")[0] + " " + x.Split(" ")[1];
                return a == b;
            }

            public int GetHashCode([DisallowNull] string obj)
            {
                var a = obj.Split(" ")[0] + " " + obj.Split(" ")[1];
                return a.GetHashCode();
            }
        }

        public static void FillData(IServiceProvider serviceProvider)
        {
            userNames = userNames.Distinct(new checker()).ToList();
            var random = new Random(123);
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var context = serviceProvider.GetService<GoodNewsContext>();
            var logger = serviceProvider.GetService<ILogger<DefaultData>>();
            var rolesManager = serviceProvider.GetService<RoleManager<Role>>();

            var users = new List<User>();
            var orgs = new List<Organization>();
            var works = new List<Work>();
            var networks = new List<SocialNetworkType>();
            var cities = new List<City>();
            var priceTypes = new List<PriceType>();
            var prices = new List<Price>();


            rolesManager.CreateAsync(new Role { Id = 1, Name = "viewer" }).Wait();
            rolesManager.CreateAsync(new Role { Id = 2, Name = "publisher" }).Wait();


            for (int i = 0; i < userNames.Count; i++)
            {
                var target = userNames[i].Split(" ");
                var id = i + 1;
                var user = new User
                {
                    Id = id,
                    UserName = $"{target[0]} {target[1]}",
                    LastName = target[0],
                    MiddleName = target[2],
                    Email = $"test{id}@rtuitlab.ru"
                };

                userManager.CreateAsync(user, "MyLongPassword").Wait();
                userManager.AddToRoleAsync(user, "viewer");
                users.Add(user);
            }

            for (int i = 0; i < orgNames.Count; i++)
            {
                var target = orgNames[i];
                orgs.Add(new Organization
                {
                    Id = i + 1,
                    FullName = target,
                    ShortName = target,
                    Description = "Пустое описание"
                });
            }

            for (int i = 0; i < Cities.CityNames.Length; i++)
            {
                var city = new City
                {
                    Id = i + 1,
                    Name = Cities.CityNames[i]
                };
                cities.Add(city);
            }

            works.Add(new Work { Id = 1, Title = "Some work", Deadline = DateTime.UtcNow.AddDays(random.Next(10)), WorkType = WorkType.Handle, Publisher = users[0] });
            works.Add(new Work { Id = 2, Title = "Another work", Deadline = DateTime.UtcNow.AddDays(random.Next(10)), WorkType = WorkType.ContentApproved, Publisher = users[0] });
            works.Add(new Work { Id = 3, Title = "Волонтеры очистили пляжи города от мусора", Deadline = DateTime.UtcNow.AddDays(random.Next(10)) });
            works.Add(new Work { Id = 4, Title = "В Кировской области прошел первый волонтёрский забег #МыспортTEAM", Deadline = DateTime.UtcNow.AddDays(random.Next(10)), WorkType = WorkType.Published, Publisher = users[0] });
            works.Add(new Work { Id = 5, Title = "Состоялся последний полуфинал ежегодного Всероссийского конкурса «Доброволец России – 2019»,", Deadline = DateTime.UtcNow.AddDays(random.Next(10)), WorkType = WorkType.Published, Publisher = users[0] });
            works.Add(new Work { Id = 6, Title = "Another work", Deadline = DateTime.UtcNow.AddDays(random.Next(10)) });

            networks.Add(new SocialNetworkType { Id = 1, Title = "VK", Url = "https://vk.com" });
            networks.Add(new SocialNetworkType { Id = 2, Title = "YouTube", Url = "https://youtube.com" });
            networks.Add(new SocialNetworkType { Id = 3, Title = "Instagram", Url = "https://instagram.com" });
            networks.Add(new SocialNetworkType { Id = 4, Title = "Twitter", Url = "https://twitter.com" });

            priceTypes.Add(new PriceType { Id = 1, Name = "Деньги", Description = "Можно получить денежное вознаграждение" });
            priceTypes.Add(new PriceType { Id = 2, Name = "Лайки", Description = "Работа на добровольной основе за отзыв" });

            foreach (var work in works)
            {



                if (random.Next(2) == 1)
                {
                    var user = users[random.Next(users.Count)];
                    work.PersonInitiator = user;
                    work.ContactPerson = user;
                }
                else
                {
                    work.OrganizationInitiator = orgs[random.Next(orgs.Count)];
                    work.ContactPerson = users[random.Next(users.Count)];
                }
                work.PlaceCity = cities[random.Next(cities.Count)];
                var networkscount = random.Next(networks.Count);
                if (work.Id == 3)
                {
                    work.WantedRecources = new List<NetworkTypeToWork> { new NetworkTypeToWork { SocialNetworkTypeId = 1 } };
                }
                else if (work.Id == 4)
                {
                    work.WantedRecources = new List<NetworkTypeToWork> { new NetworkTypeToWork { SocialNetworkTypeId = 1 } };
                    work.PublishedResources = new List<SocialNetworkLink> { new SocialNetworkLink { SocialNetworkTypeId = 1, Value = @"
<div id=""vk_post_73739616_450""></div>
<script type=""text/javascript"" src=""https://vk.com/js/api/openapi.js?162""></script>
<script type=""text/javascript"">
  (function() {
    VK.Widgets.Post(""vk_post_73739616_450"", 73739616, 450, 'YcqVvfvGFf6fRKObE_Jf6_29NxU');
                } ());
</ script >" } };  
                }
                else if (work.Id == 5)
                {
                    work.WantedRecources = new List<NetworkTypeToWork> { new NetworkTypeToWork { SocialNetworkTypeId = 1 }, new NetworkTypeToWork { SocialNetworkTypeId = 2 } };
                    work.PublishedResources = new List<SocialNetworkLink> { new SocialNetworkLink { SocialNetworkTypeId = 1, Value = @"<div id=""vk_post_11753568_6421""></div>
<script type=""text/javascript"" src=""https://vk.com/js/api/openapi.js?162""></script>
<script type=""text/javascript"">
  (function() {
    VK.Widgets.Post(""vk_post_11753568_6421"", 11753568, 6421, 'rCirQC7QPE-g_B2MGvSdbNKaUQM');
                } ());
</ script > " } , new SocialNetworkLink { SocialNetworkTypeId = 2, Value = "1raISlSwOjs" } };
                }
                else
                {

                    work.WantedRecources = Enumerable.Repeat(0, 3).Select(_ => networks[random.Next(networks.Count)]).Distinct().Select(t => new NetworkTypeToWork { SocialNetworkType = t }).ToList();
                }

                var pricesCount = random.Next(1, 3);
                if (pricesCount == 2)
                {
                    work.Prices = new List<Price> { new Price { PriceType = priceTypes[1] }, new Price { PriceType = priceTypes[0], Value = $"Около {random.Next(2, 101) * 100} руб." } };
                }
                else if (random.Next(2) == 1)
                {
                    work.Prices = new List<Price> { new Price { PriceType = priceTypes[0], Value = $"Около {random.Next(2, 101) * 100} руб." } };
                }
                else
                {
                    work.Prices = new List<Price> { new Price { PriceType = priceTypes[1] } };
                }
                work.Description = LoremIpsumBuilder.GetLorem(random, 50, 150);
                work.TaskText = LoremIpsumBuilder.GetLorem(random, 10, 50);
                if (work.Id == 3)
                {
                    work.Description = @"
Волонтеры города Туапсе смогли получить финансирование
от мэрии города, купить все необходимое и сделали большое
дело - очистили все пляжи города. 
Тем самым они спасли морских жителей, обитающих в 
пребрежной зоны, от опасного пластика, резины, латекса и
иных предметов, ухудшающих экологическое состояние 
побережья.";
                    work.TaskText = @"
Связаться с контактным лицом и получить от него подробную
информацию о событии, создать публикацию, опубликовать 
ее в средствах массовой информации.";
                }
                if (work.Id == 4)
                {
                    work.Description = @"
21 сентября в городе Вятские Поляны Кировской области состоялся первый волонтерский забег, в рамках всероссийского дня бега ""Кросс Нации -2019"", в котором приняло участие более 30 волонтёров различных организаций:

- региональное движение #МыспортTEAM;
- Ресурсный центр по развитию добровольчества в Кировской области;
-Дворец молодежи / Киров;
-благотворительный фонд ""Старость в Радость"";
-Совет молодёжи г. Вятские Поляны;
-Волонтерский отряд ""Путь к успеху"".";
                    work.TaskText = @"Благодаря участию в этом забеге, многие организации познакомились между собой и в дальнейшем готовы сотрудничать. А также все мы приобщились к спорту!";
                }
                if (work.Id == 5)
                {
                    work.Description = @"На площадке Окружного форума добровольцев Центрального федерального округа «Добро в сердце России» состоялся последний полуфинал ежегодного Всероссийского конкурса «Доброволец России – 2019», где проекты участников прошли очную оценку экспертов. Всего от ЦФО полуфиналистами стали 247 инициатив. Итоги проведения всех полуфиналов, которые прошли в 5 регионах страны и МДЦ «Артек», станут известны 13 октября.";
                    work.TaskText = @"На участие в конкурсе этого года от добровольцев Центрального федерального округа во всех категориях поступило 6328 заявок, из которых до экспертной оценки были допущены 1392 инициативы.";
                }
            }

            context.SocialNetworkTypes.AddRange(networks);

            context.Works.AddRange(works);

            context.Organizations.AddRange(orgs);

            context.Cities.AddRange(cities);

            logger.LogInformation($"networks: {networks.Count} works: {works.Count} orgs: {orgs.Count} users: {users.Count} cities: {cities.Count}");
            context.SaveChanges();
        }
    }
}
