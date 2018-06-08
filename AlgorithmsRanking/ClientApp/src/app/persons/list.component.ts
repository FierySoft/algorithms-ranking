import { Component, OnInit } from '@angular/core';

import { PersonsService } from './persons.service';
import { Account, Person } from './persons.models';

@Component({
    templateUrl: 'list.component.html'
})
export class PersonsListComponent implements OnInit {
    items: Account[];

    constructor(private _persons: PersonsService) { }

    ngOnInit() {
        this._persons.getPersons()
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );
    }

    displayingAccount: Account;
    public display = (acc: Account) => this.displayingAccount = acc;
    public hide = () => this.displayingAccount = null;

    public onSorted(event) {
        const col = event.sortColumn as string;
        const dir = event.sortDirection as string;

        this.items.sort((a: Account, b: Account) => {
            return dir === 'asc' ?
                a.person[col] === b.person[col] ? 0 : a.person[col] > b.person[col] ? 1 : -1 :
                a.person[col] === b.person[col] ? 0 : a.person[col] > b.person[col] ? -1 : 1;

        });
    }

    public selectItem(id: number): void {
        if (!id) { return; }
        this._persons.gotoItem(id);
    }

    public logs(accountId: number) {
        if (!accountId) { return; }
        this._persons.gotoLogs(accountId);
    }

    public deleteItem(item: Account) {
        if (!item) { return; }
        if (confirm(`Вы действительно хотите удалить \'${item.person.shortName}\'?`)) {
            this._persons.deletePerson(item.personId);
        }
    }

    public createItem() {
        this._persons.gotoCreate();
    }
}
