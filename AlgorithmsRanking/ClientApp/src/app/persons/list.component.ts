import { Component, OnInit } from '@angular/core';

import { PersonsService } from './persons.service';
import { Person } from './persons.models';

@Component({
    templateUrl: 'list.component.html'
})
export class PersonsListComponent implements OnInit {
    items: Person[];

    constructor(private _persons: PersonsService) { }

    ngOnInit() {
        this._persons.getPersons()
            .subscribe(
                result => this.items = result,
                error => console.log(error)
            );
    }

    public display = (p: Person) => alert(`
        Фамилия: ${p.lastName}
        Имя: ${p.firstName}
        Отчество: ${p.middleName}
        Email: ${p.email}
        Телефон: ${p.phone}
    `)

    public selectItem(id: number): void {
        if (!id) { return; }
        this._persons.gotoItem(id);
    }

    public deleteItem(item: Person) {
        if (!item) { return; }
        if (confirm(`Вы действительно хотите удалить \'${item.shortName}\'?`)) {
            this._persons.deletePerson(item.id);
        }
    }

    public createItem() {
        this._persons.gotoCreate();
    }
}
