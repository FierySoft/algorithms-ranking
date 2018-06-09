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
    accuracyRate?: ResearchRate[];
    efficiencyRate?: ResearchRate[];
    status: number;
    createdAt: Date;
    assignedAt?: Date;
    startedAt?: Date;
    executedAt?: Date;
    closedAt?: Date;
}

export class ResearchRate {
    constructor(
        public id: number,
        public researchId: number,
        public type: number,
        public value: number
    ) { }
}


export class ResearchFolders {
    active: Research[];
    toConfirm: Research[];
    archive: Research[];
}

export class ResearchInitForm {
    name: string;
    description: string;
    algorithmId?: number = null;
    dataSetId?: number = null;
    creatorId: number;
    executorId?: number = null;
}

export class ResearchCalculatedForm {
    accuracyRates: ResearchRate[] = [];
    efficiencyRates: ResearchRate[] = [];
}

export class ResearchPermissions {
    canRead: boolean;
    canEditInit: boolean;
    canEditCalculated: boolean;
    canPostComment: boolean;
    statusChangeOptions: number[];
}

export class ResearchForm {
    id: number;
    research: Research;
    init: ResearchInitForm;
    calculated: ResearchCalculatedForm;
    algorithms: EntityListItem[];
    dataSets: EntityListItem[];
    executors: EntityListItem[];
    permissions: ResearchPermissions;
}

export class ResearchStatus {
    constructor(
        public code: number,
        public name: string,
        public color: string = null,
        public icon: string = null
    ) { }

    public static get opened() { return new ResearchStatus(0, 'Новое', '#b2b2b2', 'circle-o'); }
    public static get assigned() { return new ResearchStatus(1, 'Назначено', '#f2cb1d', 'circle'); }
    public static get inProgress() { return new ResearchStatus(2, 'В работе', '#007acc', 'spinner'); }
    public static get executed() { return new ResearchStatus(3, 'Выполнено', '#ff9d00', 'question-circle'); }
    public static get declined() { return new ResearchStatus(4, 'Отклонено', '#cc293d', 'times-circle'); }
    public static get closed() { return new ResearchStatus(5, 'Закрыто', '#339933', 'check-circle'); }

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
