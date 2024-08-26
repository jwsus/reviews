# Course Review API

## Descrição

Este projeto é uma API para gerenciar Cursos, Estudantes e Avaliações. Os estudantes podem avaliar os cursos com estrelas de 1 a 5 e, opcionalmente, deixar um comentário.

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- PostgreSQL
- Swagger

## Pré-requisitos

Antes de começar, você precisará ter o seguinte instalado em sua máquina:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

## Configuração do Banco de Dados

1. **Criar o Banco de Dados**:

   Primeiro, crie um banco de dados no PostgreSQL. Use a ferramenta de sua preferência, como `pgAdmin` ou o `psql`.

   ```sql
   CREATE DATABASE coursereviewdb; ```
   
Configurar a String de Conexão:

No arquivo appsettings.json, substitua DefaultConnection pela sua string de conexão:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=coursereviewdb;Username=your_username;Password=your_password"
  }
} ```
Certifique-se de substituir your_username e your_password pelas credenciais do seu PostgreSQL.

## Aplicar as Migrações:

No terminal, execute os seguintes comandos para aplicar as migrações e criar as tabelas necessárias no banco de dados:

bash
Copiar código
dotnet ef database update
Executando o Projeto
Para executar o projeto, utilize o seguinte comando no terminal:

bash
Copiar código
dotnet run
Acessando o Swagger
## Após iniciar a API, você pode acessar a documentação interativa do Swagger no seguinte endereço:

URL: http://localhost:5000/swagger
Caso você tenha alterado a porta padrão no launchSettings.json, ajuste a URL de acordo.
