import { Component } from '@angular/core';

import { PersonsService } from './persons.service';
import { Person } from './persons.models';

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
    value: Person;

    constructor(private _persons: PersonsService) {
        this.value = new Person();
    }

    public submit(value: Person) {
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
