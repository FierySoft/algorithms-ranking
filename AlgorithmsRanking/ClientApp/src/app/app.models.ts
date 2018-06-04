export class Person {
    id: number;
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    phone: string;

    fullName: string;
    shortName: string;
}

export class Algorithm {
    id: number;
    name: string;
    type: string;
}

export class DataSet {
    id: number;
    name: string;
    type: string;
    attributesCount: number;
    stringsCount: number;
    files: string[];
    filesCount: number;
}

export class Attachment {
    id: number;
    dataSetId: number;
    constructor (public url: string) { }
}
