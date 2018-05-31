import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import { PersonsService } from './persons.service';
import { Person } from './persons.models';

@Component({
    template: `
        <spinner [active]="!value"></spinner>
        <div *ngIf="value">
            <h3>Участник '{{ value.lastName }}'</h3>
            <person-form
                [value]="value"
                (save)="submit($event)"
                (cancel)="cancel()">
            </person-form>
        </div>
    `
})
export class PersonsEditComponent implements OnInit {
    value: Person;

    constructor(
        private _persons: PersonsService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .params
            .switchMap((params: Params) => this._persons.getPerson(+params['id']))
            .subscribe(
                result => this.value = result,
                error => console.log(error)
            );
    }

    public submit(value: Person) {
        if (!value) { return; }

        this.value = value;
        this._persons.putPerson(this.value.id, this.value)
            .subscribe(
                result => this._persons.gotoList(),
                error => console.log(error)
            );
    }

    public cancel() {
        this._persons.gotoList();
    }
}
