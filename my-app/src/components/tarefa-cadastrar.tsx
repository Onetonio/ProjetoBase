import React, { useEffect, useState } from "react";
import axios from "axios";
import { Categoria } from "../models/Categoria";
import { Tarefa } from "../models/Tarefa";


function TarefaCadastrar() {
  const [titulo, setTitulo] = useState("");
  const [descricao, setDescricao] = useState("");
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [categoriaId, setCategoriaId] = useState("");



  useEffect(() => {
    axios
      .get<Categoria[]>("http://localhost:5000/categoria/listar")
      .then((response) => setCategorias(response.data))
      .catch((error) => console.error("Erro ao carregar Tarefas", error));
  }, []);

  function cadastrar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    const tarefa: Tarefa = {
      titulo,
      descricao,
      categoriaId,
    };

    axios
      .post<Tarefa>("http://localhost:5000/tarefas/cadastrar", tarefa)
      .then((response) => {
        console.log("Tarefa cadastrada com sucesso", response.data);
        setTitulo("");
        setDescricao("");
        setCategoriaId("");

      })
      .catch((error) => {
        console.error("Erro ao cadastrar tarefa", error);
      });
  }

  return (
    <div>
      <h1>Cadastrar Tarefa</h1>
      <form onSubmit={cadastrar}>
        <label>Título</label>
        <input
          type="text"
          value={titulo}
          onChange={(e) => setTitulo(e.target.value)}
          required
        />{" "}
        <br />
        <label>Categoria</label>
        <select
          value={categoriaId}
          onChange={(e) => setCategoriaId(e.target.value)}
          required
        >
          <option value="">Selecione uma categoria</option>
          {categorias.map((Categoria) => (
            <option key={Categoria.categoriaId} value={Categoria.categoriaId}>
              {Categoria.nome}
            </option>
          ))}
        </select>{" "}
        <br />
        <label>Descrição</label>
        <textarea
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
        />{" "}
        <br />
        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}

export default TarefaCadastrar;
