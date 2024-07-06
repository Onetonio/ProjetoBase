import React, { useEffect, useState } from "react";
import axios from "axios";
import { Tarefa } from "../models/Tarefa";

function TarefaListarNaoConcluidas() {
  const [tarefas, setTarefas] = useState<Tarefa[]>([]);
  const [id, setId] = useState("");

  useEffect(() => {
    carregarTarefas();
    alterarTarefa("aa46781b-3af5-4cd6-94fd-f4c2b3239d4c");
  }, []);

  function carregarTarefas() {
    axios.get("http://localhost:5000/tarefas/naoconcluidas")
      .then((response) => {
        setTarefas(response.data);
      })
      .catch((error) => {
        console.log("Erro ao carregar tarefas", error);
      });
  }

  function alterarTarefa(id: string) {
    axios.put(`http://localhost:5000/tarefas/alterar/${id}`, tarefas)
      .then(() => {
        <h1>Tarefa alterada com sucesso</h1>
      })
      .catch((error) => {
        console.log("Erro ao alterar Tarefa", error);
      });
  }

  return (
    <div>
      <h1>Listar Tarefas não concluídas</h1>
      <table>
        <thead>
          <tr>
            <th>Título</th>
            <th>Descrição</th>
            <th>Criado em:</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {tarefas.map((Tarefa) => (
            <tr key={Tarefa.id}>
              <td>{Tarefa.titulo}</td>
              <td>{Tarefa.descricao}</td>
              <td>{Tarefa.criadoEm}</td>
              <td>{Tarefa.status}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TarefaListarNaoConcluidas;
