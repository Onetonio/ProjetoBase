import React from "react";
import TarefaListar from "./components/tarefa-listar";
import TarefaListarNaoConcluidas from "./components/tarefa-listarnaoconcluidas";
import TarefaListarConcluidas from "./components/tarefa-listarconcluidas";
import { BrowserRouter, Link, Route, Routes } from "react-router-dom";
import TarefaCadastrar from "./components/tarefa-cadastrar";
import TarefaAlterar from "./components/tarefa-alterar";



function App() {
  return (
    <div>
      <BrowserRouter>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/pages/tarefas/listarnaoconcluidas">Listar tarefas não concluídas</Link>
            </li>
            <li>
              <Link to="/pages/tarefas/listarconcluidas">Listar tarefas concluídas</Link>
            </li>
            <li>
              <Link to ="/pages/tarefas/cadastrar">Cadastrar uma tarefa</Link>
            </li>
            <li>
              <Link to ="/pages/tarefas/alterar">Alterar uma tarefa</Link>
            </li>
            



          </ul>
        </nav>
        <Routes>
        <Route path="/" element={<TarefaListar/>} />
        <Route path="/pages/tarefas/listarnaoconcluidas" element={<TarefaListarNaoConcluidas/>} />
        <Route path="/pages/tarefas/listarconcluidas" element={<TarefaListarConcluidas/>} />
        <Route path="/pages/tarefas/cadastrar" element={<TarefaCadastrar/>} />
        <Route path="/pages/tarefas/alterar" element={<TarefaAlterar/>} />
        

        
        </Routes>
        <footer>
          <p>Gestão de Tarefas</p>
        </footer>
      </BrowserRouter>
    </div>
  );
}

export default App;