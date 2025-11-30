# EmpreendedoresApp

Aplicativo simples em **WPF (.NET)** criado para apoiar pequenos empreendedores da comunidade, permitindo:

- Cadastro de produtos/serviços  
- Registro de vendas e entradas  
- Registro de saídas  
- Relatório financeiro básico  
- Geração futura de PDF  

O projeto foi desenvolvido como parte do **Projeto Integrador da faculdade (Análise e Desenvolvimento de Sistemas)**.

---

## 📦 Tecnologias Utilizadas

- C# (.NET 8)
- WPF (Windows Presentation Foundation)
- Entity Framework Core
- SQLite (banco de dados local)

---

## 📁 Estrutura do Projeto
EmpreendedoresApp/
├── Models/ # Classes do sistema (Produto, Venda...)
├── Views/ # Telas WPF
├── ViewModels/ # (Futuro) Lógica de apresentação (MVVM)
├── Data/ # AppDbContext e acesso ao banco
├── Migrations/ # Histórico das mudanças no banco
└── sistema.db # Banco SQLite (ignorado no Git se preferir)


---

## 🚀 Como Rodar o Projeto

1. Clone o repositório:
git clone https://github.com/wellfaria/EmpreenderApp.git

2. Abra a solução no Visual Studio

3. Rode as migrations (cria o banco automaticamente):

update-database

4. Execute o projeto (F5)

---

## 🗃 Banco de Dados

O banco é criado automaticamente através do Entity Framework Core usando o comando:

update-database


Você também pode gerar novas migrations com:

add-migration NomeDaMigration


---

## 📌 Status do Projeto

🟢 Em desenvolvimento  
🟡 Funcionalidades básicas sendo implementadas  
🔴 Sistema ainda não pronto para produção  

---

## 👤 Autor

**Wellington Faria**  
Estudante de Análise e Desenvolvimento de Sistemas  
2º Período  
Projeto Integrador — 2025




