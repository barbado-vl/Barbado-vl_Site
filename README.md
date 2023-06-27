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


## Структура и архитектура проекта <a name="main-section-2"></a>


## Точка входа и настройка приложения <a name="main-section-3"></a>


## «Domain» <a name="main-section-4"></a>


## Контроллер <a name="main-section-5"></a>


## Models и Sidebar <a name="main-section-6"></a>


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
