import React, { useEffect, useState } from "react";
import axios from "axios";
import { BrowserRouter, Link, Route, Routes, useNavigate } from "react-router-dom";
import { Tarefa } from "../models/Tarefa";


function TarefaAlterar() {
    const [id, setId] = useState("");

    const [tarefas, setTarefas] = useState<Tarefa[]>([]);

    useEffect(() => {
      carregarTarefas();
    }, []);
  
    function carregarTarefas() {
      axios.get("http://localhost:5000/tarefas/naoconcluidas")
        .then((response) => {
          setTarefas(response.data);
          console.table(response.data);
        })
        .catch((error) => {
          console.log("Erro ao carregar Tarefas", error);
        });
    }
  
  

    function alterarTarefa(){
        axios
      .put<Tarefa>(`http://localhost:5000/tarefas/alterar/${id}`)
      .then((response) => {
        console.log("Tarefa alterada", response.data);      
      })
      .catch((error) => {
        console.error("Erro ao alterar tarefa", error);
      });
    }
    return (
        <div>
          <h1>Alterar Tarefa</h1>
          
          <form onSubmit={alterarTarefa}>
            <label>Digite o id da tarefa que gostaria de alterar</label>
            <input
              type="text"
              value={id}
              onChange={(e) => setId(e.target.value)}
              required
            />{" "}
            <button type="submit">Alterar</button>
          </form>

          <div>
      <h2>Lista de tarefas não concluídas</h2>
      <h3></h3>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Descrição</th>
            <th>Criado em:</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {tarefas.map((Tarefa) => (
            <tr key={Tarefa.id}>
              <td>{Tarefa.tarefaId}</td>
              <td>{Tarefa.titulo}</td>
              <td>{Tarefa.descricao}</td>
              <td>{Tarefa.criadoEm}</td>
              <td>{Tarefa.status}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
        </div>

        
      );
  }

  


export default TarefaAlterar;
