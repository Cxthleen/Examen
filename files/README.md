# WinUI 3 CRUD Template — Studentenniveau

Een volledig werkend startpunt voor een WinUI 3 applicatie met:
- 🔐 Login- en uitlogfunctie
- 📋 CRUD-scherm (aanmaken, lezen, bijwerken, verwijderen)
- 🧩 Overzichtelijke mappenstructuur

---

## 📁 Bestandenstructuur

```
CrudApp/
├── App.xaml               ← Applicatiebrede resources (converters)
├── App.xaml.cs            ← Opstart van de app
├── MainWindow.xaml        ← Hoofdvenster met Frame voor navigatie
├── MainWindow.xaml.cs
│
├── LoginPage.xaml         ← Inlogscherm
├── LoginPage.xaml.cs
│
├── MainPage.xaml          ← CRUD-scherm (lijst + formulier)
├── MainPage.xaml.cs
│
├── SessionManager.cs      ← Bijhouden wie er ingelogd is
├── Product.cs             ← Datamodel (pas aan naar jouw onderwerp)
├── ProductService.cs      ← CRUD-logica (in-memory lijst)
└── PrijsConverter.cs      ← Hulpklasse voor prijsweergave in XAML
```

---

## 🚀 Opstarten

1. Maak een nieuw **WinUI 3 (Blank App, Packaged)** project in Visual Studio 2022.
2. Kopieer alle bestanden naar je project.
3. Zorg dat de `namespace` overal overeenkomt (standaard `CrudApp`, of pas aan).
4. Voer uit — standaard testgebruiker: **admin / admin123**.

---

## 🔑 Inloggen

Testgebruiker (hardcoded voor oefendoeleinden):
- Gebruikersnaam: `admin`
- Wachtwoord:     `admin123`

> ⚠️ In een echte applicatie gebruik je een database en wachtwoord-hashing (bijv. BCrypt)!

---

## 🔄 CRUD uitleggen

| Actie       | Beschrijving                              | Klasse            |
|-------------|-------------------------------------------|-------------------|
| **Create**  | Nieuw product toevoegen via formulier     | `ProductService`  |
| **Read**    | Alle producten tonen in de ListView       | `ProductService`  |
| **Update**  | Selecteer een product → Bewerken → Opslaan| `ProductService`  |
| **Delete**  | Selecteer een product → Verwijderen       | `ProductService`  |

---

## 🛠️ Aanpassen aan jouw onderwerp

1. **`Product.cs`** → Hernoem naar jouw model (bijv. `Klant`, `Artikel`, `Student`)
2. **`ProductService.cs`** → Pas de lijst en methoden aan
3. **`MainPage.xaml`** → Pas de velden in het formulier aan
4. **`MainPage.xaml.cs`** → Pas de lees/schrijf logica aan

Om een echte database te gebruiken, vervang de `List<Product>` in `ProductService` door een SQLite- of SQL Server-connectie.

---

## 📦 NuGet-pakketten nodig

Zorg dat de volgende pakketten geïnstalleerd zijn:
- `Microsoft.WindowsAppSDK`
- `Microsoft.Windows.SDK.BuildTools`

Deze staan standaard in een nieuw WinUI 3 project.
