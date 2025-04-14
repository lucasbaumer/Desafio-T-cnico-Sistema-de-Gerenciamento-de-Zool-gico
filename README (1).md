
# Desafio prático - Sistema de gerenciamento de zoológico 


## Rodando o Projeto

Clone o projeto usando um dos seguintes métodos: 

**por HTTPS**

```bash
  git clone https://github.com/lucasbaumer/Desafio-Tecnico-Sistema-de-Gerenciamento-de-Zoologico.git
```
  **por SSH**
```bash
  git clone git@github.com:lucasbaumer/Desafio-Tecnico-Sistema-de-Gerenciamento-de-Zoologico.git
```

## Rodando o Front-end

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

## Rodando o Back-end

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

## Funcionalidades Implementadas
Gestão de Animais e Cuidados: Telas para listar, cadastrar, atualizar e remover tanto animais quanto cuidados.

Características dos Animais: Cada animal possui informações detalhadas como nome, descrição, data de nascimento, espécie, habitat e país de origem.

Detalhes dos Cuidados: Cada cuidado inclui nome, descrição e frequência de aplicação.

Relacionamento entre Animais e Cuidados: Foi implementado um relacionamento de muitos-para-muitos entre animais e cuidados, permitindo associar múltiplos cuidados a um único animal e vice-versa.

API Restful: O sistema é baseado em uma API REST com backend desenvolvido em C# .NET.

Endpoints da API: O backend fornece endpoints GET, POST, PUT e DELETE para manipulação de animais e cuidados.

## Dificuldades Encontradas
Conversão de Tipos (Date para String): Passei um tempo considerável enfrentando dificuldades com a conversão de tipos de data para string e, apesar de resolver parcialmente, a integração entre front-end e back-end apresentou erros devido a essa questão.

Bugs no Front-end: Enfrentei alguns problemas relacionados à renderização da página, o que gerou inconsistências na exibição dos dados.

Problemas com Inputs na Tela de Edição: Na tela de edição, os campos de data e frequência ficaram em branco após a abertura, mesmo quando os dados estavam sendo passados corretamente.

Integração Front-end e Back-end: A integração entre o front-end e o back-end apresentou desafios, onde algumas funcionalidades funcionavam de forma isolada, mas, quando integradas, nem todas as interações aconteciam como esperado.



## Autores

- [@Lucas Baumer](https://www.github.com/lucasbaumer)




