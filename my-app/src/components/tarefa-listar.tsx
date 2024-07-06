import { useEffect, useState } from "react";
import axios from "axios";
import { Tarefa } from "../models/Tarefa";
import { Link } from "react-router-dom";

function TarefaListar() {
  const [tarefas, setTarefas] = useState<Tarefa[]>([]);

  useEffect(() => {
    carregarTarefas();
  }, []);

  function carregarTarefas() {
    axios.get("http://localhost:5000/tarefas/listar")
      .then((response) => {
        setTarefas(response.data);
        console.table(response.data);
      })
      .catch((error) => {
        console.log("Erro ao carregar Tarefas de pagamento", error);
      });
  }

  return (
    <div>
      <h1>Listar Tarefas</h1>
      <h3>Caso queira concluir uma tarefa, entre no link de alterar tarefa</h3>
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
  );
}

export default TarefaListar;
