# BookStoreAPI

## Descrição do Projeto

O **BookStoreAPI** é uma API desenvolvida em .NET 8 que fornece funcionalidades para gerenciar uma livraria. A API permite a criação, leitura, atualização e exclusão de livros, autores, categorias, usuários e compras. Além disso, a API implementa autenticação JWT para proteger os endpoints e um middleware global para tratamento de exceções.

## Como Rodar o Projeto

### Pré-requisitos

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 (ou outro IDE de sua preferência)

### Configuração do Banco de Dados e token

1. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`:
2. Configure a string de SecretKey para o JWT`appsettings.json`:


```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
  },
  "JwtSettings": {
    "Issuer": "your_issuer",
    "Audience": "your_audience",
    "SecretKey": "your_secret_key"
  }
}

```

### Executando a Aplicação

1. Clone o repositório:


```shell
git clone https://github.com/seu-usuario/BookStoreAPI.git

```

2. Navegue até o diretório do projeto:


```shell
cd BookStoreAPI

```

3. Restaure as dependências do projeto:


```shell
dotnet restore

```

4. Execute as migrações para criar o banco de dados:


```shell
dotnet ef database update

```

5. Execute a aplicação:


```shell
dotnet run

```

A API estará disponível em `https://localhost:7189`.

### Documentação da API

A documentação da API pode ser acessada através do Swagger em `https://localhost:7189/swagger/index.html`.

## Tecnologias Usadas

- **.NET 8**
- **Entity Framework Core**
- **AutoMapper**
- **JWT Authentication**
- **Swagger**
- **SqlServer**

## Estrutura do Projeto

- **Controllers**: Contém os controladores da API.
- **Data**: Contém o contexto do banco de dados e as configurações do Entity Framework.
- **Interface**: Contém as interfaces dos serviços.
- **Middlewares**: Contém os middlewares personalizados.
- **Models**: Contém as entidades e DTOs.
- **Services**: Contém a implementação dos serviços.
- **Tools**: Contém classes utilitárias e exceções personalizadas.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

---

Este README fornece uma visão geral do projeto, instruções sobre como configurá-lo e executá-lo, e uma lista das tecnologias usadas. Sinta-se à vontade para personalizá-lo conforme necessário.
