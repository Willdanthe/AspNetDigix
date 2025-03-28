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
                            <button class="btn btn-secondary" onclick="editarMaquina(${maquina.id})">Editar</button>
                            <button class="btn btn-danger" onclick="deletarMaquina(${maquina.id})">Deletar</button>
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
        fk_usuario: document.getElementById("fk_usuario").value
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