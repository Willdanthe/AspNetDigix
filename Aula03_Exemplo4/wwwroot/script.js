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
                        <td>${usuario.ramal}</td>
                        <td>${usuario.especialidade}</td>
                        <td >
                            <button class="btn btn-secondary" onclick="editarUsuario(${usuario.id})">Editar</button>
                            <button class="btn btn-danger" onclick="deletarUsuario(${usuario.id})">Deletar</button>
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