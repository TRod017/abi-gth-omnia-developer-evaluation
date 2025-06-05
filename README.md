# Developer Evaluation Project

## 📑 Sumário

* [Clonando o Projeto](#clonando-o-projeto)
* [Visão Geral](#visao-geral)
* [Premissas assumidas para o módulo de Carrinho (Carts API)](#premissas-assumidas-para-o-modulo-de-carrinho-carts-api)
* [Tecnologias Utilizadas](#tecnologias-utilizadas)
* [Regras de Negócio (Vendas)](#regras-de-negocio-vendas)
* [Executando Localmente (sem Docker)](#executando-localmente-sem-docker)
* [Executando com Docker](#executando-com-docker)
* [Testando a API com Postman (modo local)](#testando-a-api-com-postman-modo-local)
* [Testes Automatizados](#testes-automatizados)
* [Observações Finais](#observacoes-finais)
* [Repositório](#repositorio)

## Clonando o Projeto

Para obter uma cópia do projeto em sua máquina, execute o comando abaixo:

```bash
git clone https://github.com/TRod017/abi-gth-omnia-developer-evaluation.git
cd abi-gth-omnia-developer-evaluation
```

Certifique-se de estar na branch principal (`main`) para acessar a versão final entregue:

```bash
git checkout main
```

## Visão Geral <a id="visao-geral"></a>

Este projeto foi desenvolvido como parte de um teste técnico para avaliação de desenvolvedores back-end, com foco na criação de uma API RESTful robusta, bem estruturada, com arquitetura em camadas e validações de regras de negócio baseadas em domínio.

A proposta consiste na implementação de um sistema de gestão de vendas (Sales), com suporte completo a operações CRUD, cálculo de descontos por quantidade, controle de cancelamento, e relacionamento com produtos e usuários.

Além da implementação técnica, o desafio avalia critérios como:

* Organização de código e estrutura de pastas
* Boas práticas de Clean Code, SOLID e DRY
* Estratégia de versionamento com commits semânticos (Git Flow)
* Adoção correta de padrões como MediatR e validação com FluentValidation
* Proficiência em testes (unitários, integração e funcionais)
* Uso correto das ferramentas fornecidas (Serilog, Bogus, NSubstitute)
* Capacidade de seguir instruções e entregar um projeto funcional com setup simples

O projeto também inclui o módulo de Carrinho de Compras (Carts), adotado como conceito intermediário entre produtos selecionados e vendas finalizadas, conforme premissas assumidas e documentadas abaixo.

<a id="premissas-assumidas-para-o-modulo-de-carrinho-carts-api"></a>
## Premissas assumidas para o módulo de Carrinho (Carts API)

* Cada carrinho pertence a um cliente (Customer).
* O carrinho possui vários itens (CartItems).
* Cada item está vinculado a um produto (Product) existente.
* O carrinho é transacional: representa uma seleção temporária de produtos até ser convertido em uma venda.

## Tecnologias Utilizadas

* .NET 8.0
* Entity Framework Core (PostgreSQL)
* MongoDB (para logging com Serilog)
* MediatR (orquestração entre camadas)
* FluentValidation
* AutoMapper
* Serilog (log estruturado)
* Bogus e NSubstitute (testes)
* xUnit (testes unitários/integrados)
* Docker / Docker Compose (opcional)

## Regras de Negócio (Vendas)<a id="regras-de-negocio-vendas"></a>

* Quantidade < 4: sem desconto
* Quantidade de 4 a 9: 10% de desconto
* Quantidade de 10 a 20: 20% de desconto
* Proibido vender mais de 20 unidades do mesmo item
* O desconto é baseado na quantidade de cada produto na venda

## Executando Localmente (sem Docker)

### Requisitos

É necessário que PostgreSQL e MongoDB estejam instalados e acessíveis localmente.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
  },
  "MongoSettings": {
    "ConnectionString": "mongodb://developer:ev%40luAt10n@localhost:27017",
    "Database": "DeveloperEvaluationLogs"
  }
}
```

O endereço `localhost`, a porta, o nome do banco de dados e as credenciais podem ser ajustados, desde que todas as referências sejam atualizadas corretamente no projeto.

### Aplicando as migrations

```bash
dotnet tool install --global dotnet-ef

$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --project "./src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj" --startup-project "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"
```

### Executando a aplicação

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet run
```

A API estará disponível em:

* Swagger (HTTPS): [https://localhost:7181/swagger/index.html](https://localhost:7181/swagger/index.html)
* Swagger (HTTP): [http://localhost:5119/swagger/index.html](http://localhost:5119/swagger/index.html)

## Executando com Docker

Para executar o projeto via Docker:

1. Certifique-se de estar na raiz do projeto (onde está localizado o arquivo `docker-compose.yml`).
2. No terminal, execute:

```bash
docker-compose up --build
```

Isso irá:

* Subir os containers da API, PostgreSQL e MongoDB
* Aplicar as migrations manualmente conforme instruções abaixo
* Swagger (HTTPS): https://localhost:8081/swagger/index.html
* Swagger (HTTP): http://localhost:8080/swagger/index.html

### Requisitos

É necessário que Docker e Docker Compose estejam instalados e funcionando corretamente no ambiente de execução.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
  },
  "MongoSettings": {
    "ConnectionString": "mongodb://developer:ev%40luAt10n@localhost:27017",
    "Database": "DeveloperEvaluationLogs"
  }
}
```

### Aplicando as migrations

As migrations não são aplicadas automaticamente. Execute o procedimento manualmente conforme abaixo:

```bash
docker exec -it nome-do-container bash
cd /app
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update
```

### Executando a aplicação

```bash
cd caminho/para/o/projeto
docker-compose up --build
```

A API estará disponível em:

* Swagger (HTTP): [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

## Testando a API com Postman (modo local)

O projeto acompanha uma coleção Postman com chamadas organizadas por módulo, permitindo testar todos os endpoints de forma prática e rápida.
...

## 📬 Coleção Postman (Importação)

Baixe e importe o arquivo `.postman_collection.json` no seu Postman:

🔗 [Download da coleção Postman](https://github.com/TRod017/abi-gth-omnia-developer-evaluation/blob/main/API%20Ambev%20Completa%20-%20Produtos%2C%20Usu%C3%A1rios%2C%20Carrinhos%20e%20Vendas.postman_collection.json)

> Basta clicar no link acima, salvar o arquivo `.json` e importar no Postman (Arquivo → Importar).


### Ordem sugerida de testes

1. **Autenticação**
   * POST /api/Auth
2. **Produtos**
   * POST /api/Products (criar 20 exemplos)
3. **Usuários**
   * POST /api/Users
4. **Carrinhos**
   * POST /api/Carts
5. **Vendas**
   * POST /api/Sales
   * GET /api/Sales
   * PATCH /api/Sales/{id}/cancel
   * GET /api/Sales/{id}
   * PUT /api/Sales/{id}
   * DELETE /api/Sales/{id}

---

## Testes Automatizados

Como se trata de um teste técnico, entendemos que o mais importante é demonstrar a capacidade de estruturar e implementar testes de forma correta e aderente às boas práticas. Por isso, implementamos testes completos em alguns módulos e, nos demais, deixamos placeholders e toda a estrutura de testes pronta e organizada — com pastas, classes e padrões definidos — demonstrando domínio técnico e arquitetura adequada para evolução rápida da cobertura. Os testes que ainda não foram implementados estão devidamente sinalizados como pendentes no código.

### Testes Unitários

Todos os testes unitários da camada de domínio foram implementados (Sale, SaleItem, SaleValidator, etc). Na camada de aplicação, os testes foram implementados para o módulo de Cart (Create, Delete, GetAll, Get, Update).

### Testes de Integração

Implementados para:

* ProductControllerIntegrationTests (WebApi)
* Users (Create, Delete, GetAll, Get, Update — Application)

### Testes Funcionais

Implementados para:

* AuthControllerFunctionalTests
* ProductsControllerFunctionalTests

---

<a id="observacoes-finais"></a>
## Observações Finais

O projeto segue os princípios de Clean Code, DDD, SOLID e DRY.

Utiliza commits semânticos e organização baseada em Git Flow.

O README.md cobre toda a instalação, execução e validação do sistema.

A API pode ser validada via Swagger ou Postman.

Para dúvidas ou considerações, entre em contato com o desenvolvedor.

---

<a id="repositorio"></a>
## Repositório

O código-fonte completo deste projeto está disponível em:

🔗 [https://github.com/TRod017/abi-gth-omnia-developer-evaluation](https://github.com/TRod017/abi-gth-omnia-developer-evaluation)
📌 Branch principal: `main`

A branch `main` contém a versão final entregue para avaliação.
