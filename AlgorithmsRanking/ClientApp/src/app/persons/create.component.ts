import { Component } from '@angular/core';

import { PersonsService } from './persons.service';
import { Person, Account } from './persons.models';

@Component({
    template: `
        <h3>Новый участник</h3>
        <person-form
            [value]="value"
            (save)="submit($event)"
            (cancel)="cancel()">
        </person-form>
    `
})
export class PersonsCreateComponent {
    value: Account;

    constructor(private _persons: PersonsService) {
        this.value = new Account();
        this.value.person = new Person();
    }

    public submit(value: Account) {
        if (!value) { return; }

        this.value = value;
        this._persons.postPerson(this.value)
            .subscribe(
                result => this._persons.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._persons.gotoList();
    }
}
