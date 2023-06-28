# Barbado-vl_Site
Обучающий, лабораторный, side проект по роликам с youtube канала Семена Алексеева [«Создание сайта с нуля на ASP.NET Core MVC»](https://www.youtube.com/watch?v=DKDCKFWNBbk&t=306s).

Задача – создание сайта-визитки из домашней страницы и страниц услуг, плюс админская страница для редактирования.

**Содержание:**
- [Технологии и зависимости](#main-section-1)
- [Структура и архитектура проекта](#main-section-2)
- [Точка входа и настройка приложения](#main-section-3)
- [«Domain»](#main-section-4)
- [Контроллер](#main-section-5)
- [Models и Sidebar](#main-section-6)
- [Views](#main-section-7)

## Технологии и зависимости <a name="main-section-1"></a>
Среда разработки, IDE – Visual Studio 2019

БД – MS SQL Server 2019
ORM – Entity Framework Core

Платформа: .NET 6 и ASP.NET Core 
Шаблон: ASP.NET Core MVC

Frontend – готовая bootstrap разметка со стилями с SASS с сайта https://html5up.net.
Редактор – CK-Editor.

Пакеты:
  - ORM и окружение:
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Identity
  - Журнал – Microsoft.Extentions.Logging.Debug
  - Scaffolding для ASP.NET Core– Microsoft.VisualStudio.Web.CodeGeneration.Design

Расширения для Visual Studio. Оптимизация web-assets и JS скриптов:
  - Web Compiler (создаем файл compileconfig.json)
  - Bundler & Minifire (создали файл bundleconfig.json и script.js, объединяющий js файлы. Ссылка на это файл лежит в ScriptPartial.cshtml)


## Структура и архитектура проекта <a name="main-section-2"></a>
Папки и файлы настроек конфигурации:
  - /Properties
    - launchSettings.json – настройка хоста
    - /PublishProfiles – ???
  - appsetting.json – настройки приложения (строка подключения к MS SQL Server, настройки logging)
  - Barbado-vl_Site.csproj – информация о зависимостях.
  - bundleconfig.json – файл с ссылками на JS скрипты, созданный с помощью Bundler & Minifire.
  - compilerconfig.json – оптимизация web-assets и стилей, файл создан при помощи Web Compiler
  - Program.cs – главный файл приложения, в нем настраивается сборка, прописываются DI и осуществляется вызов на исполнение приложения. Настройку и DI перенесли в Startup.cs.
  - Startup.cs – настройка (composition root) и DI.

Модули приложения:

  - **/Domain** – модель.
    - /Entities – модель данных
    - /Repositories – обращения к данным
      - /Abstract -- интерфейсы
      - /EntityFramework -- реализация
    - AddDBContext.cs – настройа ORM Entity Framework
    - DataManager.cs – менеджер обращения к данным
  - **/Models** – модели данных части представления (Views)
    - /ViewComponents/SidebarViewComponents.cs – данные дляSidebar.
    - LoginViewsModel.cs – данные требуемые от пользователя для аутентификации. 
  - **/Controllers**/… -- контроллеры для страниц исключая админские.
  - **/Views** – «представления».
    - /Account – страница аутентификации для админа.
    - /Home – главная страница.
    - /Services – страница со списком услуг.
    - /Shared – шаблон сайта.
      - _Layout – шаблон сайта (мастер страницы), где вызываются все модулиPartial.
      - /Components/Sidebar/Default.cshtml – sidebar.
      - файлы-модули Partial: CssPartial, ScriptsPartial, FooterPartial, HeaderPartial, MetatagsPartial, SidebarPartial.
    - _VeiwStart.cshtml – вызов шаблона сайта.
    - _ViewImports.cshtml – импортируем backend часть (/Models, /Domain/Entities, /Service,/Models/ViewComponents).
  - **/Areas** – контроллер и представления для страниц администратора
    - /Admin
      - /Views/…
      - /Controllers…
  - **/Service** – функциональные объекты.
    - AdminAreaAuthorizations.cs – реализация авторизация, чтобы перенаправлять админа на страницы Admin Area вместо обычных.
    - Config.cs – данные приложения для клиентов: имя, автор, email, строка подключения к БД.
    - Extensions – расширение методов типов (добавляем к типу string метод CutController).
  - **/wwwroot** – bootstrap папка с стилями, шрифтами, картинками и JS скриптами, суда же вставляем файлы CK-Editor-а.


Папка /Migration, создаваемая EF для файлов миграции.

Архитектура задается шаблоном ASP.NET Core MVC, который задает папки Model Views Controllers, и, который привязывает маршруты между ними. Далее, мы сами выделяем папку Domain для работы с данными бизнес логики; папку Areas, куда выносим представления и контроллеры для админских страниц; папку Service для общих функциональных моментов.


## Точка входа и настройка приложения <a name="main-section-3"></a>
*Program.cs*
*Startup.cs*
*/Service/Config.cs*

Был использован расширенный вариант настройки и запуска приложения через IHostBuilder.

В файле Program.cs точка входа в приложение, метод Main(string[] args).Внутри него вызываем экземпляр IHostBuilder, который через статический класс Host принимает настройки в виде класса Startup, а затем вызывает методы Build() и Run().

Класс Startup (файл Startup.cs). В нем определяются интерфейсы принимающие свойств и настраивающие поведение приложение.

*IConfiguraiotn* – задается как свойство и, далее вызывается встроенный метод Bind, в который передаем объект Config (/Service/Config.cs). Для работы приложения в Config.cs находится строка подключения к MS SQL Server.

*IServiceCollection* – настройка DI.
  - Подключение интерфейсов бизнес логики, это файлы Repositories и DataManager.cs.
  - Подключение DbContext, в которой передается строка подключения из свойства IConfiguraiotn.
  - Подключение и настройка Identity модели.
  - Подключение и настройка сервиса Cookie.
  - Подключение Авторизации с настройкой политик.
  - Подключение сервиса MVC – AddControllersWithViews.

*IApplicationBuilder* – прописываем конвейер вызовов. Порядок вызовов очень важен.
  - app.UseStaticFiles() – устанавливает пути к файлам по умолчанию.
  - app.UseRouting() – установка маршрутизации, по умолчанию.
  - app.UseCookiePolicy()
  - app.UseAuthentication()
  - app.UseAuthorization()
  - app.UseEndpoints() – настройка сопоставления запросов с контроллерами.


## «Domain» <a name="main-section-4"></a>
Модель бизнес логики /Domain:
  - */Entities* – схемы сущностей
    - *EntityBase.cs* – общие данные
    - *ServiceItem.cs* – описание услуги
    - *TextField.cs* -- текст
  - */Repositories* – обращения к данным
    - */Abstract* – интерфейсы
      - *IServiceItemsRepository.cs*
      - *ITextFieldsRepository.cs*
    - */EntityFramework* – реализация
      - *EFServiceItemsRepository.cs*
      - *EFTextFieldsRepository.cs*
  - *AddDBContext.cs* – настройка ORM.
  - *DataManager.cs* – менеджер обращения к данным

**Entities**. Схемы объектов бизнес логики. Есть общая схема *EntityBase*, и две наследующие от ней *ServiceItem* и *TextField*.
В схемы сущностей, для каждого свойства добавлены атрибут Display, в котором указывается запись, которая будет выводится при запросе данного свойства, и атрибут Required, указывающий, что данное свойство должно быть обязательно прописано при создании экземпляра схемы. Атрибуты берутся из библиотеки System.ComponentModel.DataAnnotations.

**AddDBContext.cs**. Определяем класс, наследующий от класса DbContext, или IdentityDbContext, если будет использоваться встроенная в Entity Framework модель данных для пользователя. Задача данного класса определить схемы данных, по которым будут созданы таблицы. Для этого указываются свойства типа DbSet<T> в который передаются схемы сущностей.
  > public DbSet<TextFiel> TextField { get; set; }
 
Во вторых в конструкторе класса указываются данные подключения к «поставщику БД», тип DbContextOptions<T>, где T это название нашего класса AddDbContext. Поскольку установку свойства идет в Startup.cs, где будет создаваться экземпляр класса, то тут мы просто указываем установку свойств от родителя.
  > public AddDbContext(DbContextOptions<AddDbContext> options) : base(options) { }
> 
В третьих переписываем метод создания модели БД OnModelCreating(ModelBuildre buildre), внутри пользуемся возможностью сразу же добавить в БД часть данных, а главное пользователя администратора.

**Repositories**. Интерфейсы (/abstract) для каждой схемы, где заявляются методы работы со схемой данных бизнес логики. И класс реализации методов (/EntityFramework). Набор методов стандартный: получить все записи по сущности, получить одну запись, удалить запись. Обращение идет к экземпляру класса AddDbContext, который дает методы обращения к таблицам БД (DbSet<T>) и методам работы с ними. Он устанавливается как свойство класса Repository и инициализируем в его конструкторе.

**DataManagr**. Класс абстракция для объединения вызова интерфейсов по каждой сущности. Через него идет обращение к классам Repositories.


## Контроллер <a name="main-section-5"></a>
В структуре проекта контроллеры находятся в:
  - /Controllers/
    - AccountController.cs
    - HomeController.cs
    - ServicesController.cs
  - /Areas/Admin/Controllers/
    - HomeController.cs
    - ServiceItemsController.cs
    - TextFieldsController.cs

**HomeController.cs и ServicesController.cs из папки Controllers.**
Вызов через DI экземпляра DataManager для чтения данных из БД.
Методы не атрибутированы. Все возвращают в качестве действия метод класса Controller View(), в который передаются данные из БД. За кулисами метода View данные встроятся в объект представления по одноименному с названием вызываемого метода и названием контроллера страницы. Пример – HomeController и метод Index() дают маршрут Views/Home/Index.cshtml.
В ServiceController.cs в методе Index(Guid id) есть развилка, где мы переопределяем маршрут указывая имя “Show”.

**AccountController.cs.** В нем Представлены методы для Аутентификации пользователя и Logout для аутентифицированного пользователя. Чтобы фильтровать доступ для класса и для методов добавлены атрибуты из AspNetCore.Authorization: [Authorize] для класса и метода Logout, [AllowAnonymous] для методов Login.

Login 2 метода.

* *IActionResult Login(string returnUrl)* *. Он введен для того, чтобы запомнить адрес “admin” при введении которого идет перенаправление на “account/login”. Адрес запоминается в свойстве динамического представления ViewBag.returnUrl.
  
* *async Task<IActionResult> Login(LoginViewModel model, string returnUrl)* *. Метод помечен атрибутом [HttpPost] и срабатывает и отправке запроса при нажатии кнопки и отправки формы из представления. Данные формы заполняют параметр model и по ним проводится аутентификация пользователя.

Для аутентификации вызываются объекты UserManager<T> и SignInManager<T>. Через них идет обращение к БД и проверка пользователя. По результатам проверки идет перенаправления на страницу из параметра returnUrl (метод Redirect()) или вывод сообщения “Неверный логин или пароль”.

**Контроллеры, классы, в /Areas/Admin**. Они помечены атрибутом [Area(“Admin”)]. В файле Startup.cs при DI сервиса AddControllersWithViews добавлено соглашение:
  >x => {
      x.Conventions.Add(new AdminAreaAuthorization(“Admin”, “AdminArea”));
  };

Оно устанавливает требование для авторизации. Политику “AdminArea” добавляем в сервис AddAuhorization. 
  >x => {
    x.AddPolicy(“AdminArea”, policy => { policy.RequareRole(“admin”); });
  };

Данные классы откликнуться только на запросы при пользователе прошедшем аутентификацию и имеющем роль «admin».

Внутри методы редактирования помеченные атрибутом [HttpPost]. Свойство DataManager для обращения к БД и возврат метода класса Controller View().

Здесь же используется метод расширения для типа String CutController, добавленный в /Service/Extensions.cs. Он используется для отрезания слова Controller от названия класса, чтобы получить маршрут для перенаправления на страницу (метод RedirecteToAction() возвращаемый в IActionResult).


## Models и Sidebar <a name="main-section-6"></a>
Структура:
  - /Models – модели данных части представления (Views)
    - /ViewComponents
      - SidebarViewComponents.cs
    - LoginViewsModel.cs 

LoginViewsModel.cs задает схему данных для полей на “account/login” страницы. Экземпляр класса вызывается в AccountController.cs и принимает данные из формы.

SidebarViewComponents.cs. Класс наследуемый от ViewComponent. Внутри вызывает DataManager и метод возвращающий данные из БД и добавляющий их в компонент по адресу “Default”. Сам класс вызывается в представлении Views/Shared/SidebarPartial.cshtml. Плюс компонент Default.cshtml в Views/Shared/Component/Sidebar/.

## Views <a name="main-section-7"></a>
Представление делится на 2 части. Это готовый шаблон bootstrap с готовыми стилями, шрифтами и встроенным скриптом JavaScript. Папка: /wwwroot. Суда же вставляем файлы CK-Editor-а. Вторая часть это основная разметка, папка /Views:
- _VeiwStart.cshtml 
- _ViewImports.cshtml
- /Account
  - Login.cshtml
- /Home
  - Contacts.cshtml
  - Index.cshtml
- /Services
  - Show.cshtml
  - Index.cshtml
- /Shared 
  - _Layout.cshtml
  - CssPartial.cshtml
  - FooterPartial.cshtml
  - HeaderPartial.cshtml
  - MetatagsPartial.cshtml
  - ScriptPartial.cshtml
  - SidebarPartial.cshtml
  - /Components/Sidebar/Default.cshtml

И разметка страниц для администратора из Areas/Admin/Views:
- _VeiwStart.cshtml 
- _ViewImports.cshtml
- /Home
  - Index.cshtml
- /ServiceItems
  - Edit.cshtml
- /TextFields 
  - Edit.cshtml
Общие элементы представления приложения разделены на Partial части и собраны в папке Shared. Эти файлу загружаются до загрузки макета.

Указания на зависимости, другие модули, собраны в файле _ViewImports.cshtml. и доступны для всех внутри папки Views.

Файл Views/Shared/_Layout.cshtml задает макет страницы. В файле запуска рендеринга _VeiwStart.cshtml на него дается ссылка. В макете прописан вызов Partial частей.
