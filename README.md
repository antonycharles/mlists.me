# Mlists (To Do)
##### To Do List com ícones de monstros feito com .Net Core
![Mlists me](https://user-images.githubusercontent.com/24979597/89239321-386f8280-d5cf-11ea-81c6-cd37bc5cefc7.gif)

### Arquitetura
![arquitetura-sistema](https://user-images.githubusercontent.com/24979597/91216365-325f5400-e6ec-11ea-9c21-d43698b85f7c.png)

## Requisitos
Para executar o projeto, será necessário instalar os seguintes programas:

<ul>
  <li>Visual Studio</li>
  <li>Banco de dados Mysql</li>
  <li>.Net core 3.1</li>
</ul>

## Desenvolvimento

Para iniciar o desenvolvimento, é necessário:
<ol>
  <li>
    Clonar o projeto do GitHub num diretório de sua preferência
    <pre>
      cd "Seu diretorio"
      git clone https://github.com/antonycharles/mlists.me.git</pre>
  </li>
  <li>
    Criar arquivo <b>appsettings.json</b> usando o arquivo de modelo <b>appsettings-modelo.json</b>
  </li>
  <li>
    Adicionar informações do banco de dados no arquivo appsettings.json
    <pre>"ConnectionStrings": {
    "AppContextConnection": "Server=localhost;DataBase=NOME_BANCO_DE_DADOS;Uid=root;Pwd=SENHA_BANCO_DE_DADOS"
  },</pre>
  </li>
  <li>
    Subir Migrations para o banco de dados no projeto <b>me.mlists.data</b>
    <pre>
      Update-Database -Context AppIdentityContext
      Update-Database -Context ApplicationContext</pre>
  </li>
</ol>
