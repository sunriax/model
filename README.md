[![Version: 1.0 Release](https://img.shields.io/badge/Version-1.0%20Release-green.svg)](https://github.com/sunriax/model) [![Version](https://travis-ci.com/sunriax/model.svg?branch=main)](https://travis-ci.com/github/sunriax/model) [![codecov](https://codecov.io/gh/sunriax/model/branch/main/graph/badge.svg?token=jiJ7i87UQe)](https://codecov.io/gh/sunriax/model) [![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

# Data models

## Description

Simple data models that are often used in development and teaching.

## PersonLib
---

### Manual installation

Download library [[zip](https://github.com/sunriax/model/releases/latest/download/Model.zip) | [tar.gz](https://github.com/sunriax/model/releases/latest/download/Model.tar.gz)] and unpack it to your project. Setup a reference and start [using](#Using-Person) it in your project.

### NuGet installation
[![NuGet](https://img.shields.io/nuget/dt/ragae.model.person.svg)](https://www.nuget.org/packages/ragae.model.person) 

``` bash
PM> Install-Package RaGae.Model.Person
```

### Using Person

``` csharp
Person p = new();

Person p = new("Max", "Mustermann");

Person p = new()
{
    FirstName = "Max",
    LastName = "Mustermann"
};

p.FirstName;    // Max
p.LastName;     // Mustermann
p.ToString();   // Max Mustermann

Person c = (Person)p.Clone();
```

---

## AddressLib
---

### Manual installation

Download library [[zip](https://github.com/sunriax/model/releases/latest/download/Model.zip) | [tar.gz](https://github.com/sunriax/model/releases/latest/download/Model.tar.gz)] and unpack it to your project. Setup a reference and start [using](#Using-Address) it in your project.

### NuGet installation
[![NuGet](https://img.shields.io/nuget/dt/ragae.model.address.svg)](https://www.nuget.org/packages/ragae.model.address) 

``` bash
PM> Install-Package RaGae.Model.Address
```

### Using Address

``` csharp
Address a = new();

Address a = new("Musterstrasse", "1")
{
    City = new(1100, "Wien"),
    Country = new("AT", "Austria")  
};

Address a = new()
{
    Street = "Musterstrasse",
    Number = "1",
    City = new()
    {
        Zip = 1100,
        Name = "Wien"
    },
    Country = new()
    {
        Code = "AT",
        Name = "Austria"
    }
};

a.Street;               // Musterstrasse
a.Number;               // 1
a.ToString();           // Musterstrasse 1

a.City.Zip              // 1100
a.City.Name             // Wien
a.City.ToString();      // 1100 Wien

a.Country.Code          // AT
a.Country.Name          // Austria
a.Country.ToString();   // AT-Wien

Address c = (Address)a.Clone();
```

### Using City

``` csharp
City c = new();

City c = new(1100, "Wien");

City c = new()
{
    Zip = 1100,
    Name = "Wien"
};

c.Zip;          // 1100
c.Name;         // Wien
c.ToString();   // 1100 Wien

City y = (City)c.Clone();
```

### Using Country

``` csharp
Country c = new();

Country c = new("AT", "Austria");

Country c = new()
{
    Code = "AT",
    Name = "Austria"
};

c.Code;         // AT
c.Name;         // Austria
c.ToString();   // AT-Austria

Country y = (Country)c.Clone();
```

---
R. GÄCHTER