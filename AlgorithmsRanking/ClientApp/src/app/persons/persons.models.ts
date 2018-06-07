import { Person } from '../app.models';
export { Person } from '../app.models';

export class Account {
    id: number;
    userName: string;
    password: string;
    role: string = null;
    avatarUri: string;
    personId: number;
    person: Person;
    registeredAt: Date;
}
