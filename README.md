
# 🐾 Desafio prático - Sistema de gerenciamento de zoológico 

## 📌 DESCRIÇÃO DO PROJETO
Este projeto é uma aplicação web desenvolvida com Angular e .NET Core seguindo a arquitetura Onion, com foco na gestão de Cuidados com Animais. O sistema permite que usuários realizem o cadastro, edição, listagem e exclusão de cuidados, vinculando esses cuidados a um ou mais animais cadastrados.

A interface do usuário foi construída com Angular (componentes standalone), utilizando validações visuais nos formulários, mensagens de feedback amigáveis (alertas de erro e sucesso) e uma experiência fluida para o usuário. O backend em .NET Core expõe uma API RESTful responsável por manipular os dados de forma segura, seguindo boas práticas de separação de responsabilidades com camadas bem definidas:

Core: Entidades e interfaces principais.

Application: DTOs, Interfaces de serviço e regras de negócio.

Infrastructure: Repositórios, Migrations e acesso ao banco de dados.

Presentation: Controllers da API.

## ✅ Funcionalidades Implementadas:
Gestão de Animais e Cuidados: Telas para listar, cadastrar, atualizar e remover tanto animais quanto cuidados.

Características dos Animais: Cada animal possui informações detalhadas como nome, descrição, data de nascimento, espécie, habitat e país de origem.

Detalhes dos Cuidados: Cada cuidado inclui nome, descrição e frequência de aplicação.

Relacionamento entre Animais e Cuidados: Foi implementado um relacionamento de muitos-para-muitos entre animais e cuidados, permitindo associar múltiplos cuidados a um único animal e vice-versa.

API Restful: O sistema é baseado em uma API REST com backend desenvolvido em C# .NET.

Endpoints da API: O backend fornece endpoints GET, POST, PUT e DELETE para manipulação de animais e cuidados.

## 🛠️ Tecnologias utilizadas

- Frontend: Angular com componentes standalone + CSS puro

- Backend: .NET Core 7 com arquitetura Onion

- Banco de Dados: SQL Server

- Ferramentas: Visual Studio Code, Visual Studio 2022, SQL Server Management Studio

## 🚀 Rodando o Projeto

## 🔁 Clone o repositório: 

**via HTTPS**

```bash
  git clone https://github.com/lucasbaumer/Desafio-Tecnico-Sistema-de-Gerenciamento-de-Zoologico.git
```
  **via SSH**
```bash
  git clone git@github.com:lucasbaumer/Desafio-Tecnico-Sistema-de-Gerenciamento-de-Zoologico.git
```

## ▶️ Rodando o Front-end

Acesse a pasta onde contém o código front-end (Angular) 

```bash
  cd animalCareApp
```

Inicie o servidor

```bash
  ng serve
```

Após iniciar o servidor, acesse o projeto web pela URL que aparecerá no terminal, geralmente:
```bash
  http://localhost:4200/  # Ou a porta que foi aberta
```

## 💻 Rodando o Back-end

Entre na pasta onde contém o código back-end (C# .NET)  
```bash
  cd backend
```
entre na pasta da solução presentation

```bash
  cd AnimalCareBackend.Presentation
```

Inicie o servidor .NET

```bash
 dotnet run
```

## ⚙️ Configurações de Conexão
Para mudar a porta do banco de dados SQL, edite o arquivo appsettings.json, localizado dentro do projeto do back-end. Substitua PORTA pela porta do servidor local:

```bash
"ConnectionStrings": {
  "DefaultConnection": "server=PORTA; database=AnimalCare; trusted_connection=true; trustservercertificate=true"
}
```

Agora, dentro da pasta do front-end, altere a URL do back-end no arquivo environment.ts para a URL correta da sua API (também substituindo PORTA pela porta do servidor local):

📌 **Nota:** Substitua `PORTA` pela porta que aparece no terminal ao rodar a aplicação.
```bash
 export const environment = {
  production: false,
  apiUrl: 'http://localhost:PORTA/api'
};
```

## 👤 Autor

- [@Lucas Baumer](https://www.github.com/lucasbaumer)




