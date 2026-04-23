<h1 align="center">🏢 API de Gerenciamento de Salas de Reunião</h1>

Esta API foi desenvolvida em ASP.NET Core para facilitar o gerenciamento, reserva e consulta de salas de reunião. 
Através dela, é possível realizar operações completas de CRUD (Create, Read, Update, Delete) integradas a um serviço de lógica de negócios.

<h2 align="center">🚀 Instruções para Execução</h2>

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) (Versão 8.0).
* IDE: Visual Studio ou Jet Brains Rider.

### Passo a Passo
1.  **Clonar o repositório:**
    ```bash
    git clone https://github.com/seu-usuario/nome-do-repositorio.git
    cd nome-do-repositorio
    ```
2.  **Restaurar dependências:**
    ```bash
    dotnet restore
    ```
3.  **Executar a aplicação:**
    ```bash
    dotnet run --project ApiReuniao
    ```
4.  **Acessar a documentação (Swagger):**
    Com a aplicação rodando, acesse no navegador:
    `http://localhost:PORTA/swagger` (Substitua PORTA pela porta configurada, geralmente 5000 ou 5001).

---
<h2 align="center">🛠️ Endpoints da API</h2>

A base URL padrão da API é: `/api/sala`

### 1. Listar Todas as Salas (GET)
Retorna uma lista com todas as salas cadastradas.
* **URL:** `GET /api/sala`
* **Sucesso (200):** Lista de objetos de salas.
* **Erro (500):** Erro interno no servidor.

### 2. Obter Sala por ID (GET By ID)
Busca os detalhes de uma sala específica.
* **URL:** `GET /api/sala/{id}`
* **Parâmetro:** `id` (int) - Deve ser maior que zero.
* **Sucesso (200):** Objeto da sala encontrada.
* **Erros:** * `400 Bad Request`: ID inválido.
    * `404 Not Found`: Sala não localizada.

### 3. Criar Nova Sala (POST)
Cadastra uma nova sala no sistema.
* **URL:** `POST /api/sala`
* **Corpo da Requisição (JSON):**
    ```json
    {
      "nome": "Sala Executiva A",
      "capacidade": 12,
      "precoHora": 150.00,
      "possuiProjetor": "Sim",
      "status": "Disponível"
    }
    ```
* **Regras:** Todos os campos são obrigatórios. Capacidade e preço devem ser maiores que zero.

### 4. Atualizar Sala (PUT)
Atualiza os dados de uma sala existente.
* **URL:** `PUT /api/sala/{id}`
* **Regra Importante:** O **ID na URL** deve ser idêntico ao **ID no corpo do JSON**. Caso contrário, a API retornará erro 400.
* **Sucesso (200):** Confirmação de atualização.

### 5. Excluir Sala (DELETE)
Remove uma sala do sistema através do ID.
* **URL:** `DELETE /api/sala/{id}`
* **Sucesso (200):** Mensagem de confirmação de exclusão.
* **Erro (400):** ID inválido (menor ou igual a zero).

---
<h2 align="center">📋 Modelo de Dados (Sala)</h2>

| Atributo        | Tipo    | Descrição                                    |
| :-------------- | :------ | :------------------------------------------- |
| `Id`            | int     | Identificador único da sala                  |
| `Nome`          | string  | Nome descritivo da sala                      |
| `Capacidade`    | int     | Quantidade máxima de pessoas                 |
| `PrecoHora`     | decimal | Valor cobrado por hora de uso                |
| `PossuiProjetor`| string  | Indica se há projetor (Ex: "Sim"/"Não")      |
| `Status`        | string  | Estado atual (Ex: "Disponível", "Reservada") |

---
<h2 align="center">⚠️ Tratamento de Erros</h2>

A API utiliza códigos de status HTTP padronizados:
* **200/201**: Sucesso.
* **400**: Erros de validação (IDs negativos, campos vazios, IDs divergentes no PUT).
* **404**: Recurso não encontrado no banco de dados.
* **500**: Erros inesperados processados pelo bloco `try-catch`.

---

> **Nota:** Esta API utiliza injeção de dependência para o `SalaService`, garantindo que a lógica de persistência de dados esteja desacoplada da controller.
