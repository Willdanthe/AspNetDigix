// Vou criar uma variável que vai receber o endereço da aplicação ASP.Net
const API = "http://localhost:5000/Maquina";

// A gente vai aribuir os valores dos campos do formulário para um objeto
// Document é um objeto que representa a página HTML
// GetElementById é um método que retorna um elemento HTML com base no ID
document.getElementById("machineForm").addEventListener("submit", salvarMaquina);
carregarMaquinas(); // Carregar os usuários que é uma função que vamos criar

function carregarMaquinas() {
    // Fetch é uma função que faz uma requisição HTTP
    fetch(API)
        .then(res => res.json()) // res.json() é uma função que converte o coneúdo da resposta para json
        .then(data => {
            const tbody = document.querySelector("#tabelaMaquinas tbody")
            tbody.innerHTML = ""; // é uma propriedade que define ou retorna o conteúdo HTML de um elemento

            data.forEach(maquina => {
                tbody.innerHTML += `
                    <tr>
                        <td>${maquina.id}</td>
                        <td>${maquina.tipo}</td>
                        <td>${maquina.hardDisk}</td>
                        <td>${maquina.memoria_Ram}</td>
                        <td>${maquina.fk_Usuario}</td>
                        <td >
                            <button class="btn btn-secondary" onclick="editarMaquina(${maquina.id})">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
  <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
  <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
</svg>
                            </button>
                            <button class="btn btn-danger" onclick="deletarMaquina(${maquina.id})">
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

function salvarMaquina(event) {
    event.preventDefault(); // Previne o comportamento padrão do formulário (não envia e recarrega a página)
    
    const id = document.getElementById("idMaquina").value; // Obtém o valor do ID
    const maquina = {
        id: parseInt(id || 0), // Se o ID estiver vazio, define 0
        tipo: document.getElementById("tipo").value,
        harddisk: document.getElementById("harddisk").value,
        memoria_ram: document.getElementById("memoria_ram").value,
        fk_usuario: document.getElementById("fk_Usuario").value
    };
    console.table(maquina)
    
    // Verifica se o ID já existe
    fetch(`${API}/${maquina.id}`)
        .then(res => {
            if (res.status === 404) { // Se o usuário não for encontrado, retorna 404
                console.log("Máquina não encontrada. Criando novo...");
                return fetch(API, { // Realiza um POST para criar o usuário
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(maquina)
                });
            } else if (res.ok) { // Se o status for 200, o usuário existe
                console.log("Máquina encontrado. Atualizando...");
                return fetch(`${API}/${maquina.id}`, { // Realiza um PUT para atualizar
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(maquina)
                });
            } else {
                throw new Error("Erro ao verificar o Máquina.");
            }
        })
        .then(res => res.json()) // Converte a resposta para JSON
        .then(() => {
            document.getElementById("machineForm").reset(); // Reseta o formulário
            carregarMaquinas(); // Atualiza a lista de usuários
        })
        .catch(err => console.error("Erro:", err));
}


function editarMaquina(id) {
    fetch(`${API}/${id}`)
        .then(res => res.json())
        .then(maquina => {
            document.getElementById("idMaquina").value = maquina.id;
            document.getElementById("tipo").value = maquina.tipo;
            document.getElementById("harddisk").value = maquina.hardDisk;
            document.getElementById("memoria_ram").value = maquina.memoria_Ram;
            document.getElementById("fk_usuario").value = maquina.fk_Usuario;
        });
}

function deletarMaquina(id) {
    fetch(`${API}/${id}`, { method: "DELETE" })
        .then(() => carregarMaquinas())
        .catch(err => alert("Erro ao deletar máquina: " + err.message));
}