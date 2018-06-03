import { Person, Algorithm, DataSet } from '../app.models';
export { Person, Algorithm, DataSet } from '../app.models';

export class Research {
    id: number;
    name: string;
    description: string;
    creatorId: number;
    creator: Person;
    executorId?: number;
    executor: Person;
    algorithmId: number;
    algorithm: Algorithm;
    dataSetId: number;
    dataSet: DataSet;
    accuracyRate?: number;
    efficiencyRate?: number;
    status: number;
    createdAt: Date;
    AssignedAt?: Date;
    StartedAt?: Date;
    ExecutedAt?: Date;
    ClosedAt?: Date;
}

export class ResearchUpdate {
    name: string;
    description: string;
    creatorId: number = 1; // TODO: временное решение, выпилить!
    executorId?: number = 2; // TODO: временное решение, выпилить!
    algorithmId?: number = null;
    dataSetId?: number = null;
}

export class ResearchForm {
    model: ResearchUpdate;
    algorithms: EntityListItem[];
    dataSets: EntityListItem[];
}

export class ResearchStatus {
    constructor(
        public code: number,
        public name: string,
        public color: string = null,
        public icon: string = null
    ) { }

    public static get opened() { return new ResearchStatus(0, 'Новое'); }
    public static get assigned() { return new ResearchStatus(1, 'Назначено'); }
    public static get inProgress() { return new ResearchStatus(2, 'В работе'); }
    public static get executed() { return new ResearchStatus(3, 'Выполнено'); }
    public static get declined() { return new ResearchStatus(4, 'Отклонено'); }
    public static get closed() { return new ResearchStatus(5, 'Закрыто'); }

    public static get all(): ResearchStatus[] {
        return [
            ResearchStatus.opened,
            ResearchStatus.assigned,
            ResearchStatus.inProgress,
            ResearchStatus.executed,
            ResearchStatus.declined,
            ResearchStatus.closed
        ];
    }

    public static byCode = (code: number): ResearchStatus => ResearchStatus.all.find(x => x.code === code);
}

export class EntityListItem {
    id: number;
    name: string;
}

export class Comment {
    id: number;
    researchId: number;
    content: string;
    author: string;
    postedAt: Date;
    isDeleted: boolean;
}
