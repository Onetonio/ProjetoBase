import { Categoria} from './Categoria';
export interface Tarefa{
    id?: string;
    tarefaId?: string;
    titulo: string;
    descricao: string;
    criadoEm?: string;
    categoria?: Categoria;
    categoriaId?: string;
    status?: string;
}

