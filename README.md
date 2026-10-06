\# EcoMonitor – Sistema de Monitoramento de Eficiência Energética



\## 1. Projeto – Cidades ESG Inteligentes



O \*\*EcoMonitor\*\* é uma API desenvolvida para apoiar empresas no monitoramento da eficiência energética.



A solução permite acompanhar informações relacionadas ao consumo de energia, setores, equipamentos, manutenções e alertas, auxiliando na identificação de desperdícios e na tomada de decisões para redução de custos e impactos ambientais.



O projeto está relacionado aos princípios de \*\*ESG\*\*, com foco principalmente na dimensão ambiental, utilizando tecnologia para apoiar o uso mais eficiente dos recursos energéticos.



\---



\## 2. Tecnologias utilizadas



\* C#

\* .NET 8

\* ASP.NET Core Web API

\* Entity Framework Core

\* SQL Server

\* JWT Authentication

\* Swagger / OpenAPI

\* Docker

\* Docker Compose

\* Git

\* GitHub

\* GitHub Actions

\* GitHub Container Registry (GHCR)

\* Render

\* xUnit

\* Entity Framework Core InMemory para testes automatizados



\---



\## 3. Estrutura do projeto



```text

EcoMonitorDevOps/

│

├── Controllers/

├── Data/

├── Middleware/

├── Models/

├── Services/

├── ViewModels/

├── MonitoramentoEnergeticoAPI.Tests/

│

├── .github/

│   └── workflows/

│       └── ci.yml

│

├── Dockerfile

├── docker-compose.yml

├── .dockerignore

├── .env.example

├── .gitignore

├── MonitoramentoEnergetico.csproj

├── Program.cs

└── README.md

```



\---



\## 4. Como executar localmente



\### Pré-requisitos



Para executar o projeto localmente, é necessário ter instalado:



\* .NET 8 SDK

\* Docker Desktop

\* Git



\---



\## 5. Configuração das variáveis de ambiente



O projeto utiliza variáveis de ambiente para configurar a senha do SQL Server.



Na raiz do projeto existe o arquivo:



```text

.env.example

```



Crie uma cópia chamada:



```text

.env

```



Exemplo:



```env

MSSQL\_SA\_PASSWORD=EcoMonitor@123456

```



O arquivo `.env` não deve ser enviado para o GitHub, pois está incluído no `.gitignore`.



\---



\## 6. Executando com Docker Compose



Na raiz do projeto, execute:



```powershell

docker compose up -d

```



O Docker Compose inicializa:



\* API EcoMonitor

\* SQL Server

\* Volume persistente do banco

\* Rede interna entre os containers



Para verificar os containers:



```powershell

docker compose ps

```



A aplicação estará disponível em:



```text

http://localhost:8080

```



O Swagger estará disponível em:



```text

http://localhost:8080/swagger

```



Para visualizar os logs:



```powershell

docker compose logs

```



Para parar os containers:



```powershell

docker compose down

```



\---



\## 7. Arquitetura de containers



O projeto utiliza dois containers principais:



```text

&#x20;             ┌──────────────────────────┐

&#x20;             │       EcoMonitor API               │

&#x20;             │       ASP.NET Core 8               │

&#x20;             │        Porta 8080                  │

&#x20;             └────────────┬─────────────┘

&#x20;                               │

&#x20;                           Rede Docker

&#x20;                               │

&#x20;             ┌────────────▼─────────────┐

&#x20;             │        SQL Server                  │

&#x20;             │          2022                      │

&#x20;             │        Porta 1433                  │

&#x20;             └────────────┬─────────────┘

&#x20;                               │

&#x20;                         Volume persistente

&#x20;                               │

&#x20;                        sqlserver\_data

```



A comunicação entre a API e o banco ocorre pela rede Docker:



```text

ecomonitor-network

```



O SQL Server utiliza o volume:



```text

sqlserver\_data

```



para persistência dos dados.



\---



\## 8. Dockerfile



O projeto possui um Dockerfile utilizando uma estratégia de múltiplos estágios.



O primeiro estágio utiliza o SDK do .NET 8 para restaurar dependências, compilar e publicar a aplicação.



O segundo estágio utiliza a imagem mais leve do ASP.NET Core Runtime para executar a aplicação.



Essa abordagem reduz o conteúdo necessário na imagem final de execução.



Para construir a imagem manualmente:



```powershell

docker build -t ecomonitor-api:dev .

```



Para executar a imagem:



```powershell

docker run -p 8080:80 ecomonitor-api:dev

```



\---



\## 9. Testes automatizados



O projeto possui testes automatizados utilizando:



\* xUnit

\* Microsoft.NET.Test.Sdk

\* WebApplicationFactory

\* Entity Framework Core InMemory



Os testes utilizam um banco de dados em memória para evitar dependência de um SQL Server externo durante a execução da pipeline.



Para executar os testes localmente:



```powershell

dotnet test ".\\MonitoramentoEnergeticoAPI.Tests\\MonitoramentoEnergeticoAPI.Tests.csproj"

```



Resultado esperado:



```text

Aprovado! – Com falha: 0, Aprovado: 5, Ignorado: 0, Total: 5

```



Atualmente são executados \*\*5 testes automatizados\*\*.



\---



\## 10. Pipeline CI/CD



O projeto utiliza \*\*GitHub Actions\*\* para implementar integração e entrega contínuas.



O workflow está localizado em:



```text

.github/workflows/ci.yml

```



A pipeline é executada automaticamente em:



\* Push na branch `master`

\* Pull Request para a branch `master`



\### Etapas da pipeline



```text

GitHub

&#x20;  │

&#x20;  ▼

Checkout do código

&#x20;  │

&#x20;  ▼

Configuração do .NET 8

&#x20;  │

&#x20;  ▼

Restore da API

&#x20;  │

&#x20;  ▼

Restore dos testes

&#x20;  │

&#x20;  ▼

Build da aplicação

&#x20;  │

&#x20;  ▼

Execução dos testes

&#x20;  │

&#x20;  ▼

Build da imagem Docker

&#x20;  │

&#x20;  ▼

Publicação no GHCR

&#x20;  │

&#x20;  ▼

Deploy automático Staging

&#x20;  │

&#x20;  ▼

Deploy automático Production

```



\---



\## 11. GitHub Container Registry



A imagem Docker da aplicação é publicada automaticamente no \*\*GitHub Container Registry (GHCR)\*\*.



Imagem:



```text

ghcr.io/largato123/ecomonitordevops:master

```



Para baixar a imagem:



```powershell

docker pull ghcr.io/largato123/ecomonitordevops:master

```



\---



\## 12. Ambientes de Staging e Production



A aplicação possui dois ambientes publicados utilizando o Render.



\### Staging



```text

https://ecomonitor-staging.onrender.com/swagger

```



O ambiente de staging é utilizado para validar a versão publicada antes da utilização em produção.



\### Production



```text

https://ecomonitor-production.onrender.com/swagger

```



O ambiente de produção disponibiliza a versão publicada da aplicação.



Os dois ambientes utilizam a imagem Docker publicada no GitHub Container Registry.



\---



\## 13. Deploy automático



O deploy dos ambientes é realizado automaticamente pela pipeline do GitHub Actions.



Após:



1\. Build da aplicação;

2\. Execução dos testes;

3\. Construção da imagem Docker;

4\. Publicação da imagem no GHCR;



a pipeline aciona os Deploy Hooks configurados no Render.



São utilizados os seguintes secrets no GitHub:



```text

RENDER\_STAGING\_DEPLOY\_HOOK

RENDER\_PRODUCTION\_DEPLOY\_HOOK

```



Os valores desses secrets não são armazenados no código-fonte.



\---



\## 14. Banco de dados



A aplicação utiliza \*\*SQL Server 2022\*\*.



O banco utilizado pela aplicação é:



```text

MonitoramentoEnergeticoDB

```



As principais tabelas são:



```text

Setores

Equipamentos

ConsumosEnergia

Manutencoes

AlertasEnergia

\_\_EFMigrationsHistory

```



O banco é criado e atualizado utilizando Entity Framework Core Migrations.



\---



\## 15. Autenticação



A API utiliza autenticação baseada em \*\*JWT (JSON Web Token)\*\*.



O endpoint de login está disponível em:



```text

POST /api/Login

```



Após realizar o login, o token JWT pode ser utilizado para acessar endpoints protegidos da API.



No Swagger, utilize o botão:



```text

Authorize

```



e informe:



```text

Bearer SEU\_TOKEN

```



\---



\## 16. Swagger



O Swagger permite visualizar e testar os endpoints da API.



\### Local



```text

http://localhost:8080/swagger

```



\### Staging



```text

https://ecomonitor-staging.onrender.com/swagger

```



\### Production



```text

https://ecomonitor-production.onrender.com/swagger

```



\---



\## 17. Principais endpoints



A API possui controllers relacionados a:



```text

/api/Login

/api/Setor

/api/Equipamento

/api/ConsumoEnergia

/api/AlertaEnergia

```



Os endpoints podem ser consultados e testados através do Swagger.



\---



\## 18. DevOps aplicado ao projeto



O projeto aplica práticas de DevOps para automatizar o ciclo de desenvolvimento e entrega da aplicação.



Entre as práticas utilizadas estão:



\* Controle de versão com Git;

\* Repositório remoto no GitHub;

\* Integração contínua com GitHub Actions;

\* Build automatizado;

\* Testes automatizados;

\* Containerização com Docker;

\* Orquestração com Docker Compose;

\* Publicação de imagens no GHCR;

\* Deploy automatizado;

\* Ambientes de staging e produção;

\* Variáveis de ambiente;

\* Secrets para informações sensíveis;

\* Persistência do banco através de volume Docker.



\---



\## 19. Evidências da execução



A entrega deve conter evidências da execução da solução, incluindo:



\* Pipeline GitHub Actions com execução bem-sucedida;

\* Build da aplicação;

\* Testes automatizados aprovados;

\* Imagem Docker publicada no GHCR;

\* Containers executando pelo Docker Compose;

\* Swagger local;

\* Swagger do ambiente de staging;

\* Swagger do ambiente de produção;

\* Deploys realizados no Render.



\---



\## 20. Desafios e soluções



\### Autenticação durante os testes



Os endpoints da API possuem proteção JWT, o que inicialmente causava respostas HTTP 401 durante os testes automatizados.



Foi implementado um mecanismo de autenticação específico para os testes utilizando um `TestAuthenticationHandler`.



\### Dependência do SQL Server nos testes



A pipeline do GitHub Actions não possuía inicialmente um SQL Server disponível para os testes.



Para solucionar o problema, os testes passaram a utilizar o \*\*Entity Framework Core InMemory\*\*, permitindo executar os testes de forma isolada na pipeline.



\### Disponibilização do Swagger em produção



Inicialmente o Swagger estava condicionado ao ambiente de desenvolvimento.



A configuração foi ajustada para permitir a disponibilização do Swagger também nos ambientes de staging e produção, facilitando a validação da aplicação publicada.



\---



\## 21. Resultado final



O EcoMonitor possui atualmente um fluxo automatizado de desenvolvimento e entrega:



```text

Código

&#x20; ↓

GitHub

&#x20; ↓

GitHub Actions

&#x20; ↓

Build

&#x20; ↓

Testes automatizados

&#x20; ↓

Docker

&#x20; ↓

GitHub Container Registry

&#x20; ↓

Staging

&#x20; ↓

Production

```



Dessa forma, o projeto atende aos principais requisitos de CI/CD, containerização, automação de testes, publicação de imagem e deploy em ambientes de staging e produção.



\---



\## 22. Autoria



\*\*Projeto:\*\* EcoMonitor – Sistema de Monitoramento de Eficiência Energética



\*\*Área:\*\* Análise e Desenvolvimento de Sistemas



\*\*Tema:\*\* Eficiência Energética e Sustentabilidade (ESG)



\*\*Desenvolvimento:\*\* Diego Romanholi



