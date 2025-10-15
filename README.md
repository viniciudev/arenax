# 🏟️ ArenaX

**ArenaX** é uma plataforma completa desenvolvida em **C# / .NET 8**, voltada para o **agendamento inteligente de quadras esportivas** com integração de pagamentos e sistema de matchmaking.

---

## 🚀 Tecnologias Principais

- **.NET 8 / ASP.NET Core**
- **C#**
- **Entity Framework Core** (Migrations / Code First)
- **SignalR/Firebase** (notificações em tempo real)
- **SQL Server**
- **Frontend (em desenvolvimento)** — integração planejada com Flutter

---

## 🧩 Módulos e Funcionalidades Concluídas

### 🧍‍♂️ Acesso do Dono da Quadra
- ✅ Cadastro de dados pessoais  
- ✅ Disponibilização de vagas e horários  
- 🟡 Agendamento na quadra com **pagamento obrigatório** (Cartão, Pix etc.)  
- ✅ Cancelamento de reservas com reembolso automático *(em fase de definição das regras)*  
- ✅ Sistema de **matchmaking** (busca de jogadores extras)  
- ✅ Exibição de **nível do jogador (0–5)** + avaliações  
- ✅ Criação de **eventos personalizados** e envio de convites  

---

### ⚽ Acesso do Jogador
- ✅ Busca de quadras com **filtros de horários e local**  
- ✅ Candidatura para participar de jogos disponíveis  
- 🟡 Pagamento após aceitação (checkout seguro e automatizado)  

---

### 📊 Extras & Complexidades
- ✅ Perfil de usuário com **estatísticas avançadas** (avaliações, comentários, histórico)  
- ✅ Notificações em **tempo real** (confirmação, lembretes, atualizações)  
- ✅ Sistema de **avaliação 1–5 estrelas** e histórico detalhado  
- 🕓 Integrações planejadas:
  - 🔸 Google Pay   
  - 🔸 Autenticação Social (Google / Apple Sign-In) *  
  - 🔸 Apple Pay *  


---

## 🧱 Estrutura do Projeto (sugerida)

arenax/
├─ src/
│ ├─ ArenaX.Api/ ← API principal (.NET 8)
│ ├─ ArenaX.Core/ ← Entidades, serviços, regras de negócio
│ ├─ ArenaX.Infrastructure/ ← Contexto, repositórios, migrations
│ ├─ ArenaX.Notifications/ ← SignalR / mensageria
├─ tests/ ← Testes unitários e de integração
├─ docs/ ← Documentação técnica
├─ assets/ ← Ícones, vídeos, mídias
├─ README.md
└─ .gitignore
