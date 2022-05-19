# Modelo DDD para .NET Core 6

### INDICE
 - [01 -Domain](#01--domain) 
 - [02 -Infrastructure](#02--infrastructure)
 - [03 -application](#03--application)
 - [04 -Web Api](#04--web-api)
	-  [Adicionar um novo controller](#adicionar-um-novo-controller)
 - [Alterar-Banco](#alterar-banco)
 - [Configurações](#configurações)
 

## 01 -Domain
![enter image description here](https://github.com/rkinob/DotNetCoreModelo/blob/master/01-Domain.GIF)

Refere-se ao domínio da aplicação e possui dois projetos:
Prodesp.Domain.Shared: Possui uma pasta Entities, aonde é configurada o Agregado/Entidade do domínio, bem como as regras do mesmo.
Prodesp.Domain: Por padrão possui as pastas Exception (no projeto padrão trata-se apenas a exceção de autenticação referente ao Token), Repositories e Services .
#### Repositories -> Interfaces
Aonde serão definidas as interfaces dos repositórios específicos. 
#### Services -> Interfaces
Aonde serão definidas as interfaces dos serviços (casos de uso) das Entidades e/ou Agregados.
#### Services -> Implementations
Aonde serão definidas as implementações  dos serviços (casos de uso) das Entidades e/ou Agregados.



## 02 -Infrastructure
![enter image description here](https://github.com/rkinob/DotNetCoreModelo/blob/master/02-Infrastructure.GIF)

Refere-se a infraestrutura da aplicação aonde são definidas as injeções de dependência, serviços externos, mapeamento dos objetos para o bando de dados.
 #### Configurations
 Todos os arquivos de mapeamento das Entidades com o banco de dados.
 #### Repositories
  Classes concretas dos Repositórios que estão no domínio.
 #### Unit Of Work
   Implementação do padrão Unit of Work - 
   
   No arquivo BaseUnitOfWork no get da Propriedade Contexto, caso precise mudar o banco de dados, deve-se alterar os parâmetros recebidos pelo DbContext.
   
   Atualmente está configurado para SQL Server => optionsBuilder.UseSqlServer(this.ConnectionStringName);
  
  Contexto.cs - Caso esteja usand EntityFramework, deve-se definir o mapeamento dos DBSets e Entities nesse arquivo.
  DependencyInjection.cs - As injeções de dependências devem ser informadas nesse arquivo. 


## 03 -Application
![enter image description here](https://github.com/rkinob/DotNetCoreModelo/blob/master/03-Application.GIF)

Camada responsável por fazer a mediação entre o domínio da aplicação e o WebAPI.
#### AppServices:
Todas as classes que serão chamadas pelas camadas mais externas. Exemplo: Recuperar GVE por Id, Lista de GVEs etc.
#### CrossCutting. TO
Request - Estrutura dos dados recebidos pela WebAPI.
Response - Estrutura dos dados enviados para a WebAPI.
#### Helper
Classes para tratar as estruturas definidas em CrossCutting.TO para enviar as estruturas definidas na Request para o domínio e tratar os dados do domínio para transformar nas estruturas definidas na pasta Response. 
#### Validation
Estruturas com os dados enviados/recebidos da WebAPI junto com as validações.

## 04 -Web Api
Ficarão todos os projetos de cada domínio do sistema.
Cada Controller poderá referenciar as classes que estão em AppServices e utilizar as funções da Helper para transformar os dados enviados ou recebidos pela aplicação externa.
Abaixo um exemplo:
![enter image description here](https://github.com/rkinob/DotNetCoreModelo/blob/master/04-WebApi.GIF )

Remover nas configurações a linha
<Nullable>enable</Nullable>
Pois no .net 6 já vem por padrão configurado, exigindo a obrigatoriedade do ? nos objetos

![image](https://user-images.githubusercontent.com/62960151/159513123-e524c9a5-7dfa-49a1-8402-f7d25e8a6f3e.png)

Caso deixe este comando, obrigatoriamente precisa adicionar a ? mesmo em string que podem ter a possibilidade de ser NULL exemplo:
![image](https://user-images.githubusercontent.com/62960151/159514292-18f923b2-76cf-45c7-a605-d327d58f5a06.png)

### Adicionar um novo controller
- Adicionar dependências:
	-  Prodesp.Core.BackEnd.Application
	-  Prodesp.Core.BackEnd.Domain
	-  Prodesp.GsNet.Core.To
	
- Referenciar os projetos abaixo:
	 -  Prodesp.Application
	 - Prodesp.Infra.EF (caso seja utilizado a UnitOfWork)
	 - WebApi.Intercept (para fazer a autenticação dos controllers via atributo Authorize)
Segue imagem de como ficará as dependências do projeto:

 ![enter image description here](https://github.com/rkinob/DotNetCoreModelo/blob/master/05-WebApi.GIF )


## Alterar Banco
- Alterar arquivos
	-  Prodesp.Infra.EF/Configurations/ConfigurationHelper.cs
	 ![image](https://user-images.githubusercontent.com/62960151/158440547-aaa9cd84-9d0b-4c52-b24d-a13a248688e9.png)
	-  Prodesp.Infra.EF/UnitOfWork/UnitOfWork.cs
	![image](https://user-images.githubusercontent.com/62960151/158441139-f83dea27-0966-49f0-8d6a-6267ddedfe6e.png)
	O arquivo abaixo , não precisa alterar é apenas para ilustrar o que vai olhar alterando acima.![image](https://user-images.githubusercontent.com/62960151/158441330-9998060a-82d0-45ad-859b-9f0edd7951e8.png)
	-  WebApi/SeuProjeto/ShareSettings.json
	![image](https://user-images.githubusercontent.com/62960151/158441607-b580c108-6351-47af-a633-68e7c026ba0a.png)
	
## Configurações
No arquivo Shared/SharedSettings.json será informada a(s) Connection(s) String que serão utilizados pelos projetos WebAPI.
No AppSettings, deve-se informar as demais configurações que serão utilizadas, por padrão estão sendo utilizadas Secret para informar a chave secreta da autenticação JWT e a JWT.EXPIRATION.HOURS para informar por quanto tempo em horas expira o token.
Caso necessite de mais configurações acrescentar na seção AppSettings e alterar
