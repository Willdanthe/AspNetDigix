// Vou criar uma variável que vai receber o endereço da aplicação ASP.Net
const API = "http://localhost:5000/Usuario";

// A gente vai aribuir os valores dos campos do formulário para um objeto
// Document é um objeto que representa a página HTML
// GetElementById é um método que retorna um elemento HTML com base no ID
document.getElementById("usuarioForm").addEventListener("submit", salvarUsuario);
carregarUsuarios(); // Carregar os usuários que é uma função que vamos criar

function carregarUsuarios() {
    // Fetch é uma função que faz uma requisição HTTP
    fetch(API)
        .then(res => res.json()) // res.json() é uma função que converte o coneúdo da resposta para json
        .then(data => {
            const tbody = document.querySelector("#tabelaUsuarios tbody")
            tbody.innerHTML = ""; // é uma propriedade que define ou retorna o conteúdo HTML de um elemento

            data.forEach(usuario => {
                tbody.innerHTML += `
                    <tr>
                        <td>${usuario.id}</td>
                        <td>${usuario.nome}</td>
                        <td>${usuario.password}</td>
                        <td>${usuario.ramal}</td>
                        <td>${usuario.especialidade}</td>
                        <td >
                        
                            <button class="btn btn-secondary" onclick="editarUsuario(${usuario.id})">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
  <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
  <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
</svg>
                            </button>
                            <button class="btn btn-danger" onclick="deletarUsuario(${usuario.id})">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
  <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"/>
  <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"/>
</svg>
                            </button>
                        </td>
                    </tr>
                    `;
            }
            )
        }
        )
}

function salvarUsuario(event) {
    event.preventDefault(); // Previne o comportamento padrão do formulário (não envia e recarrega a página)

    const id = document.getElementById("idUsuario").value; // Obtém o valor do ID
    const usuario = {
        id: parseInt(id || 0), // Se o ID estiver vazio, define 0
        nome: document.getElementById("nome").value,
        password: document.getElementById("password").value,
        ramal: document.getElementById("ramal").value,
        especialidade: document.getElementById("especialidade").value
    };
    console.table(usuario)

    // Verifica se o ID já existe
    fetch(`${API}/${usuario.id}`)
        .then(res => {
            if (res.status === 404) { // Se o usuário não for encontrado, retorna 404
                console.log("Usuário não encontrado. Criando novo...");
                return fetch(API, { // Realiza um POST para criar o usuário
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(usuario)
                });
            } else if (res.ok) { // Se o status for 200, o usuário existe
                console.log("Usuário encontrado. Atualizando...");
                return fetch(`${API}/${usuario.id}`, { // Realiza um PUT para atualizar
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(usuario)
                });
            } else {
                throw new Error("Erro ao verificar o usuário.");
            }
        })
        .then(res => res.json()) // Converte a resposta para JSON
        .then(() => {
            document.getElementById("usuarioForm").reset(); // Reseta o formulário
            carregarUsuarios(); // Atualiza a lista de usuários
        })
        .catch(err => console.error("Erro:", err));
}


function editarUsuario(id) {
    fetch(`${API}/${id}`)
        .then(res => res.json())
        .then(usuario => {
            document.getElementById("idUsuario").value = usuario.id;
            document.getElementById("nome").value = usuario.nome;
            document.getElementById("password").value = usuario.password;
            document.getElementById("ramal").value = usuario.ramal;
            document.getElementById("especialidade").value = usuario.especialidade;
        });
}

function deletarUsuario(id) {
    fetch(`${API}/${id}`, { method: "DELETE" })
        .then(() => carregarUsuarios())
        .catch(err => alert("Erro ao deletar usuário: " + err.message));
}