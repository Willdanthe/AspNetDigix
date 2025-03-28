// Vou criar uma variável que vai receber o endereço da aplicação ASP.Net
const API = "http://localhost:5000/Softwares";

// A gente vai aribuir os valores dos campos do formulário para um objeto
// Document é um objeto que representa a página HTML
// GetElementById é um método que retorna um elemento HTML com base no ID
document.getElementById("softwareForm").addEventListener("submit", salvarSoftware);
carregarSoftwares(); // Carregar os usuários que é uma função que vamos criar

function carregarSoftwares() {
    // Fetch é uma função que faz uma requisição HTTP
    fetch(API)
        .then(res => res.json()) // res.json() é uma função que converte o coneúdo da resposta para json
        .then(data => {
            const tbody = document.querySelector("#tabelaSoftwares tbody")
            tbody.innerHTML = ""; // é uma propriedade que define ou retorna o conteúdo HTML de um elemento

            data.forEach(software => {
                tbody.innerHTML += `
                    <tr>
                        <td>${software.id}</td>
                        <td>${software.produto}</td>
                        <td>${software.hardDisk}</td>
                        <td>${software.memoria_Ram}</td>
                        <td>${software.fk_Maquina}</td>
                        <td >
                            <button class="btn btn-secondary" onclick="editarSoftware(${software.id})">Editar</button>
                            <button class="btn btn-danger" onclick="deletarSoftware(${software.id})">Deletar</button>
                        </td>
                    </tr>
                    `;
            }
            )
        }
        )
}

function salvarSoftware(event) {
    event.preventDefault(); // Previne o comportamento padrão do formulário (não envia e recarrega a página)
    
    const id = document.getElementById("idSoftware").value; // Obtém o valor do ID
    const software = {
        id: parseInt(id || 0), // Se o ID estiver vazio, define 0
        produto: document.getElementById("produto").value,
        harddisk: document.getElementById("harddisk").value,
        memoria_ram: document.getElementById("memoria_ram").value,
        fk_Maquina: document.getElementById("fk_Maquina").value
    };
    console.table(software)
    
    // Verifica se o ID já existe
    fetch(`${API}/${software.id}`)
        .then(res => {
            if (res.status === 404) { // Se o usuário não for encontrado, retorna 404
                console.log("Máquina não encontrada. Criando novo...");
                return fetch(API, { // Realiza um POST para criar o usuário
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(software)
                });
            } else if (res.ok) { // Se o status for 200, o usuário existe
                console.log("Máquina encontrado. Atualizando...");
                return fetch(`${API}/${software.id}`, { // Realiza um PUT para atualizar
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(software)
                });
            } else {
                throw new Error("Erro ao verificar o Software.");
            }
        })
        .then(res => res.json()) // Converte a resposta para JSON
        .then(() => {
            document.getElementById("machineForm").reset(); // Reseta o formulário
            carregarSoftwares(); // Atualiza a lista de usuários
        })
        .catch(err => console.error("Erro:", err));
}


function editarSoftware(id) {
    fetch(`${API}/${id}`)
        .then(res => res.json())
        .then(software => {
            document.getElementById("idMaquina").value = software.id;
            document.getElementById("produto").value = software.produto;
            document.getElementById("harddisk").value = software.hardDisk;
            document.getElementById("memoria_ram").value = software.memoria_Ram;
            document.getElementById("fk_Maquina").value = software.fk_Maquina;
        });
}

function deletarSoftware(id) {
    fetch(`${API}/${id}`, { method: "DELETE" })
        .then(() => carregarSoftwares())
        .catch(err => alert("Erro ao deletar máquina: " + err.message));
}