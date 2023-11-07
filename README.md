<h2> Comando para Gerar o Mysql com Docker </h2>

```bash
docker run -d --name mySQL -p 3306:3306 -v mySQL:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=1234 -e MYSQL_PASSWORD=1234 -e MYSQL_ROOT_HOST=% mysql:5.7
```
<h2> Rode os seguintes Comandos .NET </h2>

```bash
dotnet ef migrations add init
```

```bash
dotnet ef database update
```

```bash
dotnet build
```

```bash
dotnet watch run
```

<h2>Para Obter Um Token JWT</h2>
<p>
UserName: guilherme <br> Password: 1234
</p>

<h2>OBS!!!</h2>
<h3>Pacatoes Nuget utilizados</h3>

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

```bash
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.15.0
```