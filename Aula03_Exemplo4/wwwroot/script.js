// Vou criar uma variável que vai receber o endereço da aplicação ASP.Net
const API = "http://localhost:5000/Usuario";

// A gente vai aribuir os valores dos campos do formulário para um objeto
// Document é um objeto que representa a página HTML
// GetElementById é um método que retorna um elemento HTML com base no ID
// document.getElementById("usuarioForm").addEventListener("submit", salvarUsuario);
carregarUsuarios(); // Carregar os usuários que é uma função que vamos criar

function carregarUsuarios()
{
    // Fetch é uma função que faz uma requisição HTTP
    fetch(API)
    .then(res => res.json()) // res.json() é uma função que converte o coneúdo da resposta para json
    .then(data => 
        {
            const tbody = document.querySelector("#tabelaUsuarios tbody")
            tbody.innerHTML = ""; // é uma propriedade que define ou retorna o conteúdo HTML de um elemento

            data.forEach(usuario => 
                {
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